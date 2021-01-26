using Unity.Entities;

namespace Script.DOTS.Component.Player
{
    public struct PlayerMoveData : IComponentData
    {
        public float moveSpeed;
        public int moveIndex;
        public Unity.Entities.Entity followEntity;
    }
}
