using Script.DOTS.Component.Enemy;
using Script.DOTS.Jobs.Enemy;
using Unity.Entities;
using Unity.Transforms;

namespace Script.DOTS.System.Enemy
{
    public class EnemyMoveSystem : SystemBase
    {
        private EntityQuery _query;
        protected override void OnCreate()
        {
            _query = GetEntityQuery(typeof(EnemyMoveData), typeof(Translation));
        }

        protected override void OnUpdate()
        {
            Dependency = new EnemyMoveJob
            {
                deltaTime = Time.DeltaTime,
                EnemyMoveDataTypeHandle = GetComponentTypeHandle<EnemyMoveData>(),
                TranslationTypeHandle = GetComponentTypeHandle<Translation>(),
                RotationTypeHandle = GetComponentTypeHandle<Rotation>()
            }.Schedule(_query, Dependency);
        }
    }
}
