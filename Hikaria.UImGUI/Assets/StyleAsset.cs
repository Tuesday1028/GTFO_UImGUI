using ImGuiNET;
using System.ComponentModel;
using UnityEngine;

namespace Hikaria.UImGUI.Assets
{
	public sealed class StyleAsset
	{
		[Description("Global alpha applies to everything in ImGui.")]
		public float Alpha = 1;

		[Description("Padding within a window.")]
		public Vector2 WindowPadding = new Vector2(8, 8);

		[Description("Radius of window corners rounding. Set to 0.0f to have rectangular windows. " +
			"Large values tend to lead to variety of artifacts and are not recommended.")]
		public float WindowRounding = 7f;

		[Description("Thickness of border around windows. Generally set to 0.0f or 1.0f. " +
			"(Other values are not well tested and more CPU/GPU costly).")]
		public float WindowBorderSize = 1f;

		[Description("Minimum window size. This is a global setting. " +
			"If you want to constraint individual windows, use SetNextWindowSizeConstraints().")]
		public Vector2 WindowMinSize = new Vector2(32, 32);

		[Description("Alignment for title bar text. Defaults to (0.0f,0.5f) for left-aligned,vertically centered.")]
		public Vector2 WindowTitleAlign = new Vector2(0, 0.5f);

		[Description("Side of the collapsing/docking button in the title bar (left/right). Defaults to ImGuiDir_Left.")]
		public ImGuiDir WindowMenuButtonPosition = ImGuiDir.Left;

		[Description("Radius of child window corners rounding. Set to 0.0f to have rectangular windows.")]
		public float ChildRounding = 0;

		[Description("Thickness of border around child windows. Generally set to 0.0f or 1.0f. " +
			"(Other values are not well tested and more CPU/GPU costly).")]
		public float ChildBorderSize = 1;

		[Description("Radius of popup window corners rounding. (Note that tooltip windows use WindowRounding)")]
		public float PopupRounding = 0;

		[Description("Thickness of border around popup/tooltip windows. Generally set to 0.0f or 1.0f. " +
			"(Other values are not well tested and more CPU/GPU costly).")]
		public float PopupBorderSize = 1;

		[Description("Padding within a framed rectangle (used by most widgets).")]
		public Vector2 FramePadding = new Vector2(4, 3);

		[Description("Radius of frame corners rounding. Set to 0.0f to have rectangular frame (used by most widgets).")]
		public float FrameRounding = 0;

		[Description("Thickness of border around frames. Generally set to 0.0f or 1.0f. " +
			"(Other values are not well tested and more CPU/GPU costly).")]
		public float FrameBorderSize = 0f;

		[Description("Horizontal and vertical spacing between widgets/lines.")]
		public Vector2 ItemSpacing = new Vector2(8, 4);

		[Description("Horizontal and vertical spacing between within elements of a composed widget (e.g. a slider and its label).")]
		public Vector2 ItemInnerSpacing = new Vector2(4, 4);

		[Description("Padding within a table cell.")]
		public Vector2 CellPadding = Vector2.zero;

		[Description("Expand reactive bounding box for touch-based system where touch position is not accurate enough. " +
			"Unfortunately we don't sort widgets so priority on overlap will always be given to the first widget. " +
			"So don't grow this too much!")]
		public Vector2 TouchExtraPadding = Vector2.zero;

		[Description("Horizontal indentation when e.g. entering a tree node. Generally == (FontSize + FramePadding.x*2).")]
		public float IndentSpacing = 21;

		[Description("Minimum horizontal spacing between two columns. Preferably > (FramePadding.x + 1).")]
		public float ColumnsMinSpacing = 6;

		[Description("Width of the vertical scrollbar, Height of the horizontal scrollbar.")]
		public float ScrollbarSize = 14;

		[Description("Radius of grab corners for scrollbar.")]
		public float ScrollbarRounding = 9;

		[Description("Minimum width/height of a grab box for slider/scrollbar.")]
		public float GrabMinSize = 10;

		[Description("Radius of grabs corners rounding. Set to 0.0f to have rectangular slider grabs.")]
		public float GrabRounding = 0;

