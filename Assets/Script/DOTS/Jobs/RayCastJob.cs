using Script.DOTS.Component;
using Script.DOTS.Component.Bullet;
using Script.DOTS.Component.Enemy;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace Script.DOTS.Jobs
{
    [BurstCompile]
    public struct RayCastJob : IJobChunk
    {
        public ComponentTypeHandle<BulletFlyData> BulletFlyTypeHandle;
        public ComponentTypeHandle<Translation> TranslationTypeHandle;
        public ComponentTypeHandle<DeltaTranslationData> DeltaTranslationDataHandle;
        public EntityCommandBuffer.ParallelWriter commandBuffer;
        [ReadOnly] public ComponentDataFromEntity<EnemyHpData> EnemyDataFromEntity;
        [ReadOnly] public PhysicsWorld world;
        

        public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
        {
            for (int i = 0; i < chunk.Count; i++)
            {
                var BulletFlyDates = chunk.GetNativeArray(BulletFlyTypeHandle);
                var Translations = chunk.GetNativeArray(TranslationTypeHandle);
                var DeltaTranslationDates = chunk.GetNativeArray(DeltaTranslationDataHandle);

                var data = BulletFlyDates[i];
                var translation = Translations[i];
                var deltaTranslationData = DeltaTranslationDates[i];

                Unity.Entities.Entity ce = data.ce;
                int idx = world.GetRigidBodyIndex(ce);
                CollisionFilter filter = world.GetCollisionFilter(idx);
                RaycastInput raycastInput = new RaycastInput
                {
                    Start = translation.Value,
                    End = translation.Value - deltaTranslationData.deltaTranslation,
                    Filter = filter,
                };
                bool hit = world.CastRay(raycastInput, out var rayResult);
                if (hit)
                {
                    commandBuffer.DestroyEntity(i, ce);
                    var atk = data.attack;
                    if (EnemyDataFromEntity.HasComponent(rayResult.Entity))
                    {
                        var hp = EnemyDataFromEntity[rayResult.Entity].hp;
                        if (hp - atk > 0)
                        {
                            commandBuffer.SetComponent(i, rayResult.Entity, new EnemyHpData
                            {
                                hp = hp - atk,
                            });
                        }
                        else
                        {
                            commandBuffer.DestroyEntity(i,rayResult.Entity);
                        }
                        
                    }
                }
            }
        }
    }
}