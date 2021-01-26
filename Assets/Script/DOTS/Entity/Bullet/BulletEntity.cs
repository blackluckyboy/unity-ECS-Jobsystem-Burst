using Script.DOTS.Component;
using Script.DOTS.Component.Bullet;
using Script.ECS.Entity;
using Unity.Entities;

namespace Script.DOTS.Entity.Bullet
{
    public class BulletEntity : EntityBase
    {
        public override void Convert(Unity.Entities.Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new BulletFlyData {speed = 100,attack = 1});
        }
    }
}