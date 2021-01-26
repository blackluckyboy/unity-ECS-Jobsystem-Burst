// using Script.DOTS.Component;
// using Unity.Burst;
// using Unity.Entities;
// using Unity.Physics;
//
// namespace Script.DOTS.Jobs
// {
//     [BurstCompile]
//     struct CollisionJob : ICollisionEventsJob
//     {
//         public ComponentDataFromEntity<PhysicsCollider> PhysicColliderGroup;
//         public ComponentDataFromEntity<BulletFlyData> BulletFlyDataGroup;
//         public EntityCommandBuffer.ParallelWriter ParallelWriter;
//         public void Execute(CollisionEvent collisionEvent)
//         {
//             var b1 = BulletFlyDataGroup.HasComponent(collisionEvent.EntityA);
//             var b2 = PhysicColliderGroup.HasComponent(collisionEvent.EntityB);
//             if (b1 && b2)
//             {
//                 ParallelWriter.DestroyEntity(collisionEvent.EntityA.Index,collisionEvent.EntityA);
//             }
//         }
//     }
// }
