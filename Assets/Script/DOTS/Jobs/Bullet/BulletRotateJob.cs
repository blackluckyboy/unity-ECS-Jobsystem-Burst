// using Script.DOTS.Component.Bullet;
// using Unity.Burst;
// using Unity.Collections;
// using Unity.Entities;
// using Unity.Mathematics;
// using Unity.Transforms;
// using UnityEngine;
// using NotImplementedException = System.NotImplementedException;
//
// namespace Script.DOTS.Jobs.Bullet
// {
//     [BurstCompile]
//     public struct BulletRotateJob : IJobChunk
//     {
//         public ComponentTypeHandle<Rotation> rotationTypeHandle;
//         public ComponentTypeHandle<BulletRotateData> bulletRotateDataTypeHandle;
//         [ReadOnly] public ComponentDataFromEntity<Translation> TranslationFromEntity;
//
//         public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
//         {
//             var Rotations = chunk.GetNativeArray(rotationTypeHandle);
//             var bulletRotateDates = chunk.GetNativeArray(bulletRotateDataTypeHandle);
//
//             for (int i = 0; i < chunk.Count; i++)
//             {
//                 var data = bulletRotateDates[i];
//                 var myTranslation = TranslationFromEntity[data.myEntity].Value;
//                 var targetTranslation = TranslationFromEntity[data.targetEntity].Value;
//                 var dir = targetTranslation - myTranslation;
//
//                 Rotations[i] = new Rotation
//                 {
//                     Value = quaternion.LookRotation(dir, math.up()),
//                 };
//             }
//             
//         }
//     }
// }