using Script.DOTS.Component;
using Script.DOTS.Component.Player;
using Script.DOTS.Jobs;
using Script.DOTS.Jobs.Player;
// using Script.DOTS.Jobs.Player;
using Script.Manager;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Script.DOTS.System.Player
{
    [BurstCompile]
    public class PlayerMoveSystem : SystemBase
    {
        private EntityQuery _query;

        protected override void OnCreate()
        {
            _query = GetEntityQuery(typeof(PlayerMoveData), typeof(Translation), typeof(PlayerMoveBufferData));
        }


        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;
            var input = InputManager.Instance.inputVector;
            var spacing = PlayerManager.Instance.spacing;
            if (input.x != 0 || input.y != 0 || input.z != 0)
            {
                PlayerMoveJob job = new PlayerMoveJob
                {
                    TranslationTypeHandle = GetComponentTypeHandle<Translation>(),
                    PlayerMoveDataTypeHandle = GetComponentTypeHandle<PlayerMoveData>(),
                    PlayerMoveBufferDataTypeHandle = GetBufferTypeHandle<PlayerMoveBufferData>(),
                    deltaTime = deltaTime,
                    spacing = spacing,
                    input = input,
                };
                Dependency = job.Schedule(_query, Dependency);
            }
        }
    }
}