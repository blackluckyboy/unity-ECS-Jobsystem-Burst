using Script.DOTS.Entity;
using Script.DOTS.Entity.Camera;
using Script.DOTS.Entity.Player;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Script.Manager
{
    public class PlayerManager : SingletonManager<PlayerManager>
    {
        public Entity FollowEntity = Entity.Null; //创建entity的时候，每个entity都会记录一下他的上一个entity，第一个entity的上一个为entity.null。
        public float spacing = 1f;//两个entity之间的间距。
        public Entity CameraFollowEntity = Entity.Null;
        public override void Init()
        {
            FollowEntity = Entity.Null;
            spacing = 1f;
            CameraFollowEntity = Entity.Null;
        }

        public void CreatePlayer()
        {
            LoadManager.Instance.LoadAssetBundleFromFile("prefabs/player", (bundle) =>
            {
                for (int i = 0; i < 10; i++)
                {
                    GameObject go = LoadManager.Instance.LoadAssetFromAssetBundle<GameObject>(bundle, "player");
                    GameObject player = LoadManager.Instance.InstantiateObj<GameObject>(go);
                    player.transform.position = Vector3.up + Vector3.back * i;
                    PlayerEntity comp = player.AddComponent<PlayerEntity>();
                    comp.Create(10, i);
                    BulletManager.Instance.GetBulletPrefab((bullet) =>
                    {
                        BulletManager.Instance.CreateFirePoint(player.transform, bullet, Vector3.zero);
                    });
                }
            });
        }
    }
}