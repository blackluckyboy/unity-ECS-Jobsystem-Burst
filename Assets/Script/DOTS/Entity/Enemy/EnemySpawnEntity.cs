using System.Collections.Generic;
using Script.DOTS.Component.Enemy;
using Script.ECS.Entity;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Script.DOTS.Entity.Enemy
{
    public class EnemySpawnEntity : EntityBase,IDeclareReferencedPrefabs
    {
        private GameObject prefab;

        public void Create(GameObject _prefab)
        {
            this.prefab = _prefab;
        }
        
        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(prefab);
        }
        public override void Convert(Unity.Entities.Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new EnemySpawnData
            {
                enemyId = 0,
                interval = 1,
                Prefab = conversionSystem.GetPrimaryEntity(prefab),
                time = 1,
            });
        }

       
    }
}
