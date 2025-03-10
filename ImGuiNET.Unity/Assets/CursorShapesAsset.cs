using System.ComponentModel;
using UnityEngine;

namespace ImGuiNET
{
    sealed class CursorShapesAsset
    {
        [Serializable]
        public struct CursorShape
        {
            public Texture2D texture;
            public Vector2 hotspot;
        }

        [Description("Default.")]
        public CursorShape Arrow;

        [Description("When hovering over InputText, etc.")]
        public CursorShape TextInput;

        [Description("(Unused by ImGui functions)")]
        public CursorShape ResizeAll;

        [Description("When hovering over an horizontal border")]
        public CursorShape ResizeEW;

        [Description("When hovering over a vertical border or column")]
        public CursorShape ResizeNS;

        [Description("When hovering over the bottom-left corner of a window")]
        public CursorShape ResizeNESW;

        [Description("When hovering over the bottom-right corner of a window")]
        public CursorShape ResizeNWSE;

        [Description("(Unused by ImGui functions. Use for e.g. hyperlinks)")]
        public CursorShape Hand;

        [Description("When hovering something with disabled interaction. Usually a crossed circle.")]
        public CursorShape NotAllowed;

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
