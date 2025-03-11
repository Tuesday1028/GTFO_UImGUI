using System.ComponentModel;
using UnityEngine;

namespace ImGuiNET
{
    sealed class StyleAsset
    {
        [Description("Global alpha applies to everything in ImGui.")]
        public float Alpha = 1f;

        [Description("Additional alpha multiplier applied by BeginDisabled(). Multiply over current value of Alpha.")]
        public float DisabledAlpha = 0.6f;

        [Description("Padding within a window.")]
        public Vector2 WindowPadding = new Vector2(10f, 10f);

        [Description("Radius of window corners rounding. Set to 0.0f to have rectangular windows. Large values tend to lead to variety of artifacts and are not recommended.")]
        public float WindowRounding = 0f;

        [Description("Thickness of border around windows. Generally set to 0.0f or 1.0f. (Other values are not well tested and more CPU/GPU costly).")]
        public float WindowBorderSize = 1f;

        [Description("Minimum window size. This is a global setting. If you want to constraint individual windows, use SetNextWindowSizeConstraints().")]
        public Vector2 WindowMinSize = new Vector2(32f, 32f);

        [Description("Alignment for title bar text. Defaults to (0.0f,0.5f) for left-aligned,vertically centered.")]
        public Vector2 WindowTitleAlign = new Vector2(0.5f, 0.5f);

        [Description("Side of the collapsing/docking button in the title bar (left/right). Defaults to ImGuiDir_Left.")]
        public ImGuiDir WindowMenuButtonPosition = ImGuiDir.Left;

        [Description("Radius of child window corners rounding. Set to 0.0f to have rectangular windows.")]
        public float ChildRounding = 2f;

        [Description("Thickness of border around child windows. Generally set to 0.0f or 1.0f. (Other values are not well tested and more CPU/GPU costly).")]
        public float ChildBorderSize = 0f;

        [Description("Radius of popup window corners rounding. (Note that tooltip windows use WindowRounding)")]
        public float PopupRounding = 0f;

        [Description("Thickness of border around popup/tooltip windows. Generally set to 0.0f or 1.0f. (Other values are not well tested and more CPU/GPU costly).")]
        public float PopupBorderSize = 1f;

        [Description("Padding within a framed rectangle (used by most widgets).")]
        public Vector2 FramePadding = new Vector2(8f, 4f);

        [Description("Radius of frame corners rounding. Set to 0.0f to have rectangular frame (used by most widgets).")]
        public float FrameRounding = 3f;

        [Description("Thickness of border around frames. Generally set to 0.0f or 1.0f. (Other values are not well tested and more CPU/GPU costly).")]
        public float FrameBorderSize = 0f;

        [Description("Horizontal and vertical spacing between widgets/lines.")]
        public Vector2 ItemSpacing = new Vector2(10f, 8f);

        [Description("Horizontal and vertical spacing between within elements of a composed widget (e.g. a slider and its label).")]
        public Vector2 ItemInnerSpacing = new Vector2(6f, 6f);

        [Description("Padding within a table cell. Cellpadding.x is locked for entire table. CellPadding.y may be altered between different rows.")]
        public Vector2 CellPadding = new Vector2(4f, 2f);

        [Description("Expand reactive bounding box for touch-based system where touch position is not accurate enough. Unfortunately we don't sort widgets so priority on overlap will always be given to the first widget. So don't grow this too much!")]
        public Vector2 TouchExtraPadding = new Vector2(0f, 0f);

        [Description("Horizontal indentation when e.g. entering a tree node. Generally == (FontSize + FramePadding.x*2).")]
        public float IndentSpacing = 21f;

        [Description("Minimum horizontal spacing between two columns. Preferably > (FramePadding.x + 1).")]
        public float ColumnsMinSpacing = 6f;

        [Description("Width of the vertical scrollbar, Height of the horizontal scrollbar.")]
        public float ScrollbarSize = 15f;

        [Description("Radius of grab corners for scrollbar.")]
        public float ScrollbarRounding = 3f;

        [Description("Minimum width/height of a grab box for slider/scrollbar.")]
        public float GrabMinSize = 8f;

        [Description("Radius of grabs corners rounding. Set to 0.0f to have rectangular slider grabs.")]
        public float GrabRounding = 0f;

        [Description("The size in pixels of the dead-zone around zero on logarithmic sliders that cross zero.")]
        public float LogSliderDeadzone = 4f;

        [Description("Radius of upper corners of a tab. Set to 0.0f to have rectangular tabs.")]
        public float TabRounding = 4f;

