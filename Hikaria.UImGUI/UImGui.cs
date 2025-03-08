using Hikaria.UImGUI.Assets;
using Hikaria.UImGUI.Platform;
using Hikaria.UImGUI.Renderer;
using Il2CppInterop.Runtime.Attributes;
using ImGuiNET;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering;

namespace Hikaria.UImGUI
{
	// TODO: Check Multithread run.
	public class UImGui : MonoBehaviour
	{
        public static UImGui Current { get; private set; }

		private Context _context;
		private IRenderer _renderer;
		private IPlatform _platform;
		private CommandBuffer _renderCommandBuffer;

		//[SerializeField]
		private Camera _camera = null;

		//[SerializeField]
		private RenderImGui _renderFeature = null;

		//[SerializeField]
		private RenderType _rendererType = RenderType.Procedural;

		//[SerializeField]
		private Platform.InputType _platformType = Platform.InputType.InputManager;

		[Description("Null value uses default imgui.ini file.")]
		//[SerializeField]
		private IniSettingsAsset _iniSettings = null;

		//[Header("Configuration")]

		//[SerializeField]
		private UIOConfig _initialConfiguration = new UIOConfig
		{
			ImGuiConfig = ImGuiConfigFlags.NavEnableKeyboard | ImGuiConfigFlags.DockingEnable,

			DoubleClickTime = 0.30f,
			DoubleClickMaxDist = 6.0f,

			DragThreshold = 6.0f,

			KeyRepeatDelay = 0.250f,
			KeyRepeatRate = 0.050f,

			FontGlobalScale = 1.0f,
			FontAllowUserScaling = false,

			DisplayFramebufferScale = Vector2.one,

			MouseDrawCursor = false,
			TextCursorBlink = false,

			ResizeFromEdges = true,
			MoveFromTitleOnly = true,
			ConfigMemoryCompactTimer = 1f,
		};

        //[SerializeField]
        private FontAtlasConfigAsset _fontAtlasConfiguration = null;

		//[Header("Customization")]
		//[SerializeField]
		private ShaderResourcesAsset _shaders = null;

		//[SerializeField]
		private StyleAsset _style = null;

		//[SerializeField]
		private CursorShapesAsset _cursorShapes = null;

		//[SerializeField]
		private bool _urpRenderGraphBypass = false;

		//[SerializeField]
		private bool _doGlobalEvents = true; // Do global/default Layout event too.

		private bool _isChangingCamera = false;

		public CommandBuffer CommandBuffer => _renderCommandBuffer;

		#region Events
		public event Action<UImGui> Layout;
		public event Action<UImGui> OnInitialize;
		public event Action<UImGui> OnDeinitialize;
		#endregion

		public void Reload()
		{
			OnDisable();
			OnEnable();
		}

        [HideFromIl2Cpp]
        public void SetUserData(IntPtr userDataPtr)
		{
			_initialConfiguration.UserData = userDataPtr;
			ImGuiIOPtr io = ImGui.GetIO();
			_initialConfiguration.ApplyTo(io);
		}

        [HideFromIl2Cpp]
        public void SetCamera(Camera camera)
		{
			if (camera == null)
			{
				enabled = false;
				throw new Exception($"Fail: {camera} is null.");
			}

			if (camera == _camera)
			{
				Debug.LogWarning($"Trying to change to same camera. Camera: {camera}", camera);
				return;
			}

			_camera = camera;
			_isChangingCamera = true;
		}

		private void Awake()
		{
			Current = this;
            _context = UImGuiUtility.CreateContext();
		}

		private void OnEnable()
		{
			void Fail(string reason)
			{
				enabled = false;
				throw new Exception($"Failed to start: {reason}.");
			}

            if (_camera == null)
			{
				Fail(nameof(_camera));
			}

			if (!_urpRenderGraphBypass && _renderFeature == null && RenderUtility.IsUsingURP())
			{
				Fail(nameof(_renderFeature));
			}

			_renderCommandBuffer = RenderUtility.GetCommandBuffer(Constants.UImGuiCommandBuffer);

			if (RenderUtility.IsUsingURP() && !_urpRenderGraphBypass)
			{
#if HAS_URP
				_renderFeature.Camera = _camera;
#endif
				_renderFeature.CommandBuffer = _renderCommandBuffer;
			}
			else if (!RenderUtility.IsUsingHDRP())
			{
				_camera.AddCommandBuffer(CameraEvent.AfterEverything, _renderCommandBuffer);
			}

			UImGuiUtility.SetCurrentContext(_context);

			ImGuiIOPtr io = ImGui.GetIO();
			ImGuiPlatformIOPtr platformio = ImGui.GetPlatformIO();

            _initialConfiguration.ApplyTo(io);
			_style?.ApplyTo(ImGui.GetStyle());

			_context.TextureManager.BuildFontAtlas(io, _fontAtlasConfiguration);
			_context.TextureManager.Initialize(io);

			IPlatform platform = PlatformUtility.Create(_platformType, _cursorShapes, _iniSettings);
			SetPlatform(platform, io, platformio);
			if (_platform == null)
			{
				Fail(nameof(_platform));
			}

            _shaders ??= new()
            {
                Shader = new()
                {
                    Procedural = AssetsHelper.GetAssets<Shader>("assets/resources/unityimgui/shaders/dearimgui-procedural.shader", false),
                    Mesh = AssetsHelper.GetAssets<Shader>("assets/resources/unityimgui/shaders/dearimgui-mesh.shader", false)
                },
                PropertyNames = new()
                {
                    Texture = "_Texture",
                    Vertices = "_Vertices",
                    BaseVertex = "_BaseVertex"
                }
            };

            SetRenderer(RenderUtility.Create(_rendererType, _shaders, _context.TextureManager), io);
			if (_renderer == null)
			{
				Fail(nameof(_renderer));
			}

			if (_doGlobalEvents)
			{
				UImGuiUtility.DoOnInitialize(this);
			}
			OnInitialize?.Invoke(this);
		}