		[Description("")]
		public float LogSliderDeadzone = 0;

		[Description("Radius of upper corners of a tab. Set to 0.0f to have rectangular tabs.")]
		public float TabRounding = 4;

		[Description("Thickness of border around tabs.")]
		public float TabBorderSize = 0;

		[Description("Side of the color button in the ColorEdit4 widget (left/right). " +
			"Defaults to ImGuiDir_Right.")]
		public ImGuiDir ColorButtonPosition = ImGuiDir.Right;

		[Description("Alignment of button text when button is larger than text. " +
			"Defaults to (0.5f, 0.5f) (centered).")]
		public Vector2 ButtonTextAlign = new Vector2(0.5f, 0.5f);

		[Description("Alignment of selectable text when selectable is larger than text. " +
			"Defaults to (0.0f, 0.0f) (top-left aligned).")]
		public Vector2 SelectableTextAlign = Vector2.zero;

		[Description("Window position are clamped to be visible within the display area by at least this amount. " +
			"Only applies to regular windows.")]
		public Vector2 DisplayWindowPadding = new Vector2(19, 19);

		[Description("If you cannot see the edges of your screen (e.g. on a TV) increase the safe area padding. " +
			"Apply to popups/tooltips as well regular windows. NB: Prefer configuring your TV sets correctly!")]
		public Vector2 DisplaySafeAreaPadding = new Vector2(3, 3);

		[Description("Scale software rendered mouse cursor (when io.MouseDrawCursor is enabled). May be removed later.")]
		//[Min(1)]
		public float MouseCursorScale = 1;

		[Description("Enable anti-aliasing on lines/borders. Disable if you are really tight on CPU/GPU.")]
		public bool AntiAliasedLines = true;

		[Description("Enable anti-aliased lines/borders using textures where possible. " +
			"Require backend to render with bilinear filtering. " +
			"Latched at the beginning of the frame (copied to ImDrawList).")]
		public bool AntiAliasedLinesUseTex = true;

		[Description("Enable anti-aliasing on filled shapes (rounded rectangles, circles, etc.)")]
		public bool AntiAliasedFill = true;

		[Description("Tessellation tolerance when using PathBezierCurveTo() without a specific number of segments. " +
			"Decrease for highly tessellated curves (higher quality, more polygons), increase to reduce quality.")]
		//[Min(0.01f)]
		public float CurveTessellationTol = 2;

		[Description("Maximum error (in pixels) allowed when using AddCircle()/AddCircleFilled() or " +
			"drawing rounded corner rectangles with no explicit segment count specified. " +
			"Decrease for higher quality but more geometry. Cannot be 0.")]
		//[Min(0.01f)]
		public float CircleTessellationMaxError = 2;

