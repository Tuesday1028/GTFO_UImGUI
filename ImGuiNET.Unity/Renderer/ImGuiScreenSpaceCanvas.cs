using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.UI;

#if USING_URP
using UnityEngine.Rendering.Universal;
#endif

namespace ImGuiNET
{
    internal sealed class ImGuiScreenSpaceCanvas : MonoBehaviour
    {
        private Camera myCamera;
        private RenderTexture renderTexture;

        private Canvas canvas;
        
        public void Setup()
        {
            var myCameraObject = new GameObject("Screen-Space ImGui Camera")
            {
                hideFlags = HideFlags.DontSave
            };
            myCameraObject.transform.SetParent(transform);
            
            myCamera = myCameraObject.AddComponent<Camera>();
            myCamera.enabled = false;
            myCamera.clearFlags = CameraClearFlags.SolidColor;
            myCamera.backgroundColor = Color.clear;
            myCamera.cullingMask = 0;
            myCamera.depth = float.MaxValue;
            myCamera.orthographic = true;
            myCamera.orthographicSize = 1f;
            myCamera.nearClipPlane = 0.3f;
            myCamera.farClipPlane = 1000f;
            myCamera.useOcclusionCulling = false;

            var srpType = RenderUtils.GetSRP();
            switch (srpType)
            {
                case SRPType.BuiltIn:
                    break;
                case SRPType.URP:
#if USING_URP
                    // Initialize the camera for URP
                    var data = myCamera.GetUniversalAdditionalCameraData();
                    data.SetRenderer(DearImGui.Instance.toTextureRendererIndex);
#endif
                    break;
                case SRPType.HDRP:
#if USING_HDRP
                    // Initialize the camera for HDRP
                    // TODO
#endif
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            // Create the render texture
            myCamera.targetTexture = renderTexture = new RenderTexture(Screen.width, Screen.height, 0, GraphicsFormat.R32G32B32A32_SFloat)
            {
                name = "ImGui Screen-Space Canvas",
                hideFlags = HideFlags.DontSave
            };
            
            var rt = RenderTexture.active;
            RenderTexture.active = renderTexture;
            GL.Clear(true, true, Color.clear);
            RenderTexture.active = rt;

            var buffer = UImGui.RenderCommandBuffer;
            if (buffer == null)
                throw new NullReferenceException("buffer == null");
            myCamera.AddCommandBuffer(CameraEvent.AfterEverything, buffer);
            
            // Create canvas.
            var canvasObject = new GameObject("Screen-Space ImGui Canvas")
            {
                hideFlags = HideFlags.DontSave
            };
            canvasObject.transform.SetParent(transform);
            
            canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = short.MaxValue - 1;
            canvas.additionalShaderChannels = AdditionalCanvasShaderChannels.None;
            
            var canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
            
            // Create RawImage.
            var rawImageObject = new GameObject("Screen-Space ImGui RawImage")
            {
                hideFlags = HideFlags.DontSave
            };
            rawImageObject.transform.SetParent(canvasObject.transform);
            rawImageObject.transform.localPosition = Vector3.zero;
            rawImageObject.transform.localScale = Vector3.one;
            
            // Stretch the RawImage to fit the screen.
            var rectTransform = rawImageObject.AddComponent<RectTransform>();
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.sizeDelta = Vector2.zero;
            rectTransform.anchoredPosition = Vector2.zero;
            
            var rawImage = rawImageObject.AddComponent<RawImage>();
            rawImage.texture = renderTexture;
            rawImage.color = Color.white;
            rawImage.raycastTarget = false;
        }

        private void OnDestroy()
        {
            if (myCamera != null)
                Destroy(myCamera.gameObject);
            
            if (renderTexture != null)
                Destroy(renderTexture);

            myCamera = default;
            renderTexture = default;
        }

        private void Update()
        {
            if (!UImGui.ShouldRender)
            {
                canvas.enabled = false;
                return;
            }
            
            // Validate if render texture size is valid, if not, resize it
            if (renderTexture.width != Screen.width || renderTexture.height != Screen.height)
            {
                renderTexture.Release();
                renderTexture.width = Screen.width;
                renderTexture.height = Screen.height;
            }
            else
            {
                // myCamera.Render();
                FixedRateRender();
            }
        }

        // NOTE: Imgui does not require high refresh rate experience, so we can save some performance on this.
        private float lastTime;
        private void FixedRateRender()
        {
            var time = Time.unscaledTime;
            var delta = time - lastTime;
            if (delta < 1f / Application.targetFrameRate)
                return;
            
            lastTime = time;
            myCamera.Render();
            canvas.enabled = true;
        }

        public Camera GetCamera()
        {
            return myCamera;
        }
    }
}