        [Description("Thickness of border around tabs.")]
        public float TabBorderSize = 0f;

        [Description("Minimum width for close button to appear on an unselected tab when hovered. Set to 0.0f to always show when hovering, set to FLT_MAX to never show close button unless selected.")]
        public float TabMinWidthForCloseButton = 0f;

        [Description("Thickness of tab-bar separator, which takes on the tab active color to denote focus.")]
        public float TabBarBorderSize = 1f;

        [Description("Thickness of tab-bar overline, which highlights the selected tab-bar.")]
        public float TabBarOverlineSize = 2f;

        [Description("Angle of angled headers (supported values range from -50.0f degrees to +50.0f degrees).")]
        public float TableAngledHeadersAngle = (float)(35 * (Math.PI / 180d));

        [Description("Alignment of angled headers within the cell.")]
        public Vector2 TableAngledHeadersTextAlign = new Vector2(0.5f, 0.5f);

        [Description("Side of the color button in the ColorEdit4 widget (left/right). Defaults to ImGuiDir_Right.")]
        public ImGuiDir ColorButtonPosition = ImGuiDir.Right;

        [Description("Alignment of button text when button is larger than text. Defaults to (0.5f, 0.5f) (centered).")]
        public Vector2 ButtonTextAlign = new Vector2(0.5f, 0.5f);

        [Description("Alignment of selectable text when selectable is larger than text. Defaults to (0.0f, 0.0f) (top-left aligned).")]
        public Vector2 SelectableTextAlign = new Vector2(0f, 0f);

        [Description("Thickness of border in SeparatorText().")]
        public float SeparatorTextBorderSize = 3f;

        [Description("Alignment of text within the separator. Defaults to (0.0f, 0.5f) (left aligned, center).")]
        public Vector2 SeparatorTextAlign = new Vector2(0f, 0.5f);

        [Description("Horizontal offset of text from each edge of the separator + spacing on other axis. Generally small values. .y is recommended to be == FramePadding.y.")]
        public Vector2 SeparatorTextPadding = new Vector2(20f, 4f);

        [Description("Window position are clamped to be visible within the display area by at least this amount. Only applies to regular windows.")]
        public Vector2 DisplayWindowPadding = new Vector2(19f, 19f);

        [Description("If you cannot see the edges of your screen (e.g. on a TV) increase the safe area padding. Apply to popups/tooltips as well regular windows. NB: Prefer configuring your TV sets correctly!")]
        public Vector2 DisplaySafeAreaPadding = new Vector2(3f, 3f);

        [Description("Scale software rendered mouse cursor (when io.MouseDrawCursor is enabled). May be removed later.")]
        public float MouseCursorScale = 1f;

        [Description("Enable anti-aliased lines/borders. Disable if you are really tight on CPU/GPU. Latched at the beginning of the frame (copied to ImDrawList).")]
        public bool AntiAliasedLines = true;

        [Description("Enable anti-aliased lines/borders using textures where possible. Require backend to render with bilinear filtering (NOT point/nearest filtering). Latched at the beginning of the frame (copied to ImDrawList).")]
        public bool AntiAliasedLinesUseTex = true;

        [Description("Enable anti-aliased edges around filled shapes (rounded rectangles, circles, etc.). Disable if you are really tight on CPU/GPU. Latched at the beginning of the frame (copied to ImDrawList).")]
        public bool AntiAliasedFill = true;

        [Description("Tessellation tolerance when using PathBezierCurveTo() without a specific number of segments. Decrease for highly tessellated curves (higher quality, more polygons), increase to reduce quality.")]
        public float CurveTessellationTol = 1.25f;

        [Description("Maximum error (in pixels) allowed when using AddCircle()/AddCircleFilled() or drawing rounded corner rectangles with no explicit segment count specified. Decrease for higher quality but more geometry.")]
        public float CircleTessellationMaxError = 0.3f;

        public Color[] Colors = new Color[(int)ImGuiCol.COUNT];