		private void OnDisable()
		{
			UImGuiUtility.SetCurrentContext(_context);
			ImGuiIOPtr io = ImGui.GetIO();
			ImGuiPlatformIOPtr platformio = ImGui.GetPlatformIO();

			SetRenderer(null, io);
			SetPlatform(null, io, platformio);

			UImGuiUtility.SetCurrentContext(null);

			_context.TextureManager.Shutdown();
			_context.TextureManager.DestroyFontAtlas(io);

			if (RenderUtility.IsUsingURP())
			{
				if (_renderFeature != null)
				{
#if HAS_URP
					_renderFeature.Camera = null;
#endif
					_renderFeature.CommandBuffer = null;
				}
			}
			else if (!RenderUtility.IsUsingHDRP())
			{
				if (_camera != null)
				{
					_camera.RemoveCommandBuffer(CameraEvent.AfterEverything, _renderCommandBuffer);
				}
			}

			if (_renderCommandBuffer != null)
			{
				RenderUtility.ReleaseCommandBuffer(_renderCommandBuffer);
			}

			_renderCommandBuffer = null;

			if (_doGlobalEvents)
			{
				UImGuiUtility.DoOnDeinitialize(this);
			}
			OnDeinitialize?.Invoke(this);
		}

		private void Update()
		{
			if (RenderUtility.IsUsingHDRP())
				return; // skip update call in hdrp
			DoUpdate(CommandBuffer);
		}

        [HideFromIl2Cpp]
        internal void DoUpdate(CommandBuffer buffer)
		{
			UImGuiUtility.SetCurrentContext(_context);
			ImGuiIOPtr io = ImGui.GetIO();

			Constants.PrepareFrameMarker.Begin(this);
			_context.TextureManager.PrepareFrame(io);
			_platform.PrepareFrame(io, _camera.pixelRect);
			ImGui.NewFrame();
#if !UIMGUI_REMOVE_IMGUIZMO
			ImGuizmoNET.ImGuizmo.BeginFrame();
#endif
			Constants.PrepareFrameMarker.End();

			Constants.LayoutMarker.Begin(this);
			try
			{
				if (_doGlobalEvents)
				{
					UImGuiUtility.DoLayout(this);
				}

				Layout?.Invoke(this);
			}
			finally
			{
				ImGui.Render();
				Constants.LayoutMarker.End();
			}

			Constants.DrawListMarker.Begin(this);
			_renderCommandBuffer.Clear();
			_renderer.RenderDrawLists(buffer, ImGui.GetDrawData());
			Constants.DrawListMarker.End();

			if (_isChangingCamera)
			{
				_isChangingCamera = false;
				Reload();
			}
		}

        [HideFromIl2Cpp]
        private void SetRenderer(IRenderer renderer, ImGuiIOPtr io)
		{
			_renderer?.Shutdown(io);
			_renderer = renderer;
			_renderer?.Initialize(io);
		}

        [HideFromIl2Cpp]
        private void SetPlatform(IPlatform platform, ImGuiIOPtr io, ImGuiPlatformIOPtr platformio)
		{
			_platform?.Shutdown(io, platformio);
			_platform = platform;
			_platform?.Initialize(io, platformio, _initialConfiguration, "Unity " + _platformType.ToString());
        }

		void Start()
		{
			//if (_urpRenderGraphBypass)
			//	RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
		}

		private void OnDestroy()
		{
			UImGuiUtility.DestroyContext(_context);
			//if (_urpRenderGraphBypass)
			//	RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
		}

        [HideFromIl2Cpp]
        private void OnEndCameraRendering(ScriptableRenderContext context, Camera cam)
		{
			if (cam != _camera)
				return;
			Graphics.ExecuteCommandBuffer(CommandBuffer);
		}
	}
}