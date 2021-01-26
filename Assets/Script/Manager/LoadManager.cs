using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using AsyncOperation = UnityEngine.AsyncOperation;
using Object = UnityEngine.Object;

namespace Script.Manager
{
    public class LoadManager : SingletonManager<LoadManager>
    {
        private string uri = "http://127.0.0.1/AssetBundles/";

        private Dictionary<string, AssetBundle> loadedAssetBundle;//assetBundleName,AssetBundle

        public string AssetCachesDir
        {
            get
            {
                string dir = "";
#if UNITY_EDITOR
                dir = Application.dataPath + "Caches/"; //路径：/AssetsCaches/
#elif UNITY_IOS
            dir = Application.temporaryCachePath + "/Download/";//路径：Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/Library/Caches/
#elif UNITY_ANDROID
            dir = Application.persistentDataPath + "/Download/";//路径：/data/data/xxx.xxx.xxx/files/
#else
            dir = Application.streamingAssetsPath + "/Download/";//路径：/xxx_Data/StreamingAssets/
#endif
                return dir;
            }
        }

        public string LocalFileDir => Path.Combine(Application.streamingAssetsPath, "AssetBundles");


        public override void Init()
        {
            loadedAssetBundle = new Dictionary<string, AssetBundle>();
        }
        
        /// <summary>
        /// 协程加载assetbundle从网络上加载
        /// </summary>
        /// <param name="assetBundleName">assetBundle名字 </param>
        /// <param name="callBack">拿到物体回调，callBack会有参数 就是这个物体</param>
        /// <returns></returns>
        public IEnumerator LoadAssetBundleFromNet(string assetBundleName, Action<AssetBundle> callBack)
        {
            bool isContains = loadedAssetBundle.ContainsKey(assetBundleName);
            if (isContains)
            {
                AssetBundle bundle = loadedAssetBundle[assetBundleName];
                callBack?.Invoke(bundle);
            }
            else
            {
                string url = uri + assetBundleName;
                if (!Directory.Exists(AssetCachesDir))
                {
                    Directory.CreateDirectory(AssetCachesDir);
                }

                Caching.currentCacheForWriting = Caching.AddCache(AssetCachesDir);
                UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url, 0, 0);
                yield return request.SendWebRequest();
                AssetBundle assetBundle = DownloadHandlerAssetBundle.GetContent(request);
                loadedAssetBundle[assetBundleName] = assetBundle;
                callBack?.Invoke(assetBundle);
            }
        }
        
        /// <summary>
        /// 加载assetbundle从本地文件加载
        /// </summary>
        /// <param name="assetBundleName">assetBundle名字</param>
        /// <param name="callBack">拿到物体回调，callBack会有参数 就是这个物体</param>
        public void LoadAssetBundleFromFile(string assetBundleName, Action<AssetBundle> callBack)
        {
            bool isContains = loadedAssetBundle.ContainsKey(assetBundleName);
            if (isContains)
            {
                //Debug.Log("Loaded" + assetBundleName);
                AssetBundle bundle = loadedAssetBundle[assetBundleName];
                if (bundle)
                {
                    callBack?.Invoke(bundle);
                }
            }
            else
            {
                AssetBundle bundle = AssetBundle.LoadFromFile(Path.Combine(LocalFileDir, assetBundleName));
                if (bundle)
                {
                    //Debug.Log("LoadAssetBundleFromFile" + assetBundleName);
                    loadedAssetBundle[assetBundleName] = bundle;
                    callBack?.Invoke(bundle);
                }
            }
        }

        public T LoadAssetFromAssetBundle<T>(AssetBundle assetBundle,string assetName) where T : Object
        {
           T asset = assetBundle.LoadAsset<T>(assetName);
           return asset ? asset : null;
        }

        /// <summary>
        /// 卸载掉已加载的assetbundle
        /// </summary>
        /// <param name="assetBundleName">需要卸载的名字</param>
        public void UnLoadAssetBundleByName(string assetBundleName)
        {
            assetBundleName = assetBundleName.ToLower();
            bool isContain = loadedAssetBundle.ContainsKey(assetBundleName);
            if (isContain)
            {
                AssetBundle bundle = loadedAssetBundle[assetBundleName];
                bundle.Unload(true);
                //Debug.Log("unLoad" + assetBundleName);
                loadedAssetBundle.Remove(assetBundleName);
            }
            else
            {
                //Debug.LogWarning("没加载,就释放？" + assetBundleName);
            }
        }

