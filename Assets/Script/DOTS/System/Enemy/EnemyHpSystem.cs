using System.Globalization;
using Script.DOTS.Component.Enemy;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Script.DOTS.System.Enemy
{
    public class EnemyHpSystem : SystemBase
    {
        // private CompanionGameObjectUpdateTransformSystem endSimCommandBufferSystem;
        //
        // protected override void OnCreate()
        // {
        //     endSimCommandBufferSystem = World.GetOrCreateSystem<CompanionGameObjectUpdateTransformSystem>();
        // }

        protected override void OnUpdate()
        {
            var enemyDataFromEntity = GetComponentDataFromEntity<EnemyHpData>();
            Entities.WithoutBurst().ForEach((Unity.Entities.Entity entity, int entityInQueryIndex, TextMesh textMesh, in PreviousParent parent) =>
            {
                var hp = enemyDataFromEntity[parent.Value].hp;
                textMesh.text = hp.ToString(CultureInfo.InvariantCulture);
            }).Run();
        }
    }
}