        public StyleAsset()
        {
            Colors[(int)ImGuiCol.Text] = new(1.00f, 1.00f, 1.00f, 1.00f);
            Colors[(int)ImGuiCol.TextDisabled] = new(0.50f, 0.50f, 0.50f, 1.00f);
            Colors[(int)ImGuiCol.WindowBg] = new(0.06f, 0.06f, 0.06f, 0.94f);
            Colors[(int)ImGuiCol.ChildBg] = new(0.00f, 0.00f, 0.00f, 0.00f);
            Colors[(int)ImGuiCol.PopupBg] = new(0.08f, 0.08f, 0.08f, 0.94f);
            Colors[(int)ImGuiCol.Border] = new(0.43f, 0.43f, 0.50f, 0.50f);
            Colors[(int)ImGuiCol.BorderShadow] = new(0.00f, 0.00f, 0.00f, 0.00f);
            Colors[(int)ImGuiCol.FrameBg] = new(0.16f, 0.29f, 0.48f, 0.54f);
            Colors[(int)ImGuiCol.FrameBgHovered] = new(0.26f, 0.59f, 0.98f, 0.40f);
            Colors[(int)ImGuiCol.FrameBgActive] = new(0.26f, 0.59f, 0.98f, 0.67f);
            Colors[(int)ImGuiCol.TitleBg] = new(0.04f, 0.04f, 0.04f, 1.00f);
            Colors[(int)ImGuiCol.TitleBgActive] = new(0.16f, 0.29f, 0.48f, 1.00f);
            Colors[(int)ImGuiCol.TitleBgCollapsed] = new(0.00f, 0.00f, 0.00f, 0.51f);
            Colors[(int)ImGuiCol.MenuBarBg] = new(0.14f, 0.14f, 0.14f, 1.00f);
            Colors[(int)ImGuiCol.ScrollbarBg] = new(0.02f, 0.02f, 0.02f, 0.53f);
            Colors[(int)ImGuiCol.ScrollbarGrab] = new(0.31f, 0.31f, 0.31f, 1.00f);
            Colors[(int)ImGuiCol.ScrollbarGrabHovered] = new(0.41f, 0.41f, 0.41f, 1.00f);
            Colors[(int)ImGuiCol.ScrollbarGrabActive] = new(0.51f, 0.51f, 0.51f, 1.00f);
            Colors[(int)ImGuiCol.CheckMark] = new(0.26f, 0.59f, 0.98f, 1.00f);
            Colors[(int)ImGuiCol.SliderGrab] = new(0.24f, 0.52f, 0.88f, 1.00f);
            Colors[(int)ImGuiCol.SliderGrabActive] = new(0.26f, 0.59f, 0.98f, 1.00f);
            Colors[(int)ImGuiCol.Button] = new(0.26f, 0.59f, 0.98f, 0.40f);
            Colors[(int)ImGuiCol.ButtonHovered] = new(0.26f, 0.59f, 0.98f, 1.00f);
            Colors[(int)ImGuiCol.ButtonActive] = new(0.06f, 0.53f, 0.98f, 1.00f);
            Colors[(int)ImGuiCol.Header] = new(0.26f, 0.59f, 0.98f, 0.31f);
            Colors[(int)ImGuiCol.HeaderHovered] = new(0.26f, 0.59f, 0.98f, 0.80f);
            Colors[(int)ImGuiCol.HeaderActive] = new(0.26f, 0.59f, 0.98f, 1.00f);
            Colors[(int)ImGuiCol.Separator] = Colors[(int)ImGuiCol.Border];
            Colors[(int)ImGuiCol.SeparatorHovered] = new(0.10f, 0.40f, 0.75f, 0.78f);
            Colors[(int)ImGuiCol.SeparatorActive] = new(0.10f, 0.40f, 0.75f, 1.00f);
            Colors[(int)ImGuiCol.ResizeGrip] = new(0.26f, 0.59f, 0.98f, 0.20f);
            Colors[(int)ImGuiCol.ResizeGripHovered] = new(0.26f, 0.59f, 0.98f, 0.67f);
            Colors[(int)ImGuiCol.ResizeGripActive] = new(0.26f, 0.59f, 0.98f, 0.95f);
            Colors[(int)ImGuiCol.TabHovered] = Colors[(int)ImGuiCol.HeaderHovered];
            Colors[(int)ImGuiCol.Tab] = Color.Lerp(Colors[(int)ImGuiCol.Header], Colors[(int)ImGuiCol.TitleBgActive], 0.80f);
            Colors[(int)ImGuiCol.TabSelected] = Color.Lerp(Colors[(int)ImGuiCol.HeaderActive], Colors[(int)ImGuiCol.TitleBgActive], 0.60f);
            Colors[(int)ImGuiCol.TabSelectedOverline] = Colors[(int)ImGuiCol.HeaderActive];
            Colors[(int)ImGuiCol.TabDimmed] = Color.Lerp(Colors[(int)ImGuiCol.Tab], Colors[(int)ImGuiCol.TitleBg], 0.80f);
            Colors[(int)ImGuiCol.TabDimmedSelected] = Color.Lerp(Colors[(int)ImGuiCol.TabSelected], Colors[(int)ImGuiCol.TitleBg], 0.40f);
            Colors[(int)ImGuiCol.TabDimmedSelectedOverline] = new(0.50f, 0.50f, 0.50f, 0.00f);
            Colors[(int)ImGuiCol.DockingPreview] = Colors[(int)ImGuiCol.HeaderActive] * new Color(1.0f, 1.0f, 1.0f, 0.7f);
            Colors[(int)ImGuiCol.DockingEmptyBg] = new(0.20f, 0.20f, 0.20f, 1.00f);
            Colors[(int)ImGuiCol.PlotLines] = new(0.61f, 0.61f, 0.61f, 1.00f);
            Colors[(int)ImGuiCol.PlotLinesHovered] = new(1.00f, 0.43f, 0.35f, 1.00f);
            Colors[(int)ImGuiCol.PlotHistogram] = new(0.90f, 0.70f, 0.00f, 1.00f);
            Colors[(int)ImGuiCol.PlotHistogramHovered] = new(1.00f, 0.60f, 0.00f, 1.00f);
            Colors[(int)ImGuiCol.TableHeaderBg] = new(0.19f, 0.19f, 0.20f, 1.00f);
            Colors[(int)ImGuiCol.TableBorderStrong] = new(0.31f, 0.31f, 0.35f, 1.00f);   // Prefer using Alpha=1.0 here
            Colors[(int)ImGuiCol.TableBorderLight] = new(0.23f, 0.23f, 0.25f, 1.00f);   // Prefer using Alpha=1.0 here
            Colors[(int)ImGuiCol.TableRowBg] = new(0.00f, 0.00f, 0.00f, 0.00f);
            Colors[(int)ImGuiCol.TableRowBgAlt] = new(1.00f, 1.00f, 1.00f, 0.06f);
            Colors[(int)ImGuiCol.TextLink] = Colors[(int)ImGuiCol.HeaderActive];
            Colors[(int)ImGuiCol.TextSelectedBg] = new(0.26f, 0.59f, 0.98f, 0.35f);
            Colors[(int)ImGuiCol.DragDropTarget] = new(1.00f, 1.00f, 0.00f, 0.90f);
            Colors[(int)ImGuiCol.NavCursor] = new(0.26f, 0.59f, 0.98f, 1.00f);
            Colors[(int)ImGuiCol.NavWindowingHighlight] = new(1.00f, 1.00f, 1.00f, 0.70f);
            Colors[(int)ImGuiCol.NavWindowingDimBg] = new(0.80f, 0.80f, 0.80f, 0.20f);
            Colors[(int)ImGuiCol.ModalWindowDimBg] = new(0.80f, 0.80f, 0.80f, 0.35f);
        }

