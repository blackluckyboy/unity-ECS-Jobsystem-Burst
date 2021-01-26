using Unity.Entities;
using UnityEngine;

namespace Script.DOTS.Component.Enemy
{
    public struct EnemyRotateData : IComponentData
    {
        public float rotateSpeed;
        public Unity.Entities.Entity Entity;
        
    }
}
