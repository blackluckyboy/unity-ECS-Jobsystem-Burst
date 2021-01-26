using Script.DOTS.Component.Camera;
using Script.DOTS.Entity.Camera;
using Script.Manager;
using Unity.Entities;
using Unity.Transforms;

namespace Script.DOTS.System.Camera
{
    public class CameraFollowSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var entityFromEntity = GetComponentDataFromEntity<Translation>();
            var CameraFollowEntity = PlayerManager.Instance.CameraFollowEntity;
            Entities.
                WithName("CameraFollowSystem").
                WithoutBurst().
                ForEach((CameraEntity Entity,int entityInQueryIndex,in CameraFollowData data ) =>
            {
                if (CameraFollowEntity != Unity.Entities.Entity.Null)
                {
                    var translation = entityFromEntity[CameraFollowEntity];
                    Entity.transform.position = translation.Value + data.location;
                }
               
            }).Run();
        }
    }
}
