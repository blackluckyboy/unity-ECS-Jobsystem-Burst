using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Script.Manager
{
    public class InputManager : SingletonManager<InputManager>
    {
        [SerializeField] private GameObject TouchUI = null;

        private Transform touchUITransform = null;
        private Transform touchItem = null;

        [SerializeField] private float radius = 230;

        public float3 inputVector = float3.zero;

        public override void Init()
        {
            if (TouchUI == null)
            {
                return;
            }

            if (touchUITransform == null)
            {
                touchUITransform = Instantiate(TouchUI).transform;
                touchUITransform.SetParent(this.transform);
                touchItem = touchUITransform.GetChild(0);
            }

            touchUITransform.gameObject.SetActive(false);
        }

        private void Update()
        {
#if UNITY_EDITOR
            MoveByEditor();
#else
            MoveByPhone();
#endif
            var localPosition = touchItem.localPosition;
            var dir = (localPosition - Vector3.zero).normalized;
            var maxPos = dir * radius;
            var dic = Vector3.Distance(localPosition, Vector3.zero);
            if (dic > radius)
            {
                touchItem.localPosition = maxPos;
            }
            inputVector = new float3(dir.x, 0, dir.y);
        }

        void MoveByEditor()
        {
            if (Input.GetMouseButtonUp(0))
            {
                touchUITransform.gameObject.SetActive(false);
                touchItem.localPosition = Vector3.zero;
            }

            if (Input.GetMouseButtonDown(0))
            {
                touchUITransform.gameObject.SetActive(true);
                touchUITransform.position = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                touchItem.position = Input.mousePosition;
            }
        }

        void MoveByPhone()
        {
            switch (Input.touchCount)
            {
                case 0:
                    touchUITransform.gameObject.SetActive(false);
                    touchItem.localPosition = Vector3.zero;
                    return;
                case 1:
                {
                    var touch = Input.touches[0];
                    if (touch.phase == TouchPhase.Began)
                    {
                        // if (!touchUITransform.gameObject.activeSelf)
                        // {
                        touchUITransform.gameObject.SetActive(true);
                        touchUITransform.position = touch.position;
                        // }
                    }

                    if (touch.phase == TouchPhase.Moved)
                    {
                        touchItem.position = touch.position;
                    }

                    return;
                }
            }
        }
    }
}