using Script.DOTS.Component;
using Script.DOTS.Component.Player;
using Script.Manager;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Script.DOTS.Jobs.Player
{
    [BurstCompile]
    public struct PlayerMoveJob : IJobChunk
    {
        public ComponentTypeHandle<PlayerMoveData> PlayerMoveDataTypeHandle;
        public ComponentTypeHandle<Translation> TranslationTypeHandle;
        public BufferTypeHandle<PlayerMoveBufferData> PlayerMoveBufferDataTypeHandle;
        public float deltaTime;
        public float spacing;
        public float3 input;

        public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
        {
            var PlayerMoveDates = chunk.GetNativeArray(PlayerMoveDataTypeHandle);
            var Translations = chunk.GetNativeArray(TranslationTypeHandle);
            var PlayerMoveBufferDates = chunk.GetBufferAccessor(PlayerMoveBufferDataTypeHandle);
            for (int i = 0; i < chunk.Count; i++)
            {
                var playerMoveData = PlayerMoveDates[i];
                var translation = Translations[i];
                float3 newPos;
                if (i == 0)
                {
                    newPos = translation.Value + input * playerMoveData.moveSpeed * deltaTime;
                }
                else
                {
                    DynamicBuffer<PlayerMoveBufferData> preMoveBuffer = PlayerMoveBufferDates[i - 1];
                    Translation preTranslation = Translations[i - 1];

                    var dir = preMoveBuffer[preMoveBuffer.Length - 1].Value - translation.Value;
                    newPos = translation.Value + dir * playerMoveData.moveSpeed * deltaTime;
                    
                    var disSum = 0f;
                    var preTranslationValue = preTranslation.Value;
                    for (int j = 0; j < preMoveBuffer.Length; j++)
                    {
                        var pos = preMoveBuffer[preMoveBuffer.Length - j - 1];
                        var dis = math.distance(preTranslationValue, pos.Value);
                        disSum += dis;
                        preTranslationValue = pos.Value;
                        if (disSum >= spacing)
                        {
                            newPos = pos.Value;
                            preMoveBuffer.RemoveRange(0, preMoveBuffer.Length - j - 1);
                            break;
                        }
                    }
                }

                Translations[i] = new Translation
                {
                    Value = newPos,
                };
                PlayerMoveBufferDates[i].Add(new PlayerMoveBufferData
                {
                    Value = Translations[i].Value,
                });
            }
        }
    }
}