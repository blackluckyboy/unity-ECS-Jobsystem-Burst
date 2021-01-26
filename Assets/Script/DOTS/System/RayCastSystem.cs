using Script.DOTS.Component;
using Script.DOTS.Component.Bullet;
using Script.DOTS.Component.Enemy;
using Script.DOTS.Jobs;
using Script.Manager;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;

namespace Script.DOTS.System
{
    // [UpdateAfter(typeof(BuildPhysicsWorld)), UpdateBefore(typeof(StepPhysicsWorld))]
    public class RayCastSystem : SystemBase
    {
        private BuildPhysicsWorld buildPhysicsWorld;
        private EndSimulationEntityCommandBufferSystem endSimCommandBufferSystem;
        private EntityQuery _query;
        protected override void OnCreate()
        {
            _query = GetEntityQuery(typeof(BulletFlyData), typeof(Translation), typeof(DeltaTranslationData));
            buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
            endSimCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            ref PhysicsWorld world = ref buildPhysicsWorld.PhysicsWorld;
            RayCastJob job = new RayCastJob
            {
                BulletFlyTypeHandle = GetComponentTypeHandle<BulletFlyData>(),
                TranslationTypeHandle = GetComponentTypeHandle<Translation>(),
                DeltaTranslationDataHandle = GetComponentTypeHandle<DeltaTranslationData>(),
                world = world,
                commandBuffer = endSimCommandBufferSystem.CreateCommandBuffer().AsParallelWriter(),
                EnemyDataFromEntity = GetComponentDataFromEntity<EnemyHpData>(),
            };
            
            Dependency = job.Schedule(_query, Dependency);
        }
    }
}