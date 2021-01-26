using Unity.Entities;
using UnityEngine;

namespace Script.DOTS.Component.Enemy
{
    public struct EnemyHpData : IComponentData
    {
        public float hp;
    }
}
