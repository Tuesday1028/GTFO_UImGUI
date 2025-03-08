using Hikaria.UImGUI.Assets;
using Hikaria.UImGUI.Texture;
using Il2CppInterop.Runtime;
using ImGuiNET;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;
using Object = UnityEngine.Object;

// TODO: switch from using ComputeBuffer to GraphicsBuffer
// starting from 2020.1 API that takes ComputeBuffer can also take GraphicsBuffer
// https://docs.unity3d.com/2020.1/Documentation/ScriptReference/GraphicsBuffer.Target.html

namespace Hikaria.UImGUI.Renderer
{
    /// <summary>
    /// Renderer bindings in charge of producing instructions for rendering ImGui draw data.
    /// Uses DrawProceduralIndirect to build geometry from a buffer of vertex data.
    /// </summary>
    /// <remarks>Requires shader model 4.5 level hardware.</remarks>
    internal sealed class RendererProcedural : IRenderer
	{
		private class GraphicsBuffer : IDisposable
		{
            ~GraphicsBuffer()
            {
                Dispose(false);
            }

            public GraphicsBuffer(UnityEngine.GraphicsBuffer.Target target, int count, int stride)
            {
                bool flag = count <= 0;
                if (flag)
                {
                    throw new ArgumentException("Attempting to create a zero length graphics buffer", "count");
                }
                bool flag2 = stride <= 0;
                if (flag2)
                {
                    throw new ArgumentException("Attempting to create a graphics buffer with a negative or null stride", "stride");
                }
                bool flag3 = (target & UnityEngine.GraphicsBuffer.Target.Index) != (UnityEngine.GraphicsBuffer.Target)0 && stride != 2 && stride != 4;
                if (flag3)
                {
                    throw new ArgumentException("Attempting to create an index buffer with an invalid stride: " + stride, "stride");
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
                    GraphicsBuffer_DestroyBufferDelegateField(this);
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

			public int count => GraphicsBuffer_get_countDelegateField(this);

            public int stride => GraphicsBuffer_get_strideDelegateField(this);

            public IntPtr m_Ptr;
		}

        private static readonly GraphicsBuffer_DestroyBufferDelegate GraphicsBuffer_DestroyBufferDelegateField = IL2CPP.ResolveICall<GraphicsBuffer_DestroyBufferDelegate>("UnityEngine.GraphicsBuffer::DestroyBuffer");
        private static readonly GraphicsBuffer_get_countDelegate GraphicsBuffer_get_countDelegateField = IL2CPP.ResolveICall<GraphicsBuffer_get_countDelegate>("UnityEngine.GraphicsBuffer::get_count");
        private static readonly GraphicsBuffer_get_strideDelegate GraphicsBuffer_get_strideDelegateField = IL2CPP.ResolveICall<GraphicsBuffer_get_strideDelegate>("UnityEngine.GraphicsBuffer::get_stride");
        private static readonly GraphicsBuffer_InternalSetNativeDataDelegate GraphicsBuffer_InternalSetNativeDataDelegateField = IL2CPP.ResolveICall<GraphicsBuffer_InternalSetNativeDataDelegate>("UnityEngine.GraphicsBuffer::InternalSetNativeData");
        private static readonly CommandBuffer_Internal_DrawProceduralIndexedIndirectDelegate CommandBuffer_Internal_DrawProceduralIndexedIndirectDelegateField = IL2CPP.ResolveICall<CommandBuffer_Internal_DrawProceduralIndexedIndirectDelegate>("UnityEngine.Rendering.CommandBuffer::Internal_DrawProceduralIndexedIndirect_Injected");

        private delegate void GraphicsBuffer_DestroyBufferDelegate(GraphicsBuffer buf);
        private delegate int GraphicsBuffer_get_countDelegate(GraphicsBuffer buf);
        private delegate int GraphicsBuffer_get_strideDelegate(GraphicsBuffer buf);
        private delegate void GraphicsBuffer_InternalSetNativeDataDelegate(GraphicsBuffer buf, IntPtr data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count, int elemSize);
        private delegate void CommandBuffer_Internal_DrawProceduralIndexedIndirectDelegate(IntPtr commandBuffer, GraphicsBuffer indexBuffer, Matrix4x4 matrix, IntPtr material, int shaderPass, MeshTopology topology, IntPtr bufferWithArgs, int argsOffset, IntPtr properties);

        private readonly Shader _shader;
		private readonly int _textureID;
		private readonly int _verticesID;
		private readonly int _baseVertexID;
		private readonly TextureManager _textureManager;

		private readonly MaterialPropertyBlock _materialProperties = new MaterialPropertyBlock();

		private Material _material;

		private ComputeBuffer _vertexBuffer; // GPU buffer for vertex data.
		private GraphicsBuffer _indexBuffer; // GPU buffer for indexes.
		private ComputeBuffer _argumentsBuffer; // GPU buffer for draw arguments.

		public RendererProcedural(ShaderResourcesAsset resources, TextureManager texManager)
		{
			if (SystemInfo.graphicsShaderLevel < 45)
			{
				throw new System.Exception("Device not supported.");
			}

			_shader = resources.Shader.Procedural;
			_textureManager = texManager;
			_textureID = Shader.PropertyToID(resources.PropertyNames.Texture);
			_verticesID = Shader.PropertyToID(resources.PropertyNames.Vertices);
			_baseVertexID = Shader.PropertyToID(resources.PropertyNames.BaseVertex);
        }

		public void Initialize(ImGuiIOPtr io)
		{
			io.SetBackendRendererName("Unity Procedural");
			// Supports ImDrawCmd::VtxOffset to output large meshes while still using 16-bits indices.
			io.BackendFlags |= ImGuiBackendFlags.RendererHasVtxOffset;

			_material = new Material(_shader)
			{
				hideFlags = HideFlags.HideAndDontSave & ~HideFlags.DontUnloadUnusedAsset
			};
		}

		public void Shutdown(ImGuiIOPtr io)
		{
			io.SetBackendRendererName(null);

			if (_material != null) { Object.Destroy(_material); _material = null; }
			_vertexBuffer?.Release(); _vertexBuffer = null;
			_indexBuffer?.Release(); _indexBuffer = null;
			_argumentsBuffer?.Release(); _argumentsBuffer = null;
		}

		public void RenderDrawLists(CommandBuffer cmd, ImDrawDataPtr drawData)
		{
			Vector2 fbSize = drawData.DisplaySize * drawData.FramebufferScale;

			// Avoid rendering when minimized.
			if (fbSize.x <= 0f || fbSize.y <= 0f || drawData.TotalVtxCount == 0) return;

			Constants.UpdateBuffersMarker.Begin();
			UpdateBuffers(drawData);
			Constants.UpdateBuffersMarker.End();

			cmd.BeginSample(Constants.ExecuteDrawCommandsMarker);

			Constants.CreateDrawComandsMarker.Begin();
			CreateDrawCommands(cmd, drawData, fbSize);
			Constants.CreateDrawComandsMarker.End();

			cmd.EndSample(Constants.ExecuteDrawCommandsMarker);
		}

        private void CreateOrResizeVtxBuffer(ref ComputeBuffer buffer, int count)
		{
			buffer?.Release();

			unsafe
			{
				int num = (((count - 1) / 256) + 1) * 256;
				buffer = new ComputeBuffer(num, sizeof(ImDrawVert));
			}
		}

		private void CreateOrResizeIdxBuffer(ref GraphicsBuffer buffer, int count)
		{
			buffer?.Release();

			unsafe
			{
				int num = (((count - 1) / 256) + 1) * 256;
                buffer = new GraphicsBuffer(UnityEngine.GraphicsBuffer.Target.Index, num, sizeof(ushort));
            }
		}

		private void CreateOrResizeArgBuffer(ref ComputeBuffer buffer, int count)
		{
			buffer?.Release();
			unsafe
			{
				int num = (((count - 1) / 256) + 1) * 256;
				buffer = new ComputeBuffer(num, sizeof(int), ComputeBufferType.IndirectArguments);
			}
		}

		private unsafe void UpdateBuffers(ImDrawDataPtr drawData)
		{
            int drawArgCount = 0; // nr of drawArgs is the same as the nr of ImDrawCmd
			for (int n = 0, nMax = drawData.CmdListsCount; n < nMax; ++n)
			{
				drawArgCount += drawData.CmdLists[n].CmdBuffer.Size;
			}

            // create or resize vertex/index buffers
            if (_vertexBuffer == null || _vertexBuffer.count < drawData.TotalVtxCount)
			{
				CreateOrResizeVtxBuffer(ref _vertexBuffer, drawData.TotalVtxCount);
			}

            if (_indexBuffer == null || _indexBuffer.count < drawData.TotalIdxCount)
			{
				CreateOrResizeIdxBuffer(ref _indexBuffer, drawData.TotalIdxCount);
			}

			if (_argumentsBuffer == null || _argumentsBuffer.count < drawArgCount * 5)
			{
				CreateOrResizeArgBuffer(ref _argumentsBuffer, drawArgCount * 5);
			}

            // upload vertex/index data into buffers
            int vtxOf = 0;
			int idxOf = 0;
			int argOf = 0;
			for (int n = 0, nMax = drawData.CmdListsCount; n < nMax; ++n)
			{
				ImDrawListPtr drawList = drawData.CmdLists[n];

                ImDrawVert[] vtxArray = new ImDrawVert[drawList.VtxBuffer.Size];
                ushort[] idxArray = new ushort[drawList.IdxBuffer.Size];

                fixed (ImDrawVert* pDest = vtxArray)
                {
                    Buffer.MemoryCopy(
                        (void*)drawList.VtxBuffer.Data,
                        pDest,
                        vtxArray.Length * sizeof(ImDrawVert),
                        vtxArray.Length * sizeof(ImDrawVert)
                    );
                    _vertexBuffer.InternalSetNativeData((IntPtr)pDest, 0, vtxOf, vtxArray.Length, sizeof(ImDrawVert));
                }

                fixed (ushort* pDest = idxArray)
                {
                    Buffer.MemoryCopy(
                        (void*)drawList.IdxBuffer.Data,
                        pDest,
                        idxArray.Length * sizeof(ushort),
                        idxArray.Length * sizeof(ushort)
                    );
                    GraphicsBuffer_InternalSetNativeDataDelegateField(_indexBuffer, (IntPtr)pDest, 0, idxOf, idxArray.Length, sizeof(ushort));
                }

				// Arguments for indexed draw.
				for (int meshIndex = 0, iMax = drawList.CmdBuffer.Size; meshIndex < iMax; ++meshIndex)
				{
					ImDrawCmdPtr cmd = drawList.CmdBuffer[meshIndex];
					var drawArgs = new int[]
					{
						(int)cmd.ElemCount,
						1,
						idxOf + (int)cmd.IdxOffset,
						vtxOf,
						0
					};
					fixed (int* pDest = drawArgs)
					{
                        _argumentsBuffer.InternalSetNativeData((IntPtr)pDest, 0, argOf, 5, sizeof(int));
                    }
                    argOf += 5; // 5 int for each command.
				}
				vtxOf += vtxArray.Length;
				idxOf += idxArray.Length;
			}
		}

		private void CreateDrawCommands(CommandBuffer cmd, ImDrawDataPtr drawData, Vector2 fbSize)
		{
			IntPtr prevTextureId = IntPtr.Zero;
			Vector4 clipOffst = new Vector4(drawData.DisplayPos.x, drawData.DisplayPos.y,
				drawData.DisplayPos.x, drawData.DisplayPos.y);
			Vector4 clipScale = new Vector4(drawData.FramebufferScale.x, drawData.FramebufferScale.y,
				drawData.FramebufferScale.x, drawData.FramebufferScale.y);

			_material.SetBuffer(_verticesID, _vertexBuffer); // Bind vertex buffer.

			cmd.SetViewport(new Rect(0f, 0f, fbSize.x, fbSize.y));
			cmd.SetViewProjectionMatrices(
				Matrix4x4.Translate(new Vector3(0.5f / fbSize.x, 0.5f / fbSize.y, 0f)), // Small adjustment to improve text.
				Matrix4x4.Ortho(0f, fbSize.x, fbSize.y, 0f, 0f, 1f));

			int vtxOf = 0;
			int argOf = 0;
			for (int commandListIndex = 0, nMax = drawData.CmdListsCount; commandListIndex < nMax; ++commandListIndex)
			{
				ImDrawListPtr drawList = drawData.CmdLists[commandListIndex];
				for (int commandIndex = 0, iMax = drawList.CmdBuffer.Size; commandIndex < iMax; ++commandIndex, argOf += 5 * 4)
				{
					ImDrawCmdPtr drawCmd = drawList.CmdBuffer[commandIndex];
					if (drawCmd.UserCallback != IntPtr.Zero)
					{
						UserDrawCallback userDrawCallback = Marshal.GetDelegateForFunctionPointer<UserDrawCallback>(drawCmd.UserCallback);
						userDrawCallback(drawList, drawCmd);
					}
					else
					{
						// Project scissor rectangle into framebuffer space and skip if fully outside.
						Vector4 clipSize = drawCmd.ClipRect - clipOffst;
						Vector4 clip = Vector4.Scale(clipSize, clipScale);

						if (clip.x >= fbSize.x || clip.y >= fbSize.y || clip.z < 0f || clip.w < 0f) continue;

						if (prevTextureId != drawCmd.TextureId)
						{
							prevTextureId = drawCmd.TextureId;

							// TODO: Implement ImDrawCmdPtr.GetTexID().
							bool hasTexture = _textureManager.TryGetTexture(prevTextureId, out UnityEngine.Texture texture);
							if (!hasTexture)
							{
								Logs.LogError($"Texture {prevTextureId} does not exist. Try to use UImGuiUtility.GetTextureID().");
								continue;
							}

							_materialProperties.SetTexture(_textureID, texture);
						}

						// Base vertex location not automatically added to SV_VertexID.
						_materialProperties.SetInt(_baseVertexID, vtxOf + (int)drawCmd.VtxOffset);

						cmd.EnableScissorRect(new Rect(clip.x, fbSize.y - clip.w, clip.z - clip.x, clip.w - clip.y)); // Invert y.
                        CommandBuffer_Internal_DrawProceduralIndexedIndirectDelegateField(cmd.Pointer, _indexBuffer, Matrix4x4.identity, _material.Pointer, -1, 
							MeshTopology.Triangles, _argumentsBuffer.Pointer, argOf, _materialProperties.Pointer);
                    }
				}
				vtxOf += drawList.VtxBuffer.Size;
			}
			cmd.DisableScissorRect();
		}
	}
}
