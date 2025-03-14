using System;
using UnityEngine;
using System.Runtime.InteropServices;
using ImGuiNET;

namespace ImGuizmoNET
{
    public static unsafe partial class ImGuizmoNative
    {
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuizmo_AllowAxisFlip(byte value);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuizmo_BeginFrame();
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuizmo_DecomposeMatrixToComponents(float* matrix, float* translation, float* rotation, float* scale);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuizmo_DrawCubes(float* view, float* projection, float* matrices, int matrixCount);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuizmo_DrawGrid(float* view, float* projection, float* matrix, float gridSize);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuizmo_Enable(byte enable);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint ImGuizmo_GetID_Str(byte* str_id);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint ImGuizmo_GetID_StrStr(byte* str_id_begin, byte* str_id_end);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint ImGuizmo_GetID_Ptr(void* ptr_id);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern Style* ImGuizmo_GetStyle();
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte ImGuizmo_IsOver_Nil();
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte ImGuizmo_IsOver_OPERATION(OPERATION op);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte ImGuizmo_IsOver_FloatPtr(float* position, float pixelRadius);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte ImGuizmo_IsUsing();
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte ImGuizmo_IsUsingAny();
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte ImGuizmo_IsUsingViewManipulate();
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte ImGuizmo_IsViewManipulateHovered();
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte ImGuizmo_Manipulate(float* view, float* projection, OPERATION operation, MODE mode, float* matrix, float* deltaMatrix, float* snap, float* localBounds, float* boundsSnap);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuizmo_PopID();
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuizmo_PushID_Str(byte* str_id);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuizmo_PushID_StrStr(byte* str_id_begin, byte* str_id_end);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuizmo_PushID_Ptr(void* ptr_id);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuizmo_PushID_Int(int int_id);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuizmo_RecomposeMatrixFromComponents(float* translation, float* rotation, float* scale, float* matrix);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuizmo_SetAlternativeWindow(ImGuiWindowClass* window);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuizmo_SetAxisLimit(float value);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuizmo_SetAxisMask(byte x, byte y, byte z);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuizmo_SetDrawlist(ImDrawList* drawlist);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuizmo_SetGizmoSizeClipSpace(float value);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuizmo_SetID(int id);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuizmo_SetImGuiContext(IntPtr ctx);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuizmo_SetOrthographic(byte isOrthographic);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuizmo_SetPlaneLimit(float value);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuizmo_SetRect(float x, float y, float width, float height);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuizmo_ViewManipulate_Float(float* view, float length, Vector2 position, Vector2 size, uint backgroundColor);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuizmo_ViewManipulate_FloatPtr(float* view, float* projection, OPERATION operation, MODE mode, float* matrix, float length, Vector2 position, Vector2 size, uint backgroundColor);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Style_destroy(Style* self);
        [DllImport("cimguizmo", CallingConvention = CallingConvention.Cdecl)]
        public static extern Style* Style_Style();
    }
}
