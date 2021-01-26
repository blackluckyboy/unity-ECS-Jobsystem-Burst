using System;
using System.Collections.Generic;
using Script.DOTS.Component;
using Script.DOTS.Component.Bullet;
using Script.ECS.Entity;
using Unity.Entities;
using UnityEngine;

namespace Script.DOTS.Entity.Bullet
{
    public class FirePointEntity : EntityBase, IDeclareReferencedPrefabs
    {
        private GameObject prefab;
        private Vector3 _vector3;

        public void Create(GameObject go)
        {
            prefab = go;
            _vector3 = transform.position;
        }

        public override void Convert(Unity.Entities.Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new FirePointData
            {
                bulletId = 0,
                interval = 0.1f,
                time = 0.1f,
                position = _vector3,
                Prefab = conversionSystem.GetPrimaryEntity(prefab),
                range = 10,
            });
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            Debug.Log("DeclareReferencedPrefabs");
            referencedPrefabs.Add(prefab);
        }
    }
}