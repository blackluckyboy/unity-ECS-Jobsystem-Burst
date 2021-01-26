using Script.DOTS.Entity;
using Script.DOTS.Entity.Camera;
using Unity.Mathematics;
using UnityEngine;

namespace Script.Manager
{
    public class BattleManager : SingletonManager<BattleManager>
    {
        public void StartBattle()
        {
            // BulletManager.Instance.GetBulletPrefab((bullet) =>
            // {
            //     BulletManager.Instance.CreateFirePoint(this.transform, bullet, Vector3.up);
            // });
            EnemyManager.Instance.CreateEnemy();
            PlayerManager.Instance.CreatePlayer();
            Camera.main.gameObject.AddComponent<CameraEntity>().Create
            (
                new float3(0,15,-8),
                quaternion.AxisAngle(
                    new float3(1,0,0),
                    math.radians(60)
                    )
                );
        }

        // Update is called once per frame
        // void Update()
        // {
        // }
    }
}