		//[HideInInspector]
		public Color[] Colors = new Color[(int)ImGuiCol.COUNT]
		{
			new Color(1f, 1f, 1f, 1f),
			new Color(0.5019608f, 0.5019608f, 0.5019608f, 1f),
			new Color(0.058823533f, 0.058823533f, 0.058823533f, 0.9019608f),
			new Color(0f, 0f, 0f, 0.5254902f),
			new Color(0.078431375f, 0.078431375f, 0.078431375f, 1f),
			new Color(0.43137258f, 0.43137258f, 0.5019608f, 0.54509807f),
			new Color(0f, 0f, 0f, 0.59607846f),
			new Color(0.16078432f, 0.2901961f, 0.4784314f, 0.6117647f),
			new Color(0.25882354f, 0.5882353f, 0.9803922f, 0.5372549f),
			new Color(0.25882354f, 0.5882353f, 0.9803922f, 0.65882355f),
			new Color(0.039215688f, 0.039215688f, 0.039215688f, 1f),
			new Color(0.16078432f, 0.2901961f, 0.4784314f, 1f),
			new Color(0f, 0f, 0f, 0.53333336f),
			new Color(0.14117648f, 0.14117648f, 0.14117648f, 1f),
			new Color(0.019607844f, 0.019607844f, 0.019607844f, 0.5176471f),
			new Color(0.30980393f, 0.30980393f, 0.30980393f, 1f),
			new Color(0.41176474f, 0.41176474f, 0.41176474f, 1f),
			new Color(0.50980395f, 0.50980395f, 0.50980395f, 1f),
			new Color(0.25882354f, 0.5882353f, 0.9803922f, 1f),
			new Color(0.25882354f, 0.5882353f, 0.9803922f, 1f),
			new Color(0.25882354f, 0.5882353f, 0.9803922f, 1f),
			new Color(0.25882354f, 0.5882353f, 0.9803922f, 0.3529412f),
			new Color(0.25882354f, 0.5882353f, 0.9803922f, 1f),
			new Color(0.058823533f, 0.5294118f, 0.9803922f, 1f),
			new Color(0.25882354f, 0.5882353f, 0.9803922f, 0.3529412f),
			new Color(0.25882354f, 0.5882353f, 0.9803922f, 0.87058824f),
			new Color(0.25882354f, 0.5882353f, 0.9803922f, 0.7294118f),
			new Color(0.43137258f, 0.43137258f, 0.5019608f, 0.4117647f),
			new Color(0.10196079f, 0.40000004f, 0.7490196f, 0.59607846f),
			new Color(0.10196079f, 0.40000004f, 0.7490196f, 1f),
			new Color(0.25882354f, 0.5882353f, 0.9803922f, 0.34117648f),
			new Color(0.25882354f, 0.5882353f, 0.9803922f, 0.79607844f),
			new Color(0.25882354f, 0.5882353f, 0.9803922f, 1f),
			new Color(0.18039216f, 0.34901962f, 0.5803922f, 0.89411765f),
			new Color(0.25882354f, 0.5882353f, 0.9803922f, 0.78039217f),
			new Color(0.20000002f, 0.41176474f, 0.6784314f, 1f),
			new Color(0.06666667f, 0.10196079f, 0.14901961f, 0.94509804f),
			new Color(0.13725491f, 0.2627451f, 0.42352945f, 1f),
			new Color(0.6117647f, 0.6117647f, 0.6117647f, 1f),
			new Color(1f, 0.43137258f, 0.34901962f, 1f),
			new Color(0.90196085f, 0.7019608f, 0f, 1f),
			new Color(1f, 0.6f, 0f, 1f),
			new Color(0.25882354f, 0.5882353f, 0.9803922f, 0.34117648f),
			new Color(1f, 1f, 0f, 0.78039217f),
			new Color(0.25882354f, 0.5882353f, 0.9803922f, 1f),
			new Color(1f, 1f, 1f, 0.6745098f),
			new Color(0.8000001f, 0.8000001f, 0.8000001f, 0.30588236f),
			new Color(0.8000001f, 0.8000001f, 0.8000001f, 0.38431373f),
			new Color(0.8000001f, 0.8000001f, 0.8000001f, 1f),
			new Color(0f, 0f, 0f, 1f),
			new Color(0f, 1f, 0f, 1f),
			new Color(1f, 0f, 0.9449339f, 1f),
			new Color(1f, 0f, 0.69310665f, 1f),
			new Color(0.11671257f, 0f, 1f, 1f),
			new Color(0f, 0f, 1f, 1f),
			new Color(1f, 1f, 1f, 1f),
			new Color(1f, 1f, 1f, 1f),
			new Color(1f, 1f, 1f, 1f)
		};


