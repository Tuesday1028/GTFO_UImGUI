using Il2CppInterop.Runtime;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace ImGuiNET.CoreCLR.Interop
{
    internal static class CommandBufferInterop
    {
        private static readonly CommandBuffer_Internal_DrawProceduralIndexedIndirect_InjectedDelegate CommandBuffer_Internal_DrawProceduralIndexedIndirect_InjectedDelegateField = IL2CPP.ResolveICall<CommandBuffer_Internal_DrawProceduralIndexedIndirect_InjectedDelegate>("UnityEngine.Rendering.CommandBuffer::Internal_DrawProceduralIndexedIndirect_Injected");

        private delegate void CommandBuffer_Internal_DrawProceduralIndexedIndirect_InjectedDelegate(IntPtr commandBuffer, GraphicsBuffer indexBuffer, Matrix4x4 matrix, IntPtr material, int shaderPass, MeshTopology topology, IntPtr bufferWithArgs, int argsOffset, IntPtr properties);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawProceduralIndirect(this CommandBuffer buffer, GraphicsBuffer indexBuffer, Matrix4x4 matrix, Material material, int shaderPass, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
        {
            CommandBuffer_Internal_DrawProceduralIndexedIndirect_InjectedDelegateField(buffer.Pointer, indexBuffer, matrix, material.Pointer, shaderPass, topology, bufferWithArgs.Pointer, argsOffset, properties.Pointer);
        }
    }
}
