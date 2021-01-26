using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Script.DOTS.Component.Camera
{
    public struct CameraFollowData : IComponentData
    {
        public float3 location;
        public quaternion rotation;
    }
}
