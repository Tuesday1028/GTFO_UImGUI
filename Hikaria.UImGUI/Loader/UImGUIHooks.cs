using Globals;
using Il2CppInterop.Runtime.Attributes;
using TheArchive.Core.Attributes;
using TheArchive.Core.FeaturesAPI;
using TheArchive.Loader;
using UnityEngine;

namespace Hikaria.UImGUI
{
    [EnableFeatureByDefault]
    [DisallowInGameToggle]
    [DoNotSaveToConfig]
    public class UImGUIHooks : Feature
    {
        public override string Name => "UImGUI Hooks";

        public override void Init()
        {
            LoaderWrapper.ClassInjector.RegisterTypeInIl2Cpp<UImGui>();
        }

        private static bool isInited = false;

        [ArchivePatch(typeof(CameraManager), nameof(CameraManager.ChangeToCamera))]
        private class CameraManager__ChangeToCamera__Patch
        {
            private static void Postfix()
            {
                if (!isInited)
                {
                    GameObject go = new GameObject("UImGUI_Holder")
                    {
                        hideFlags = HideFlags.HideAndDontSave
                    };
                    UnityEngine.Object.DontDestroyOnLoad(go);
                    go.AddComponent<UImGui>();
                    isInited = true;
                }

                UImGui.Current.SetCamera(CameraManager.GetCurrentCamera());
            }
        }

        [ArchivePatch(typeof(GlobalSetup), nameof(GlobalSetup.Awake))]
        private static class GlobalSetup__Awake__Patch
        {
            private static void Postfix()
            {
                AssetsHelper.Init();
            }
        }
    }
}
