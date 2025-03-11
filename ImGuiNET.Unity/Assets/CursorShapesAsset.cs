using Hikaria.UImGUI;
using System.ComponentModel;
using UnityEngine;

namespace ImGuiNET
{
    sealed class CursorShapesAsset
    {
        [Serializable]
        public struct CursorShape
        {
            public Texture2D Texture;
            public Vector2 Hotspot;
        }

        [Description("Default.")]
        public CursorShape Arrow = new()
        {
            Texture = AssetsHelper.GetAssets<Texture2D>("Assets/Resources/UnityImGUI/Cursors/default.png"),
            Hotspot = new Vector2(7, 5)
        };

        [Description("When hovering over InputText, etc.")]
        public CursorShape TextInput = new()
        {
            Texture = AssetsHelper.GetAssets<Texture2D>("Assets/Resources/UnityImGUI/Cursors/text.png"),
            Hotspot = new Vector2(11, 11)
        };

        [Description("(Unused by ImGui functions)")]
        public CursorShape ResizeAll = new()
        {
            Texture = AssetsHelper.GetAssets<Texture2D>("Assets/Resources/UnityImGUI/Cursors/all-scroll.png"),
            Hotspot = new Vector2(4, 5)
        };

        [Description("When hovering over an horizontal border")]
        public CursorShape ResizeEW = new()
        {
            Texture = AssetsHelper.GetAssets<Texture2D>("Assets/Resources/UnityImGUI/Cursors/col-resize.png"),
            Hotspot = new Vector2(11, 11)
        };

        [Description("When hovering over a vertical border or column")]
        public CursorShape ResizeNS = new()
        {
            Texture = AssetsHelper.GetAssets<Texture2D>("Assets/Resources/UnityImGUI/Cursors/row-resize.png"),
            Hotspot = new Vector2(11, 11)
        };

        [Description("When hovering over the bottom-left corner of a window")]
        public CursorShape ResizeNESW = new()
        {
            Texture = AssetsHelper.GetAssets<Texture2D>("Assets/Resources/UnityImGUI/Cursors/bottom_left_corner.png"),
            Hotspot = new Vector2(11, 11)
        };

        [Description("When hovering over the bottom-right corner of a window")]
        public CursorShape ResizeNWSE = new()
        {
            Texture = AssetsHelper.GetAssets<Texture2D>("Assets/Resources/UnityImGUI/Cursors/bottom_right_corner.png"),
            Hotspot = new Vector2(11, 11)
        };

        [Description("(Unused by ImGui functions. Use for e.g. hyperlinks)")]
        public CursorShape Hand = new()
        {
            Texture = AssetsHelper.GetAssets<Texture2D>("Assets/Resources/UnityImGUI/Cursors/pointer.png"),
            Hotspot = new Vector2(9, 5)
        };

        [Description("When hovering something with disabled interaction. Usually a crossed circle.")]
        public CursorShape NotAllowed = new()
        {
            Texture = AssetsHelper.GetAssets<Texture2D>("Assets/Resources/UnityImGUI/Cursors/not-allowed.png"),
            Hotspot = new Vector2(11, 11)
        };

        public ref CursorShape this[ImGuiMouseCursor cursor]
        {
            get
            {
                switch (cursor)
                {
                    case ImGuiMouseCursor.Arrow:      return ref Arrow;
                    case ImGuiMouseCursor.TextInput:  return ref TextInput;
                    case ImGuiMouseCursor.ResizeAll:  return ref ResizeAll;
                    case ImGuiMouseCursor.ResizeEW:   return ref ResizeEW;
                    case ImGuiMouseCursor.ResizeNS:   return ref ResizeNS;
                    case ImGuiMouseCursor.ResizeNESW: return ref ResizeNESW;
                    case ImGuiMouseCursor.ResizeNWSE: return ref ResizeNWSE;
                    case ImGuiMouseCursor.Hand:       return ref Hand;
                    case ImGuiMouseCursor.NotAllowed: return ref NotAllowed;
                    default:                          return ref Arrow;
                }
            }
        }
    }
}
