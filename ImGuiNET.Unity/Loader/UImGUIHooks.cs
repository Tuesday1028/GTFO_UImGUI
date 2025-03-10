using Globals;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using ImGuiNET;
using LevelGeneration;
using UnityEngine;

namespace Hikaria.UImGUI
{
    [HarmonyPatch]
    internal static class UImGUIHooks
    {
        public static void Init()
        {
            ClassInjector.RegisterTypeInIl2Cpp<UImGui>();
            ClassInjector.RegisterTypeInIl2Cpp<ImGuiScreenSpaceCanvas>();
        }

        // 使用 ImGuiScreenSpaceCanvas 则不需要设置相机
        //[HarmonyPatch(typeof(CameraManager), nameof(CameraManager.ChangeToCamera))]
        //private class CameraManager__ChangeToCamera__Patch
        //{
        //    private static void Postfix()
        //    {
        //        UImGui.Instance.SetCamera(CameraManager.GetCurrentCamera());
        //    }
        //}

        [HarmonyPatch(typeof(GlobalSetup), nameof(GlobalSetup.Awake))]
        private static class GlobalSetup__Awake__Patch
        {
            private static void Postfix()
            {
                GameObject go = new GameObject("UImGUI_Holder")
                {
                    hideFlags = HideFlags.HideAndDontSave
                };
                UnityEngine.Object.DontDestroyOnLoad(go);
                go.AddComponent<UImGui>();
            }
        }

        [HarmonyPatch(typeof(PlayerChatManager), nameof(PlayerChatManager.UpdateTextChatInput))]
        private class PlayerChatManager__UpdateTextChatInput__Patch
        {
            private static bool Prefix()
            {
                if (UImGui.ShouldRender)
                {
                    PlayerChatManager.ExitChatIfInChatMode();
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(LG_TERM_PlayerInteracting), nameof(LG_TERM_PlayerInteracting.ParseInput))]
        private class LG_TERM_PlayerInteracting__ParseInput__Patch
        {
            private static bool Prefix()
            {
                if (UImGui.ShouldRender)
                {
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(Cursor), nameof(Cursor.visible))]
        [HarmonyPatch(MethodType.Setter)]
        private class Cursor__set_visible__Patch
        {
            private static void Prefix(ref bool value)
            {
                if (UImGui.ShouldRender)
                {
                    value = true;
                }
            }
        }

        [HarmonyPatch(typeof(Cursor), nameof(Cursor.lockState))]
        [HarmonyPatch(MethodType.Setter)]
        private class Cursor__set_lockState__Patch
        {
            private static void Prefix(ref CursorLockMode value)
            {
                if (UImGui.ShouldRender)
                {
                    value = CursorLockMode.None;
                }
            }
        }

        [HarmonyPatch(typeof(InputMapper), nameof(InputMapper.DoGetAxis))]
        private class InputMapper__DoGetAxis__Patch
        {
            private static bool Prefix(ref float __result)
            {
                if (Cursor.lockState == CursorLockMode.None || UImGui.ShouldRender)
                {
                    __result = 0f;
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(InputMapper), nameof(InputMapper.DoGetButton))]
        private class InputMapper__DoGetButton__Patch
        {
            private static bool Prefix(ref bool __result)
            {
                if (Cursor.lockState == CursorLockMode.None || UImGui.ShouldRender)
                {
                    __result = false;
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(InputMapper), nameof(InputMapper.DoGetButtonDown))]
        private class InputMapper__DoGetButtonDown__Patch
        {
            private static bool Prefix(ref bool __result)
            {
                if (Cursor.lockState == CursorLockMode.None || UImGui.ShouldRender)
                {
                    __result = false;
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(InputMapper), nameof(InputMapper.DoGetButtonUp))]
        private class InputMapper__DoGetButtonUp__Patch
        {
            private static bool Prefix(ref bool __result)
            {
                if (Cursor.lockState == CursorLockMode.None || UImGui.ShouldRender)
                {
                    __result = false;
                    return false;
                }
                return true;
            }
        }
    }
}