        public unsafe void ApplyTo(ImGuiStylePtr s)
        {
            s.Alpha                       = Alpha;
            s.DisabledAlpha               = DisabledAlpha;
            s.WindowPadding               = WindowPadding;
            s.WindowRounding              = WindowRounding;
            s.WindowBorderSize            = WindowBorderSize;
            s.WindowMinSize               = WindowMinSize;
            s.WindowTitleAlign            = WindowTitleAlign;
            s.WindowMenuButtonPosition    = WindowMenuButtonPosition;
            s.ChildRounding               = ChildRounding;
            s.ChildBorderSize             = ChildBorderSize;
            s.PopupRounding               = PopupRounding;
            s.PopupBorderSize             = PopupBorderSize;
            s.FramePadding                = FramePadding;
            s.FrameRounding               = FrameRounding;
            s.FrameBorderSize             = FrameBorderSize;
            s.ItemSpacing                 = ItemSpacing;
            s.ItemInnerSpacing            = ItemInnerSpacing;
            s.CellPadding                 = CellPadding;
            s.TouchExtraPadding           = TouchExtraPadding;
            s.IndentSpacing               = IndentSpacing;
            s.ColumnsMinSpacing           = ColumnsMinSpacing;
            s.ScrollbarSize               = ScrollbarSize;
            s.ScrollbarRounding           = ScrollbarRounding;
            s.GrabMinSize                 = GrabMinSize;
            s.GrabRounding                = GrabRounding;
            s.LogSliderDeadzone           = LogSliderDeadzone;
            s.TabRounding                 = TabRounding;
            s.TabBorderSize               = TabBorderSize;   
            s.TabMinWidthForCloseButton   = TabMinWidthForCloseButton;
            s.TabBarBorderSize            = TabBarBorderSize;
            s.TabBarOverlineSize          = TabBarOverlineSize;
            s.TableAngledHeadersAngle     = TableAngledHeadersAngle;
            s.TableAngledHeadersTextAlign = TableAngledHeadersTextAlign;
            s.ColorButtonPosition         = ColorButtonPosition;
            s.ButtonTextAlign             = ButtonTextAlign;
            s.SelectableTextAlign         = SelectableTextAlign;
            s.SeparatorTextBorderSize     = SeparatorTextBorderSize;
            s.SeparatorTextAlign          = SeparatorTextAlign;
            s.SeparatorTextPadding        = SeparatorTextPadding;
            s.DisplayWindowPadding        = DisplayWindowPadding;
            s.DisplaySafeAreaPadding      = DisplaySafeAreaPadding;
            s.MouseCursorScale            = MouseCursorScale;
            s.AntiAliasedLines            = AntiAliasedLines;
            s.AntiAliasedLinesUseTex      = AntiAliasedLinesUseTex;
            s.AntiAliasedFill             = AntiAliasedFill;
            s.CurveTessellationTol        = CurveTessellationTol;
            s.CircleTessellationMaxError  = CircleTessellationMaxError;
            for (var i = 0; i < Colors.Length; ++i)
                s.Colors[i] = Colors[i];
        }

