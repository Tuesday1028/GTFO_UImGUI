using System.Reflection;
using UnityEngine;

namespace Hikaria.UImGUI
{
    internal static class AssetsHelper
    {
        static AssetsHelper()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets/uimgui");
            AssetBundle assetBundle = AssetBundle.LoadFromFile(path);
            string[] array = assetBundle.AllAssetNames();
            foreach (string text in array)
            {
                UnityEngine.Object obj = assetBundle.LoadAsset(text);
                if (obj != null)
                {
                    s_AssetsLookup.Add(text.ToUpperInvariant(), obj);
                }
            }
        }

        private static readonly Dictionary<string, UnityEngine.Object> s_AssetsLookup = new();

        public static bool TryGetAssets<T>(string path, out T asset, bool instantiate = true) where T : UnityEngine.Object
        {
            path = path.ToUpperInvariant();
            asset = null;
            if (!s_AssetsLookup.TryGetValue(path, out var obj))
                return false;
            asset = instantiate ? UnityEngine.Object.Instantiate(obj.Cast<T>()) : obj.Cast<T>();
            return true;
        }

        public static T GetAssets<T>(string path, bool instantiate = true) where T : UnityEngine.Object
        {
            path = path.ToUpperInvariant();
            if (!s_AssetsLookup.TryGetValue(path, out var obj))
                return null;
            if (!instantiate)
                return obj.Cast<T>();
            return UnityEngine.Object.Instantiate(obj.Cast<T>());
        }
    }
}
