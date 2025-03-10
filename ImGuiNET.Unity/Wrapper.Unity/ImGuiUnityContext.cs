using UnityEngine;

namespace ImGuiNET
{
    public interface IImGuiLayout
    {
        string GuiName { get; }
        bool ShouldRender { get; }

        /// <summary>
        ///     Always invokes before the ImGui layout is done, even if <see cref="UImGui.ShouldRender"/> is false.
        /// </summary>
        void OnGuiUpdate();

        void OnGuiLayout();
    }

    sealed class ImGuiUnityContext
    {
        public IntPtr imGuiContext;     // ImGui internal imGuiContext
        public IntPtr imNodesContext;
        public IntPtr imPlotContext;
        public TextureManager textures; // texture / font imGuiContext
    }

    public static unsafe partial class ImGuiUn
    {
        internal static void OnImGuiLayout()
        {
            for (int i = 0; i < _guiLayouts.Count; i++)
            {
                try
                {
                    _guiLayouts[i].OnGuiLayout();
                }
                catch (Exception ex)
                {
                    Debug.LogError($"[ImGuiUn::GUI::{_guiLayouts[i].GuiName}] {ex}: {ex.Message}\n{ex.StackTrace}");
                }
            }
        }

        internal static void OnImGuiUpdate()
        {
            for (int i = 0; i < _guiLayouts.Count; i++)
            {
                try
                {
                    _guiLayouts[i].OnGuiUpdate();
                }
                catch (Exception ex)
                {
                    Debug.LogError($"[ImGuiUn::GUI::{_guiLayouts[i].GuiName}] {ex}: {ex.Message}\n{ex.StackTrace}");
                }
            }
        }

        internal static bool ShouldRender
        {
            get
            {
                bool result = false;
                for (int i = 0; i < _guiLayouts.Count; i++)
                {
                    try
                    {
                        result |= _guiLayouts[i].ShouldRender;
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"[ImGuiUn::GUI::{_guiLayouts[i].GuiName}] {ex}: {ex.Message}\n{ex.StackTrace}");
                    }
                }
                return result;
            }
        }

        public static void RegisterImGui(IImGuiLayout gui)
        {
            _guiLayouts.Add(gui);
        }

        public static void UnregisterImGui(IImGuiLayout gui)
        {
            _guiLayouts.Remove(gui);
        }

        private static readonly List<IImGuiLayout> _guiLayouts = new List<IImGuiLayout>();

        // textures
        public static int GetTextureId(Texture texture) => s_currentUnityContext?.textures.GetTextureId(texture) ?? -1;
        internal static SpriteInfo GetSpriteInfo(Sprite sprite) => s_currentUnityContext?.textures.GetSpriteInfo(sprite) ?? null;

        internal static ImGuiUnityContext s_currentUnityContext;

        internal static ImGuiUnityContext CreateUnityContext()
        {
            return new ImGuiUnityContext 
            {
                imGuiContext = ImGui.CreateContext(),
                textures = new TextureManager(),
            };
        }

        internal static void DestroyUnityContext(ImGuiUnityContext context)
        {
            s_currentUnityContext = default;
            ImGui.DestroyContext(context.imGuiContext);
        }

        internal static void SetUnityContext(ImGuiUnityContext context)
        {
            s_currentUnityContext = context;
            ImGui.SetCurrentContext(context?.imGuiContext ?? IntPtr.Zero);
        }
    }
}