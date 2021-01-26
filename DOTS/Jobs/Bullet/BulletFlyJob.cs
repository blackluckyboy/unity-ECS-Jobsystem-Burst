using Script.DOTS.Component;
using Script.DOTS.Component.Bullet;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Script.DOTS.Jobs.Bullet
{
    [BurstCompile]
    public struct BulletFlyJob : IJobChunk
    {
        public ComponentTypeHandle<Translation> translationHandle;
        public ComponentTypeHandle<Rotation> rotationHandle;
        [ReadOnly] public ComponentTypeHandle<BulletFlyData> bulletFlyDataHandle;
        public ComponentTypeHandle<DeltaTranslationData> deltaTransDataHandle;
        public float deltaTime;

        public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
        {
            var translations = chunk.GetNativeArray(translationHandle);
            var bulletFlyDates = chunk.GetNativeArray(bulletFlyDataHandle);
            var deltaTranslations = chunk.GetNativeArray(deltaTransDataHandle);
            var rotations = chunk.GetNativeArray(rotationHandle);

            for (int i = 0; i < chunk.Count; i++)
            {
                var translation = translations[i];
                var bulletFlyData = bulletFlyDates[i];
                var rotation = rotations[i];

                translations[i] = new Translation
                {
                    Value = translation.Value + math.forward(rotation.Value) * bulletFlyData.speed * deltaTime,
                };
                deltaTranslations[i] = new DeltaTranslationData
                {
                    deltaTranslation = translations[i].Value - translation.Value,
                };
            }
        }
    }
}