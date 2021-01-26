using UnityEngine;
/*
 * 推荐使用这种，直接挂载于empty物件上的方式。这样，如果有需要一些prefab时，可以拉至脚本中。
 * 不推荐使用当instance=null时new Gamaobject然后AddComponent的方式。
 */
namespace Script.Manager
{
    public abstract class SingletonManager<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError(typeof(T).ToString() + " is NULL.");
                return _instance;
            }
        }
        private void Awake()
        {
            if (_instance != null)
            {
                //one more Initialize
                Debug.LogError(typeof(T).ToString() + " aready have one on" + _instance.gameObject.name);
            }

            _instance = this as T; //or  _instance = (T)this; 
            //call Init
            Init();
        }

        /// <summary>
        /// 需要在awake初始化的请重载Init
        /// </summary>
        public virtual void Init()
        {
            //optional to override
        }
    }
}