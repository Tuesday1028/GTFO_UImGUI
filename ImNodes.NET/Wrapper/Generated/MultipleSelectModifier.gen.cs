using System;
using UnityEngine;
using System.Runtime.CompilerServices;
using System.Text;
using ImGuiNET;

namespace ImNodesNET
{
    public unsafe partial struct MultipleSelectModifier
    {
        public byte* Modifier;
    }
    public unsafe partial struct MultipleSelectModifierPtr
    {
        public MultipleSelectModifier* NativePtr { get; }
        public MultipleSelectModifierPtr(MultipleSelectModifier* nativePtr) => NativePtr = nativePtr;
        public MultipleSelectModifierPtr(IntPtr nativePtr) => NativePtr = (MultipleSelectModifier*)nativePtr;
        public static implicit operator MultipleSelectModifierPtr(MultipleSelectModifier* nativePtr) => new MultipleSelectModifierPtr(nativePtr);
        public static implicit operator MultipleSelectModifier* (MultipleSelectModifierPtr wrappedPtr) => wrappedPtr.NativePtr;
        public static implicit operator MultipleSelectModifierPtr(IntPtr nativePtr) => new MultipleSelectModifierPtr(nativePtr);
        public IntPtr Modifier { get => (IntPtr)NativePtr->Modifier; set => NativePtr->Modifier = (byte*)value; }
        public void Destroy()
        {
            ImNodesNative.MultipleSelectModifier_destroy(NativePtr);
        }
    }
}
