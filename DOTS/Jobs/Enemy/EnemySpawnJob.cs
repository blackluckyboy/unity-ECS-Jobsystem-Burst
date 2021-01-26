using Script.DOTS.Component.Enemy;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Script.DOTS.Jobs.Enemy
{
    [BurstCompile]
    public struct EnemySpawnJob : IJobChunk
    {
        public ComponentTypeHandle<EnemySpawnData> EnemySpawnDataTypeHandle;
        public EntityCommandBuffer.ParallelWriter commandBuffer;
        public float deltaTime;

        public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
        {
            var EnemySpawnDates = chunk.GetNativeArray(EnemySpawnDataTypeHandle);
            for (int i = 0; i < chunk.Count; i++)
            {
                var data = EnemySpawnDates[i];
                var leftTime = data.time;
                float newTime;
                if (leftTime <= 0)
                {
                    newTime = data.interval - leftTime;
                    var instance = commandBuffer.Instantiate(chunkIndex, data.Prefab);
                    commandBuffer.SetComponent(chunkIndex, instance, new Translation {Value = new float3(0, 0.5f, 20)});
                    commandBuffer.AddComponent(chunkIndex, instance, new EnemyHpData {hp = 50});
                    commandBuffer.AddComponent(chunkIndex, instance, new EnemyMoveData {moveSpeed = 5, rotateSpeed = 10, Entity = instance});
                    commandBuffer.AddComponent(chunkIndex, instance, new EnemyRotateData {rotateSpeed = 10, Entity = instance});
                }
                else
                {
                    newTime = data.time - deltaTime;
                }

                EnemySpawnDates[i] = new EnemySpawnData
                {
                    enemyId = data.enemyId,
                    interval = data.interval,
                    Prefab = data.Prefab,
                    time = newTime
                };
            }
        }
    }
}