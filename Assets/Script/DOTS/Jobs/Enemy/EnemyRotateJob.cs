using Script.DOTS.Component.Enemy;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Script.DOTS.Jobs.Enemy
{
    [BurstCompile]
    public struct EnemyRotateJob : IJobChunk
    {
        [ReadOnly] public Unity.Entities.Entity targetEntity;
        [ReadOnly] public ComponentDataFromEntity<Translation> translationFromEntity;
        public ComponentTypeHandle<Rotation> RotationTypeHandle;
        public ComponentTypeHandle<EnemyRotateData> EnemyRotateDataTypeHandle;
        public float deltaTime;

        public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
        {
            var rotations = chunk.GetNativeArray(RotationTypeHandle);
            var rotateDates = chunk.GetNativeArray(EnemyRotateDataTypeHandle);

            for (int i = 0; i < chunk.Count; i++)
            {
                var rotateData = rotateDates[i];
                // var rotation = rotations[i];
                var myTranslation = translationFromEntity[rotateData.Entity];
                var targetTranslation = translationFromEntity[targetEntity];

                var targetDir = math.normalize(targetTranslation.Value - myTranslation.Value);
                
                rotations[i] = new Rotation
                {
                    Value = quaternion.LookRotation(targetDir,math.up()),
                };
            }
        }
    }
}