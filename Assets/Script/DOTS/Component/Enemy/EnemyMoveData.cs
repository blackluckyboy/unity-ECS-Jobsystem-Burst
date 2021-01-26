using Unity.Entities;
using UnityEngine;

namespace Script.DOTS.Component.Enemy
{
    public struct EnemyMoveData : IComponentData
    {
        public float moveSpeed;
        public float rotateSpeed;
        public Unity.Entities.Entity Entity;
    }
}
