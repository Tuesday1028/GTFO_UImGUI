using Hikaria.UImGUI;
using ImGuiNET.CoreCLR.Interop;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Rendering;
using GraphicsBuffer = ImGuiNET.CoreCLR.GraphicsBuffer;
using Object = UnityEngine.Object;

// TODO: switch from using ComputeBuffer to GraphicsBuffer
// starting from 2020.1 API that takes ComputeBuffer can also take GraphicsBuffer
// https://docs.unity3d.com/2020.1/Documentation/ScriptReference/GraphicsBuffer.Target.html

namespace ImGuiNET
{
    /// <summary>
    /// Renderer bindings in charge of producing instructions for rendering ImGui draw data.
    /// Uses DrawProceduralIndirect to build geometry from a buffer of vertex data.
    /// </summary>
    /// <remarks>Requires shader model 4.5 level hardware.</remarks>
    sealed class ImGuiRendererProcedural : IImGuiRenderer
    {
        readonly Shader _shader;
        readonly int _texID;
        readonly int _verticesID;
        readonly int _baseVertexID;

        Material _material;
        readonly MaterialPropertyBlock _properties = new MaterialPropertyBlock();

        readonly TextureManager _texManager;

        ComputeBuffer _vtxBuf;                                                  // gpu buffer for vertex data
        GraphicsBuffer _idxBuf;                                         // gpu buffer for indexes
        ComputeBuffer _argBuf;                                                  // gpu buffer for draw arguments
        readonly int[] _drawArgs = new int[] { 0, 1, 0, 0, 0 };                 // used to build argument buffer

        static readonly ProfilerMarker s_updateBuffersPerfMarker = new ProfilerMarker("DearImGui.RendererProcedural.UpdateBuffers");
        static readonly ProfilerMarker s_createDrawComandsPerfMarker = new ProfilerMarker("DearImGui.RendererProcedural.CreateDrawCommands");

        public ImGuiRendererProcedural(ShaderResourcesAsset resources, TextureManager texManager)
        {
            if (SystemInfo.graphicsShaderLevel < 45)
                throw new Exception("Device not supported");

            _shader = resources.shaders.procedural;
            _texManager = texManager;
            _texID = Shader.PropertyToID(resources.propertyNames.texture);
            _verticesID = Shader.PropertyToID(resources.propertyNames.vertices);
            _baseVertexID = Shader.PropertyToID(resources.propertyNames.baseVertex);
        }

        public void Initialize(ImGuiIOPtr io)
        {
            io.SetBackendRendererName("Unity Procedural");                      // setup renderer info and capabilities
            io.BackendFlags |= ImGuiBackendFlags.RendererHasVtxOffset;          // supports ImDrawCmd::VtxOffset to output large meshes while still using 16-bits indices

            _material = new Material(_shader) { hideFlags = HideFlags.HideAndDontSave & ~HideFlags.DontUnloadUnusedAsset };
        }

        public void Shutdown(ImGuiIOPtr io)
        {
            io.SetBackendRendererName(null);

            if (_material != null) { Object.Destroy(_material); _material = null; }
            _vtxBuf?.Release(); _vtxBuf = null;
            _idxBuf?.Release(); _idxBuf = null;
            _argBuf?.Release(); _argBuf = null;
        }

        public void RenderDrawLists(CommandBuffer cmd, ImDrawDataPtr drawData)
        {
            Vector2 fbSize = drawData.DisplaySize * drawData.FramebufferScale;
            if (fbSize.x <= 0f || fbSize.y <= 0f || drawData.TotalVtxCount == 0)
                return; // avoid rendering when minimized

            s_updateBuffersPerfMarker.Begin();
            UpdateBuffers(drawData);
            s_updateBuffersPerfMarker.End();

            cmd.BeginSample("DearImGui.ExecuteDrawCommands");
            s_createDrawComandsPerfMarker.Begin();
            CreateDrawCommands(cmd, drawData, fbSize);
            s_createDrawComandsPerfMarker.End();
            cmd.EndSample("DearImGui.ExecuteDrawCommands");
        }

        unsafe void CreateOrResizeVtxBuffer(ref ComputeBuffer buffer, int count)
        {
            int num = ((count - 1) / 256 + 1) * 256;
            buffer?.Release();
            buffer = new ComputeBuffer(num, sizeof(ImDrawVert));
        }
        unsafe void CreateOrResizeIdxBuffer(ref GraphicsBuffer buffer, int count)
        {
            int num = ((count - 1) / 256 + 1) * 256;
            buffer?.Release();
            buffer = new GraphicsBuffer(UnityEngine.GraphicsBuffer.Target.Index, num, sizeof(ushort));
        }
        unsafe void CreateOrResizeArgBuffer(ref ComputeBuffer buffer, int count)
        {
            int num = ((count - 1) / 256 + 1) * 256;
            buffer?.Release();
            buffer = new ComputeBuffer(num, sizeof(int), ComputeBufferType.IndirectArguments);
        }

