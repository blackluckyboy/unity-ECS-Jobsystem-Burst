using Script.DOTS.Component.Enemy;
using Script.DOTS.Jobs.Enemy;
using Script.Manager;
using Unity.Entities;
using Unity.Transforms;

namespace Script.DOTS.System.Enemy
{
    public class EnemyRotateSystem : SystemBase
    {
        private EntityQuery _query;
        protected override void OnCreate()
        {
            _query = GetEntityQuery(typeof(EnemyRotateData), typeof(Rotation));
        }

        protected override void OnUpdate()
        {
            Dependency = new EnemyRotateJob
            {
                deltaTime = Time.DeltaTime,
                EnemyRotateDataTypeHandle = GetComponentTypeHandle<EnemyRotateData>(),
                RotationTypeHandle = GetComponentTypeHandle<Rotation>(),
                translationFromEntity = GetComponentDataFromEntity<Translation>(),
                targetEntity = PlayerManager.Instance.CameraFollowEntity,
            }.Schedule(_query,Dependency);
        }
    }
}
