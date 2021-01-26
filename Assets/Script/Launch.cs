using System;
using Script.Manager;
using Unity.Entities;
using UnityEngine;

namespace Script
{
    public class Launch : MonoBehaviour
    {
        [SerializeField] private GameObject[] dontDestroyOnLoads = new GameObject[0];

        private void Awake()
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                Application.targetFrameRate = 120;
            }
            for (var i = 0; i < dontDestroyOnLoads.Length; i++)
            {
                var go = dontDestroyOnLoads[i];
                DontDestroyOnLoad(go);
            }
        }

        private void Start()
        {
            StartCoroutine(LoadManager.Instance.LoadSceneAsyncByIdx(1, null, (asyncOperation) => { BattleManager.Instance.StartBattle(); }));
        }
    }
}