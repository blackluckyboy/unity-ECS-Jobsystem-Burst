using System;
using UnityEngine;

namespace Script.Test
{
    public class FlyTest : MonoBehaviour
    {
        private float speed = 0;
        private Action<GameObject> action;

        // Start is called before the first frame update
        void Start()
        {
        }

        public void SetData(float _speed, Action<GameObject> _action)
        {
            this.speed = _speed;
            this.action = _action;
        }


        // Update is called once per frame
        void Update()
        {
            var transform1 = transform;
            var position = transform1.position;
            transform1.position += transform1.forward * (speed * Time.deltaTime);

            if (Physics.Raycast(position, this.transform.position - position, out var hit, Vector3.Distance(transform.position, position)))
            {
                if (hit.collider.CompareTag("Plane"))
                {
                    action?.Invoke(this.gameObject);
                }
            }
        }
    }
}