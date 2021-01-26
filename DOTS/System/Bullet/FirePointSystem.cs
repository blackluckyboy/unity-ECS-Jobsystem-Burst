using Script.DOTS.Component;
using Script.DOTS.Component.Bullet;
using Script.DOTS.Component.Enemy;
using Script.DOTS.Jobs.Bullet;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Script.DOTS.System.Bullet
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public class FirePointSystem : SystemBase
    {
        BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;
        private EntityQuery _query;

        protected override void OnCreate()
        {
            _query = GetEntityQuery(typeof(FirePointData), typeof(LocalToWorld));
            m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
        }


        protected override void OnUpdate()
        {
            var arr =  GetEntityQuery(typeof(EnemyHpData)).ToEntityArray(Allocator.TempJob);
            Dependency = new FirePointJob
            {
                FirePointDataTypeHandle = GetComponentTypeHandle<FirePointData>(),
                LocalToWorldTypeHandle = GetComponentTypeHandle<LocalToWorld>(),
                commandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer().AsParallelWriter(),
                deltaTime = Time.DeltaTime,
                enemyQuery = arr,
                enemyTranslation = GetComponentDataFromEntity<Translation>(),
            }.Schedule(_query, Dependency);
            Dependency.Complete();
            arr.Dispose();
        }
    }
}