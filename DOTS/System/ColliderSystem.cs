// using Script.DOTS.Component;
// using Script.DOTS.Jobs;
// using Unity.Entities;
// using Unity.Physics;
// using Unity.Physics.Systems;
//
// namespace Script.DOTS.System
// {
//     [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
//     [UpdateAfter(typeof(EndFramePhysicsSystem))]
//     public class ColliderSystem : SystemBase
//     {
//         private BuildPhysicsWorld buildPhysicsWorld;
//         private StepPhysicsWorld stepPhysicsWorld;
//         private EntityCommandBufferSystem m_Barrier;
//
//         protected override void OnCreate()
//         {
//             buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
//             stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
//             m_Barrier = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
//         }
//
//         protected override void OnUpdate()
//         {
//             Dependency = new CollisionJob
//             {
//                 PhysicColliderGroup = GetComponentDataFromEntity<PhysicsCollider>(),
//                 BulletFlyDataGroup = GetComponentDataFromEntity<BulletFlyData>(),
//                 ParallelWriter = m_Barrier.CreateCommandBuffer().AsParallelWriter(),
//             }.Schedule(stepPhysicsWorld.Simulation, ref buildPhysicsWorld.PhysicsWorld, Dependency);
//             m_Barrier.AddJobHandleForProducer(Dependency);
//         }
//     }
// }