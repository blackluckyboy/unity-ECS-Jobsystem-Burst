using Script.DOTS.Component.Enemy;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Script.DOTS.Jobs.Enemy
{
    [BurstCompile]
    public struct EnemyMoveJob : IJobChunk
    {
        public ComponentTypeHandle<EnemyMoveData> EnemyMoveDataTypeHandle;
        public ComponentTypeHandle<Translation> TranslationTypeHandle;
        public ComponentTypeHandle<Rotation> RotationTypeHandle;
        public float deltaTime;

        public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
        {
            var EnemyMoveDates = chunk.GetNativeArray(EnemyMoveDataTypeHandle);
            var Translations = chunk.GetNativeArray(TranslationTypeHandle);
            var Rotations = chunk.GetNativeArray(RotationTypeHandle);
            for (int i = 0; i < chunk.Count; i++)
            {
                var data = EnemyMoveDates[i];
                var translation = Translations[i];
                var rotation = Rotations[i];
                var dir = math.forward(rotation.Value);
                Translations[i] = new Translation
                {
                    Value = translation.Value + math.normalize(dir) * data.moveSpeed * deltaTime,
                };
            }
        }
    }
}