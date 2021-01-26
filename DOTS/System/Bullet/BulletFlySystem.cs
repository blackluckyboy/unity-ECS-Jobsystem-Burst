using Script.DOTS.Component;
using Script.DOTS.Component.Bullet;
using Script.DOTS.Jobs;
using Script.DOTS.Jobs.Bullet;
using Unity.Entities;
using Unity.Transforms;

namespace Script.DOTS.System.Bullet
{
    public class BulletFlySystem : SystemBase
    {
        private EntityQuery _query;

        protected override void OnCreate()
        {
            _query = GetEntityQuery(typeof(Translation), typeof(DeltaTranslationData), ComponentType.ReadOnly<BulletFlyData>());
        }

        protected override void OnUpdate()
        {
            BulletFlyJob job = new BulletFlyJob
            {
                bulletFlyDataHandle = GetComponentTypeHandle<BulletFlyData>(true),
                translationHandle = GetComponentTypeHandle<Translation>(),
                deltaTransDataHandle = GetComponentTypeHandle<DeltaTranslationData>(),
                rotationHandle = GetComponentTypeHandle<Rotation>(),
                deltaTime = Time.DeltaTime,
            };
            Dependency = job.Schedule(_query, Dependency);
        }
    }
}