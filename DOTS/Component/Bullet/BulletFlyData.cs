using Unity.Entities;

namespace Script.DOTS.Component.Bullet
{
    public struct BulletFlyData : IComponentData
    {
        public float speed;
        public Unity.Entities.Entity ce;
        public float attack;
    }
}
