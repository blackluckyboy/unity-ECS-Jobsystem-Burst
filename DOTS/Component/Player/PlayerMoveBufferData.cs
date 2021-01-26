using Unity.Entities;
using Unity.Mathematics;

namespace Script.DOTS.Component.Player
{
    public struct PlayerMoveBufferData : IBufferElementData
    {
        public float3 Value;
    }
}
