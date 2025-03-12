using System.Runtime.InteropServices;
using UnityEngine;
using ImGuiNET.CoreCLR.Interop;

namespace ImGuiNET.CoreCLR
{
    [StructLayout(LayoutKind.Explicit)]
    internal class GraphicsBuffer : IDisposable
    {
        [FieldOffset(16)]
        public IntPtr m_Ptr;

        ~GraphicsBuffer()
        {
            Dispose(false);
        }

        public GraphicsBuffer(UnityEngine.GraphicsBuffer.Target target, int count, int stride)
        {
            if (count <= 0)
            {
                throw new ArgumentException("Attempting to create a zero length graphics buffer", nameof(count));
            }
            if (stride <= 0)
            {
                throw new ArgumentException("Attempting to create a graphics buffer with a negative or null stride", nameof(stride));
            }
            if ((target & UnityEngine.GraphicsBuffer.Target.Index) != 0 && stride != 2 && stride != 4)
            {
                throw new ArgumentException("Attempting to create an index buffer with an invalid stride: " + stride, nameof(stride));
            }
            m_Ptr = UnityEngine.GraphicsBuffer.InitBuffer(target, count, stride);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.DestroyBuffer();
            }
            else
            {
                bool flag = m_Ptr != IntPtr.Zero;
                if (flag)
                {
                    Debug.LogWarning("GarbageCollector disposing of GraphicsBuffer. Please use GraphicsBuffer.Release() or .Dispose() to manually release the buffer.");
                }
            }
            m_Ptr = IntPtr.Zero;
        }

        public bool IsValid()
        {
            return m_Ptr != IntPtr.Zero;
        }

        public void Release()
        {
            Dispose();
        }

        public int count => this.Get_count();
        public int stride => this.Get_stride();
    }
}