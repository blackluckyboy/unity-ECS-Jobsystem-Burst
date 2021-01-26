using Script.DOTS.Component.Camera;
using Script.ECS.Entity;
using Unity.Entities;
using Unity.Mathematics;

namespace Script.DOTS.Entity.Camera
{
    public class CameraEntity : EntityBase
    {
        private float3 location;
        private quaternion rotation;
        
        public void Create(float3 _location,quaternion _rotation)
        {
            this.location = _location;
            this.rotation = _rotation;
            this.transform.localPosition = this.location;
            this.transform.localRotation = this.rotation;
        }
        
        public override void Convert(Unity.Entities.Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new CameraFollowData
            {
                location = this.location,
                rotation = this.rotation,
            });
        }
    }
}
