using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class ExportAssetBundles : MonoBehaviour
    {
        [@MenuItem("ExportTools/Build Asset Bundles _%#_a")]
        static void BuildAssetBundles()
        {
            //Debug.Log("AssetBundles导出开始");
            string dir = Path.Combine(Application.streamingAssetsPath,"AssetBundles") ;
            if(Directory.Exists(dir) == false)
            {
                Directory.CreateDirectory(dir); 
            }
            BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.iOS);
        }
    }
}