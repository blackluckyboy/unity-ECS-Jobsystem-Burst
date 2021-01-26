using Script.DOTS.Component.Enemy;
using Script.DOTS.Jobs.Enemy;
using Unity.Entities;

namespace Script.DOTS.System.Enemy
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public class EnemySpawnSystem : SystemBase
    {
       
        BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;
        private EntityQuery _query;
        
        protected override void OnCreate()
        {
            _query = GetEntityQuery(typeof(EnemySpawnData));
            m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            Dependency = new EnemySpawnJob
            {
                EnemySpawnDataTypeHandle = GetComponentTypeHandle<EnemySpawnData>(),
                commandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer().AsParallelWriter(),
                deltaTime = Time.DeltaTime,
            }.Schedule(_query, Dependency);
            Dependency.Complete();
        }
    }
}