        public unsafe void ApplyTo(ImGuiStylePtr s)
		{
			s.Alpha = Alpha;

			s.WindowPadding = WindowPadding;
			s.WindowRounding = WindowRounding;
			s.WindowBorderSize = WindowBorderSize;
			s.WindowMinSize = WindowMinSize;
			s.WindowTitleAlign = WindowTitleAlign;
			s.WindowMenuButtonPosition = WindowMenuButtonPosition;

			s.ChildRounding = ChildRounding;
			s.ChildBorderSize = ChildBorderSize;

			s.PopupRounding = PopupRounding;
			s.PopupBorderSize = PopupBorderSize;

			s.FramePadding = FramePadding;
			s.FrameRounding = FrameRounding;
			s.FrameBorderSize = FrameBorderSize;

			s.ItemSpacing = ItemSpacing;
			s.ItemInnerSpacing = ItemInnerSpacing;

			s.CellPadding = CellPadding;

			s.TouchExtraPadding = TouchExtraPadding;

			s.IndentSpacing = IndentSpacing;

			s.ColumnsMinSpacing = ColumnsMinSpacing;

			s.ScrollbarSize = ScrollbarSize;
			s.ScrollbarRounding = ScrollbarRounding;

			s.GrabMinSize = GrabMinSize;
			s.GrabRounding = GrabRounding;

			s.LogSliderDeadzone = LogSliderDeadzone;

			s.TabRounding = TabRounding;
			s.TabBorderSize = TabBorderSize;

			s.ColorButtonPosition = ColorButtonPosition;

			s.ButtonTextAlign = ButtonTextAlign;

			s.SelectableTextAlign = SelectableTextAlign;

			s.DisplayWindowPadding = DisplayWindowPadding;
			s.DisplaySafeAreaPadding = DisplaySafeAreaPadding;

			s.MouseCursorScale = MouseCursorScale;

			s.AntiAliasedLines = AntiAliasedLines;
			s.AntiAliasedLinesUseTex = AntiAliasedLinesUseTex;
			s.AntiAliasedFill = AntiAliasedFill;

			s.CurveTessellationTol = CurveTessellationTol;
			s.CircleTessellationMaxError = CircleTessellationMaxError;

			for (int colorIndex = 0; colorIndex < Colors.Length; ++colorIndex)
			{
				s.Colors[colorIndex] = Colors[colorIndex];
			}
		}

		public unsafe void SetFrom(ImGuiStylePtr s)
		{
			Alpha = s.Alpha;

			WindowPadding = s.WindowPadding;
			WindowRounding = s.WindowRounding;
			WindowBorderSize = s.WindowBorderSize;
			WindowMinSize = s.WindowMinSize;
			WindowTitleAlign = s.WindowTitleAlign;
			WindowMenuButtonPosition = s.WindowMenuButtonPosition;

			ChildRounding = s.ChildRounding;
			ChildBorderSize = s.ChildBorderSize;

			PopupRounding = s.PopupRounding;
			PopupBorderSize = s.PopupBorderSize;

			FramePadding = s.FramePadding;
			FrameRounding = s.FrameRounding;
			FrameBorderSize = s.FrameBorderSize;

			ItemSpacing = s.ItemSpacing;
			ItemInnerSpacing = s.ItemInnerSpacing;

			CellPadding = s.CellPadding;

			TouchExtraPadding = s.TouchExtraPadding;

			IndentSpacing = s.IndentSpacing;

			ColumnsMinSpacing = s.ColumnsMinSpacing;

			ScrollbarSize = s.ScrollbarSize;
			ScrollbarRounding = s.ScrollbarRounding;

			GrabMinSize = s.GrabMinSize;
			GrabRounding = s.GrabRounding;

			LogSliderDeadzone = s.LogSliderDeadzone;

			TabRounding = s.TabRounding;
			TabBorderSize = s.TabBorderSize;

			ColorButtonPosition = s.ColorButtonPosition;

			ButtonTextAlign = s.ButtonTextAlign;

			SelectableTextAlign = s.SelectableTextAlign;

			DisplayWindowPadding = s.DisplayWindowPadding;
			DisplaySafeAreaPadding = s.DisplaySafeAreaPadding;

			MouseCursorScale = s.MouseCursorScale;

			AntiAliasedLines = s.AntiAliasedLines;
			AntiAliasedLinesUseTex = s.AntiAliasedLinesUseTex;
			AntiAliasedFill = s.AntiAliasedFill;

			CurveTessellationTol = s.CurveTessellationTol;
			CircleTessellationMaxError = s.CircleTessellationMaxError;

			for (int colorIndex = 0; colorIndex < Colors.Length; ++colorIndex)
			{
				Colors[colorIndex] = s.Colors[colorIndex];
			}
		}

		public void SetDefault()
		{
			System.IntPtr context = ImGui.CreateContext();
			ImGui.SetCurrentContext(context);
			SetFrom(ImGui.GetStyle());
			ImGui.DestroyContext(context);
		}
	}
}
