using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Script.DOTS.Component.Enemy
{
    public struct EnemySpawnData : IComponentData
    {
        public int enemyId;
        public float interval;
        public float time;
        public Unity.Entities.Entity Prefab;
    }
}
