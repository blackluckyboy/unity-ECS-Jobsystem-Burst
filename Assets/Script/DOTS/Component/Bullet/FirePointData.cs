using Unity.Entities;
using Unity.Mathematics;

namespace Script.DOTS.Component.Bullet
{
    public struct FirePointData : IComponentData
    {
        public int bulletId;
        public float interval;
        public float time;
        public float3 position;
        public Unity.Entities.Entity Prefab;
        public float range;
    }
}
