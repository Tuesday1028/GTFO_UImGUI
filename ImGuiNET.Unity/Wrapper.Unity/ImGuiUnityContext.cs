using ImGuizmoNET;
using ImNodesNET;
using ImPlotNET;
using UnityEngine;

namespace ImGuiNET
{
    public interface IImGuiLayout
    {
        string GuiName { get; }
        bool ShouldGuiRender { get; }
        bool ShouldGuiBlockGameInput { get; }

        /// <summary>
        ///     Always invokes before the ImGui layout is done, even if <see cref="UImGui.ShouldRender"/> is false.
        /// </summary>
        void OnGuiUpdate();

        void OnGuiLayout();
    }

    sealed class ImGuiUnityContext
    {
        public IntPtr ImGuiContext;     // ImGui internal ImGuiContext
        public IntPtr ImNodesContext;
        public IntPtr ImPlotContext;
        public TextureManager Textures; // Texture / font ImGuiContext
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

        internal static bool ShouldBlockGameInput
        {
            get
            {
                bool result = false;
                for (int i = 0; i < _guiLayouts.Count; i++)
                {
                    try
                    {
                        result |= _guiLayouts[i].ShouldGuiBlockGameInput;
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"[ImGuiUn::GUI::{_guiLayouts[i].GuiName}] {ex}: {ex.Message}\n{ex.StackTrace}");
                    }
                }
                return result;
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
                        result |= _guiLayouts[i].ShouldGuiRender;
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"[ImGuiUn::GUI::{_guiLayouts[i].GuiName}] {ex}: {ex.Message}\n{ex.StackTrace}");
                    }
                }
                return result;
            }
        }

        public static void RegisterImGuiLayout(IImGuiLayout gui)
        {
            _guiLayouts.Add(gui);
        }

        public static void UnregisterImGuiLayout(IImGuiLayout gui)
        {
            _guiLayouts.Remove(gui);
        }

        private static readonly List<IImGuiLayout> _guiLayouts = new List<IImGuiLayout>();

        // Textures
        public static int GetTextureId(Texture texture) => s_currentUnityContext?.Textures.GetTextureId(texture) ?? -1;
        internal static SpriteInfo GetSpriteInfo(Sprite sprite) => s_currentUnityContext?.Textures.GetSpriteInfo(sprite) ?? null;

        internal static ImGuiUnityContext s_currentUnityContext;

        internal static ImGuiUnityContext CreateUnityContext()
        {
            var context = new ImGuiUnityContext();
            context.ImGuiContext = ImGui.CreateContext();
            ImGuizmo.SetImGuiContext(context.ImGuiContext);
            ImPlot.CreateContext();
            ImNodes.CreateContext();
            context.Textures = new TextureManager();
            return context;
        }

        internal static void DestroyUnityContext(ImGuiUnityContext context)
        {
            s_currentUnityContext = default;
            ImNodes.DestroyContext(context.ImNodesContext);
            ImPlot.DestroyContext(context.ImPlotContext);
            ImGuizmo.SetImGuiContext(IntPtr.Zero);
            ImGui.DestroyContext(context.ImGuiContext);
        }

        internal static void SetUnityContext(ImGuiUnityContext context)
        {
            s_currentUnityContext = context;
            ImGuizmo.SetImGuiContext(context?.ImGuiContext ?? IntPtr.Zero);
            ImNodes.SetCurrentContext(context?.ImNodesContext ?? IntPtr.Zero);
            ImPlot.SetCurrentContext(context?.ImPlotContext ?? IntPtr.Zero);
            ImGui.SetCurrentContext(context?.ImGuiContext ?? IntPtr.Zero);
        }
    }
}