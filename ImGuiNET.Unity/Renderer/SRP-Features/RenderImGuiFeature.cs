﻿using UnityEngine.Rendering;

#if USING_URP
using UnityEngine.Rendering.Universal;
#endif

#if USING_URP
namespace ImGuiNET
{
    public class RenderImGuiFeature : ScriptableRendererFeature
    {
        class ExecuteCommandBufferPass : ScriptableRenderPass
        {
            public CommandBuffer cmd;

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                var buffer = cmd;
                context.ExecuteCommandBuffer(buffer);
            }
        }

        ExecuteCommandBufferPass _executeCommandBufferPass;

        public CommandBuffer commandBuffer;
        public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;

        public override void Create()
        {
            _executeCommandBufferPass = new ExecuteCommandBufferPass()
            {
                cmd = commandBuffer,
                renderPassEvent = renderPassEvent,
            };
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if (commandBuffer == null) return;
            _executeCommandBufferPass.renderPassEvent = renderPassEvent;
            _executeCommandBufferPass.cmd = commandBuffer;
            renderer.EnqueuePass(_executeCommandBufferPass);
        }
    }
}
#else
namespace ImGuiNET
{
    public class RenderImGuiFeature
    {
        public CommandBuffer commandBuffer;
    }
}
#endif