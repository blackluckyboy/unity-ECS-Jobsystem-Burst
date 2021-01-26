using System.Collections.Generic;
using UnityEngine;

namespace Script.Test
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private GameObject go = null;

        private Queue<GameObject> pool = new Queue<GameObject>();

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < 100; i++)
            {
                GameObject g;
                if (pool.Count > 0)
                {
                    g = pool.Dequeue();
                }
                else
                {
                    g = Instantiate(go);
                    var fly = g.AddComponent<FlyTest>();
                    fly.SetData(10, Remove);
                }

                g.SetActive(true);
                g.transform.position = Vector3.up + Vector3.right * i;
            }
        }

        public void Remove(GameObject go)
        {
            go.SetActive(false);
            pool.Enqueue(go);
        }
    }
}