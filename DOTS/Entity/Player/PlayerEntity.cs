using Script.DOTS.Component;
using Script.DOTS.Component.Player;
using Script.ECS.Entity;
using Script.Manager;
using Unity.Entities;

namespace Script.DOTS.Entity.Player
{
    public class PlayerEntity : EntityBase
    {
        private float speed = 0;
        private int index = 0;

        public void Create(float _speed, int _index)
        {
            this.speed = _speed;
            this.index = _index;
        }

        public override void Convert(Unity.Entities.Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponents(entity, new ComponentTypes(
                new ComponentType[]
                {
                    typeof(PlayerMoveBufferData),
                    typeof(PlayerMoveData),
                }));

            dstManager.SetComponentData(entity, new PlayerMoveData
            {
                moveSpeed = speed,
                moveIndex = index,
                followEntity = PlayerManager.Instance.FollowEntity,
            });
            PlayerManager.Instance.FollowEntity = entity;
            if (index == 0)
            {
                PlayerManager.Instance.CameraFollowEntity = entity;
            }
        }
    }
}