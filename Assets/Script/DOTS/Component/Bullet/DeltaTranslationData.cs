using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Script.DOTS.Component
{
    public struct DeltaTranslationData : IComponentData
    {
        public float3 deltaTranslation;
    }
}
