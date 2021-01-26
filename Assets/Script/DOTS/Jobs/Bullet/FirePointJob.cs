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
    public struct FirePointJob : IJobChunk
    {
        public ComponentTypeHandle<FirePointData> FirePointDataTypeHandle;
        public ComponentTypeHandle<LocalToWorld> LocalToWorldTypeHandle;
        public EntityCommandBuffer.ParallelWriter commandBuffer;
        [ReadOnly] public NativeArray<Unity.Entities.Entity> enemyQuery;
        [ReadOnly] public ComponentDataFromEntity<Translation> enemyTranslation;
        public float deltaTime;

        public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
        {
            var firePoints = chunk.GetNativeArray(FirePointDataTypeHandle);
            var localToWorlds = chunk.GetNativeArray(LocalToWorldTypeHandle);
            for (int i = 0; i < chunk.Count; i++)
            {
                var data = firePoints[i];
                var localPos = localToWorlds[i];
                var targetEntity = CheckInRangeEntity(data.range, localPos.Position);
                if (targetEntity != Unity.Entities.Entity.Null)
                {
                    var leftTime = data.time;
                    float newTime;

                    if (leftTime <= 0)
                    {
                        var dir = enemyTranslation[targetEntity].Value - localPos.Position;
                        
                        newTime = data.interval - leftTime;
                        var instance = commandBuffer.Instantiate(chunkIndex, data.Prefab);
                        commandBuffer.SetComponent(chunkIndex, instance, new Translation {Value = localPos.Position});
                        commandBuffer.SetComponent(chunkIndex, instance, new Rotation() {Value = quaternion.LookRotation(dir, math.up())});
                        commandBuffer.AddComponent(chunkIndex, instance, new DeltaTranslationData {deltaTranslation = float3.zero});
                        commandBuffer.AddComponent(chunkIndex, instance, new BulletFlyData {speed = 50, ce = instance,attack = 1});//TODO 读表子弹飞行速度,子弹伤害
                        // commandBuffer.AddComponent(chunkIndex, instance, new BulletRotateData {targetEntity = targetEntity,myEntity = instance});
                    }
                    else
                    {
                        newTime = data.time - deltaTime;
                    }

                    firePoints[i] = new FirePointData
                    {
                        bulletId = data.bulletId,
                        interval = data.interval,
                        position = data.position,
                        Prefab = data.Prefab,
                        time = newTime,
                        range = data.range,
                    };
                }
            }
        }

        private Unity.Entities.Entity CheckInRangeEntity(float range, float3 myPos)
        {
            for (var index = 0; index < enemyQuery.Length; index++)
            {
                var entity = enemyQuery[index];
                if (entity == Unity.Entities.Entity.Null) continue;
                var entityTranslation = enemyTranslation[entity];
                if (math.distance(myPos, entityTranslation.Value) <= range)
                {
                    return entity;
                }
            }

            return Unity.Entities.Entity.Null;
        }
    }
}