        /// <summary>
        /// 协程加载场景
        /// </summary>
        /// <param name="idx">场景索引，在buildsetting中设置的</param>
        /// <param name="updateProgressAction">加载的时候会有一个百分比，通过这个委托传出来</param>
        /// <param name="completeAction">加载完成之后需要做什么事情通过这个委托传进去</param>
        /// <returns></returns>
        public IEnumerator LoadSceneAsyncByIdx(int idx, Action<float> updateProgressAction, Action<AsyncOperation> completeAction)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(idx);
            operation.allowSceneActivation = true;
            operation.completed += completeAction;
            while (!operation.isDone)
            {
                updateProgressAction?.Invoke(operation.progress);
                yield return null;
            }

            //Debug.Log("加载完成");
        }

        // /// <summary>
        // /// 随便封装一层系统的实例化，万一以后有用呢
        // /// </summary>
        // /// <param name="obj">需要实例化的预制体，脚本之类的</param>
        // /// <returns></returns>
        public Object InstantiateObj(Object obj)
        {
            Object _instantiate = Instantiate(obj);
            _instantiate.name = obj.name;
            return _instantiate;
        }
        
        public T InstantiateObj<T>(T obj) where T : Object
        {
            T _instantiate = Instantiate(obj);
            _instantiate.name = obj.name;
            return _instantiate;
        }

        /// <summary>
        /// 异步加载一个assetbundle。。。
        /// </summary>
        /// <param name="assetBundleName">需要加载的assetbundle</param>
        /// <param name="updateProgressAction">加载的时候会有一个百分比，通过这个委托传出来</param>
        /// <param name="completeAction">加载完成之后需要做什么事情通过这个委托传进去</param>
        /// <returns></returns>
        public IEnumerator LoadAssetBundleFromFileAsync(string assetBundleName, Action<float> updateProgressAction, Action<AsyncOperation> completeAction)
        {
            bool isContains = loadedAssetBundle.ContainsKey(assetBundleName);
            if (!isContains)
            {
                AssetBundleCreateRequest bundleCreateRequest = AssetBundle.LoadFromFileAsync(Path.Combine(LocalFileDir, assetBundleName));
                bundleCreateRequest.completed += completeAction;
                while (!bundleCreateRequest.isDone)
                {
                    updateProgressAction?.Invoke(bundleCreateRequest.progress);
                    //Debug.Log(bundleCreateRequest.progress);
                    yield return null;
                }

                if (bundleCreateRequest.assetBundle)
                {
                    loadedAssetBundle[assetBundleName] = bundleCreateRequest.assetBundle;
                }

                //Debug.Log("异步加载完成" + assetBundleName);
            }
        }

        /// <summary>
        /// 异步加载一堆assetbundle
        /// </summary>
        /// <param name="assetBundleNames">加载队列</param>
        /// <param name="updateProgressAction">加载的时候会有一个百分比，通过这个委托传出来</param>
        /// <param name="completeAction">加载完成之后需要做什么事情通过这个委托传进去</param>
        public void LoadAssetBundleArrFromFileAsync(string[] assetBundleNames, Action<float> updateProgressAction, Action completeAction)
        {
            float nowProgress = 0.0f;
            List<string> completedName = new List<string>();
            foreach (string bundleName in assetBundleNames)
            {
                StartCoroutine(LoadAssetBundleFromFileAsync(bundleName, (progress) =>
                {
                    nowProgress += progress;
                    updateProgressAction?.Invoke(nowProgress);
                }, (async) =>
                {
                    completedName.Add(bundleName);
                    if (completedName.Count >= assetBundleNames.Length)
                    {
                        completeAction?.Invoke();
                    }
                }));
            }
        }

        public AssetBundle GetLoadedAssetBundle(string assetName)
        {
            loadedAssetBundle.TryGetValue(assetName, out var b);
            return b;
        }

    
    }
}