        public unsafe void SetFrom(ImGuiStylePtr s)
        {
            Alpha                       = s.Alpha;
            DisabledAlpha               = s.DisabledAlpha;
            WindowPadding               = s.WindowPadding;
            WindowRounding              = s.WindowRounding;
            WindowBorderSize            = s.WindowBorderSize;
            WindowMinSize               = s.WindowMinSize;
            WindowTitleAlign            = s.WindowTitleAlign;
            WindowMenuButtonPosition    = s.WindowMenuButtonPosition;
            ChildRounding               = s.ChildRounding;
            ChildBorderSize             = s.ChildBorderSize;
            PopupRounding               = s.PopupRounding;
            PopupBorderSize             = s.PopupBorderSize;
            FramePadding                = s.FramePadding;
            FrameRounding               = s.FrameRounding;
            FrameBorderSize             = s.FrameBorderSize;
            ItemSpacing                 = s.ItemSpacing;
            ItemInnerSpacing            = s.ItemInnerSpacing;
            CellPadding                 = s.CellPadding;
            TouchExtraPadding           = s.TouchExtraPadding;
            IndentSpacing               = s.IndentSpacing;
            ColumnsMinSpacing           = s.ColumnsMinSpacing;
            ScrollbarSize               = s.ScrollbarSize;
            ScrollbarRounding           = s.ScrollbarRounding;
            GrabMinSize                 = s.GrabMinSize;
            GrabRounding                = s.GrabRounding;
            LogSliderDeadzone           = s.LogSliderDeadzone;
            TabRounding                 = s.TabRounding;
            TabBorderSize               = s.TabBorderSize;
            TabMinWidthForCloseButton   = s.TabMinWidthForCloseButton;
            TabBarBorderSize            = s.TabBarBorderSize;
            TabBarOverlineSize          = s.TabBarOverlineSize;
            TableAngledHeadersAngle     = s.TableAngledHeadersAngle;
            TableAngledHeadersTextAlign = s.TableAngledHeadersTextAlign;
            ColorButtonPosition         = s.ColorButtonPosition;
            ButtonTextAlign             = s.ButtonTextAlign;
            SelectableTextAlign         = s.SelectableTextAlign;
            SeparatorTextBorderSize     = s.SeparatorTextBorderSize;
            SeparatorTextAlign          = s.SeparatorTextAlign;
            SeparatorTextPadding        = s.SeparatorTextPadding;
            DisplayWindowPadding        = s.DisplayWindowPadding;
            DisplaySafeAreaPadding      = s.DisplaySafeAreaPadding;
            MouseCursorScale            = s.MouseCursorScale;
            AntiAliasedLines            = s.AntiAliasedLines;
            AntiAliasedLinesUseTex      = s.AntiAliasedLinesUseTex;
            AntiAliasedFill             = s.AntiAliasedFill;
            CurveTessellationTol        = s.CurveTessellationTol;
            CircleTessellationMaxError  = s.CircleTessellationMaxError;
            for (var i = 0; i < Colors.Length; ++i)
                Colors[i] = s.Colors[i];
        }

        void Reset()
        {
            var context = ImGui.CreateContext();
            ImGui.SetCurrentContext(context);
            SetFrom(ImGui.GetStyle());
            ImGui.DestroyContext(context);
        }
    }
}