        unsafe void UpdateBuffers(ImDrawDataPtr drawData)
        {
            int drawArgCount = 0; // nr of drawArgs is the same as the nr of ImDrawCmd
            for (int n = 0, nMax = drawData.CmdListsCount; n < nMax; ++n)
                drawArgCount += drawData.CmdLists[n].CmdBuffer.Size;

            // create or resize vertex/index buffers
            if (_vtxBuf == null || _vtxBuf.count < drawData.TotalVtxCount)
                CreateOrResizeVtxBuffer(ref _vtxBuf, drawData.TotalVtxCount);
            if (_idxBuf == null || _idxBuf.count < drawData.TotalIdxCount)
                CreateOrResizeIdxBuffer(ref _idxBuf, drawData.TotalIdxCount);
            if (_argBuf == null || _argBuf.count < drawArgCount * 5)
                CreateOrResizeArgBuffer(ref _argBuf, drawArgCount * 5);

            // upload vertex/index data into buffers
            int vtxOf = 0;
            int idxOf = 0;
            int argOf = 0;
            for (int n = 0, nMax = drawData.CmdListsCount; n < nMax; ++n)
            {
                ImDrawListPtr drawList = drawData.CmdLists[n];

                // upload vertex/index data
                ImDrawVert[] vtxArray = new ImDrawVert[drawList.VtxBuffer.Size];
                fixed (ImDrawVert* pDest = vtxArray)
                {
                    Buffer.MemoryCopy(
                        (void*)drawList.VtxBuffer.Data,
                        pDest,
                        vtxArray.Length * sizeof(ImDrawVert),
                        vtxArray.Length * sizeof(ImDrawVert)
                    );
                    _vtxBuf.InternalSetNativeData((IntPtr)pDest, 0, vtxOf, vtxArray.Length, sizeof(ImDrawVert));
                }

                ushort[] idxArray = new ushort[drawList.IdxBuffer.Size];
                fixed (ushort* pDest = idxArray)
                {
                    Buffer.MemoryCopy(
                         (void*)drawList.IdxBuffer.Data,
                         pDest,
                         idxArray.Length * sizeof(ushort),
                         idxArray.Length * sizeof(ushort)
                     );
                    _idxBuf.InternalSetNativeData((IntPtr)pDest, 0, idxOf, idxArray.Length, sizeof(ushort));
                }

                // arguments for indexed draw
                _drawArgs[3] = vtxOf;                                           // base vertex location
                for (int i = 0, iMax = drawList.CmdBuffer.Size; i < iMax; ++i)
                {
                    ImDrawCmdPtr cmd = drawList.CmdBuffer[i];
                    _drawArgs[0] = (int)cmd.ElemCount;                          // index count per instance
                    _drawArgs[2] = idxOf + (int)cmd.IdxOffset;                  // start index location
                    fixed (int* pDest = _drawArgs)
                    {
                        _argBuf.InternalSetNativeData((IntPtr)pDest, 0, argOf, 5, sizeof(int));
                    }

                    argOf += 5;                                                 // 5 int for each cmd
                }
                vtxOf += vtxArray.Length;
                idxOf += idxArray.Length;
            }
        }

        void CreateDrawCommands(CommandBuffer cmd, ImDrawDataPtr drawData, Vector2 fbSize)
        {
            var prevTextureId = System.IntPtr.Zero;
            var clipOffst = new Vector4(drawData.DisplayPos.x, drawData.DisplayPos.y, drawData.DisplayPos.x, drawData.DisplayPos.y);
            var clipScale = new Vector4(drawData.FramebufferScale.x, drawData.FramebufferScale.y, drawData.FramebufferScale.x, drawData.FramebufferScale.y);

            _material.SetBuffer(_verticesID, _vtxBuf);                          // bind vertex buffer

            cmd.SetViewport(new Rect(0f, 0f, fbSize.x, fbSize.y));
            cmd.SetViewProjectionMatrices(
                Matrix4x4.Translate(new Vector3(0.5f / fbSize.x, 0.5f / fbSize.y, 0f)), // small adjustment to improve text
                Matrix4x4.Ortho(0f, fbSize.x, fbSize.y, 0f, 0f, 1f));

            int vtxOf = 0;
            int argOf = 0;
            for (int n = 0, nMax = drawData.CmdListsCount; n < nMax; ++n)
            {
                ImDrawListPtr drawList = drawData.CmdLists[n];
                for (int i = 0, iMax = drawList.CmdBuffer.Size; i < iMax; ++i, argOf += 5 * 4)
                {
                    ImDrawCmdPtr drawCmd = drawList.CmdBuffer[i];
                    // TODO: user callback in drawCmd.UserCallback & drawCmd.UserCallbackData

                    // project scissor rectangle into framebuffer space and skip if fully outside
                    var clip = Vector4.Scale(drawCmd.ClipRect - clipOffst, clipScale);
                    if (clip.x >= fbSize.x || clip.y >= fbSize.y || clip.z < 0f || clip.w < 0f) continue;

                    if (prevTextureId != drawCmd.TextureId)
                        _properties.SetTexture(_texID, _texManager.GetTexture((int)(prevTextureId = drawCmd.TextureId)));

                    _properties.SetInt(_baseVertexID, vtxOf + (int)drawCmd.VtxOffset); // base vertex location not automatically added to SV_VertexID
                    cmd.EnableScissorRect(new Rect(clip.x, fbSize.y - clip.w, clip.z - clip.x, clip.w - clip.y)); // invert y
                    cmd.DrawProceduralIndirect(_idxBuf, Matrix4x4.identity, _material, -1, MeshTopology.Triangles, _argBuf, argOf, _properties);
                }
                vtxOf += drawList.VtxBuffer.Size;
            }
            cmd.DisableScissorRect();
        }
    }
}