using Script.DOTS.Entity.Enemy;
using UnityEngine;

namespace Script.Manager
{
    public class EnemyManager : SingletonManager<EnemyManager>
    {
        public void CreateEnemy()
        {
            LoadManager.Instance.LoadAssetBundleFromFile("prefabs/enemy", (bundle) =>
            {
                var enemy = LoadManager.Instance.LoadAssetFromAssetBundle<GameObject>(bundle, "enemy");
                GameObject go = new GameObject("enemySpawn");
                go.transform.position = Vector3.zero;
                EnemySpawnEntity comp = go.AddComponent<EnemySpawnEntity>();
                comp.Create(enemy);
            });
        }
    }
}
