// using Script.DOTS.Component.Bullet;
// using Script.DOTS.Jobs.Bullet;
// using Unity.Entities;
// using Unity.Transforms;
// using UnityEngine;
// using NotImplementedException = System.NotImplementedException;
//
// namespace Script.DOTS.System.Bullet
// {
//     public class BulletRotateSystem : SystemBase
//     {
//         private EntityQuery _query;
//         
//         protected override void OnCreate()
//         {
//             _query = GetEntityQuery(typeof(BulletRotateData), typeof(Rotation));
//         }
//
//         protected override void OnUpdate()
//         {
//             Dependency = new BulletRotateJob
//             {
//                 bulletRotateDataTypeHandle = GetComponentTypeHandle<BulletRotateData>(),
//                 rotationTypeHandle = GetComponentTypeHandle<Rotation>(),
//                 TranslationFromEntity = GetComponentDataFromEntity<Translation>(),
//             }.Schedule(_query, Dependency);
//         }
//     }
// }
