using Unity.Entities;
using UnityEngine;

namespace Script.ECS.Entity
{
    [RequireComponent(typeof(ConvertToEntity))]
    public abstract class EntityBase : MonoBehaviour,IConvertGameObjectToEntity
    {
        public abstract void Convert(Unity.Entities.Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem);
    }
}
