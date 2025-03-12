using Il2CppInterop.Runtime;
using System.Runtime.CompilerServices;

namespace ImGuiNET.CoreCLR.Interop
{
    internal static class GraphicsBufferInterop
    {
        private static readonly GraphicsBuffer_DestroyBufferDelegate GraphicsBuffer_DestroyBufferDelegateField = IL2CPP.ResolveICall<GraphicsBuffer_DestroyBufferDelegate>("UnityEngine.GraphicsBuffer::DestroyBuffer");
        private static readonly GraphicsBuffer_get_countDelegate GraphicsBuffer_get_countDelegateField = IL2CPP.ResolveICall<GraphicsBuffer_get_countDelegate>("UnityEngine.GraphicsBuffer::get_count");
        private static readonly GraphicsBuffer_get_strideDelegate GraphicsBuffer_get_strideDelegateField = IL2CPP.ResolveICall<GraphicsBuffer_get_strideDelegate>("UnityEngine.GraphicsBuffer::get_stride");
        private static readonly GraphicsBuffer_InternalSetNativeDataDelegate GraphicsBuffer_InternalSetNativeDataDelegateField = IL2CPP.ResolveICall<GraphicsBuffer_InternalSetNativeDataDelegate>("UnityEngine.GraphicsBuffer::InternalSetNativeData");

        private delegate void GraphicsBuffer_DestroyBufferDelegate(GraphicsBuffer buf);
        private delegate int GraphicsBuffer_get_countDelegate(GraphicsBuffer buf);
        private delegate int GraphicsBuffer_get_strideDelegate(GraphicsBuffer buf);
        private delegate void GraphicsBuffer_InternalSetNativeDataDelegate(GraphicsBuffer buf, IntPtr data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InternalSetNativeData(this GraphicsBuffer buffer, IntPtr data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize)
        {
            GraphicsBuffer_InternalSetNativeDataDelegateField(buffer, data, nativeBufferStartIndex, graphicsBufferStartIndex, count, elemSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DestroyBuffer(this GraphicsBuffer buffer)
        {
            GraphicsBuffer_DestroyBufferDelegateField(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Get_count(this GraphicsBuffer buffer)
        {
            return GraphicsBuffer_get_countDelegateField(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Get_stride(this GraphicsBuffer buffer)
        {
            return GraphicsBuffer_get_strideDelegateField(buffer);
        }
    }       
}
