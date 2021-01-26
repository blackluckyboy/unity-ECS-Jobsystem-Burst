using System;
using Script.DOTS.Entity;
using Script.DOTS.Entity.Bullet;
using UnityEngine;

namespace Script.Manager
{
    public class BulletManager : SingletonManager<BulletManager>
    {
        public void GetBulletPrefab(Action<GameObject> action)
        {
            LoadManager.Instance.LoadAssetBundleFromFile("prefabs/bullet", (bundle) =>
            {
                GameObject prefab = LoadManager.Instance.LoadAssetFromAssetBundle<GameObject>(bundle, "bullet");
                action?.Invoke(prefab);
            });
        }

        public GameObject CreateFirePoint(Transform parent, GameObject bulletPrefab, Vector3 offset)
        {
            GameObject go = new GameObject("firePoint");
            go.transform.SetParent(parent);
            go.transform.localPosition = offset;
            FirePointEntity entity = go.AddComponent<FirePointEntity>();
            entity.Create(bulletPrefab);
            return go;
        }
    }
}