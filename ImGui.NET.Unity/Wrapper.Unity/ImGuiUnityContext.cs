using ImGuizmoNET;
using ImNodesNET;
using ImPlotNET;
using UnityEngine;

namespace ImGuiNET
{
    sealed class ImGuiUnityContext
    {
        public IntPtr ImGuiContext;     // ImGui internal ImGuiContext
        public ImNodesContextPtr ImNodesContext;
        public IntPtr ImPlotContext;
        public TextureManager Textures; // Texture / font ImGuiContext
    }

    public static unsafe partial class ImGuiUn
    {
#if false
        static ImGuiUn()
        {
            RegisterImGuiLayout(new TestUI());
        }

        class TestUI : IImGuiLayout
        {
            public string GuiName => "TestUI";

            public bool ShouldGuiRender => true;

            public bool ShouldGuiBlockGameInput => true;

            float[] x = new float[100];
            float[] y = new float[100];

            public void OnGuiLayout()
            {
                ImGui.Begin("ImPlot Example");
                if (ImPlot.BeginPlot("My Plot"))
                {
                    for (int i = 0; i < 100; ++i)
                    {
                        x[i] = i * 0.01f;
                        y[i] = Mathf.Sin(x[i]);
                    }
                    ImPlot.PlotLine("sin(x)", ref x[0], ref y[0], 100);
                    ImPlot.EndPlot();
                }
                ImGui.End();
            }

            public void OnGuiUpdate()
            {
            }
        }
#endif

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
            context.ImPlotContext = ImPlot.CreateContext();
            context.ImNodesContext = ImNodes.CreateContext();

            ImGuizmo.SetImGuiContext(context.ImGuiContext);
            ImPlot.SetImGuiContext(context.ImGuiContext);
            ImNodes.SetImGuiContext(context.ImGuiContext);

            context.Textures = new TextureManager();
            return context;
        }

        internal static void DestroyUnityContext(ImGuiUnityContext context)
        {
            s_currentUnityContext = default;
            ImNodes.DestroyContext(context.ImNodesContext);
            ImPlot.DestroyContext(context.ImPlotContext);
            ImGui.DestroyContext(context.ImGuiContext);
        }

        internal static void SetUnityContext(ImGuiUnityContext context)
        {
            s_currentUnityContext = context;
            ImGui.SetCurrentContext(context?.ImGuiContext ?? IntPtr.Zero);
            ImNodes.SetCurrentContext(context?.ImNodesContext ?? IntPtr.Zero);
            ImPlot.SetCurrentContext(context?.ImPlotContext ?? IntPtr.Zero);

            ImGuizmo.SetImGuiContext(context?.ImGuiContext ?? IntPtr.Zero);
            ImPlot.SetImGuiContext(context?.ImGuiContext ?? IntPtr.Zero);
            ImNodes.SetImGuiContext(context?.ImGuiContext ?? IntPtr.Zero);
        }
    }
}