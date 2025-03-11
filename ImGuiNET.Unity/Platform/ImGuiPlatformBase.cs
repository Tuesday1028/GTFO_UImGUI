using System.Runtime.CompilerServices;
using UnityEngine;

namespace ImGuiNET
{
    class ImGuiPlatformBase : IImGuiPlatform
    {
        protected readonly CursorShapesAsset _cursorShapes = new();                               // cursor shape definitions
        protected ImGuiMouseCursor _lastCursor = ImGuiMouseCursor.COUNT;                  // last cursor requested by ImGui

        protected readonly IniSettingsAsset _iniSettings;                                 // ini settings data

        protected readonly PlatformCallbacks _callbacks = new PlatformCallbacks
        {
            GetClipboardText = (_) => GUIUtility.systemCopyBuffer,
            SetClipboardText = (_, text) => GUIUtility.systemCopyBuffer = text,
            SetImeData = (_, viewport, data) =>
            {
                Input.imeCompositionMode = data.WantVisible ? IMECompositionMode.On : IMECompositionMode.Auto;
                Input.compositionCursorPos = data.InputPos;
            },
#if IMGUI_FEATURE_CUSTOM_ASSERT
            LogAssert = (condition, file, line) => Debug.LogError($"[DearImGui] Assertion failed: '{condition}', file '{file}', line: {line}."),
            DebugBreak = () => System.Diagnostics.Debugger.Break(),
#endif
        };

        protected ImGuiPlatformBase(CursorShapesAsset cursorShapes, IniSettingsAsset iniSettings)
        {
            _cursorShapes = cursorShapes;
            _iniSettings = iniSettings;
        }

        public virtual bool Initialize(ImGuiIOPtr io, ImGuiPlatformIOPtr platformio)
        {
            io.SetBackendPlatformName("Unity Input Manager");                   // setup backend info and capabilities
            io.BackendFlags |= ImGuiBackendFlags.HasMouseCursors;               // can honor GetMouseCursor() values
            io.BackendFlags &= ~ImGuiBackendFlags.HasSetMousePos;               // can't honor io.WantSetMousePos requests
                                                                                // io.BackendFlags |= ImGuiBackendFlags.HasGamepad;                 // set by UpdateGamepad()

            _callbacks.Assign(io, platformio);                                              // assign platform callbacks
            platformio.Platform_ClipboardUserData = IntPtr.Zero;

            if (_iniSettings != null)                                           // ini settings
            {
                io.SetIniFilename(null);                                        // handle ini saving manually
                ImGui.LoadIniSettingsFromMemory(_iniSettings.Load());           // call after CreateContext(), before first call to NewFrame()
            }

            return true;
        }

        public virtual void Shutdown(ImGuiIOPtr io, ImGuiPlatformIOPtr platformio)
        {
            io.SetBackendPlatformName(null);

            _callbacks.Unset(io, platformio);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void PrepareFrame(ImGuiIOPtr io, Rect displayRect)
        {
            io.DisplaySize = displayRect.size;// setup display size (every frame to accommodate for window resizing)
            // TODO: dpi aware, scale, etc

            io.DeltaTime = Time.unscaledDeltaTime;                              // setup timestep

            if (_iniSettings != null && io.WantSaveIniSettings)
            {
                _iniSettings.Save(ImGui.SaveIniSettingsToMemory());
                io.WantSaveIniSettings = false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void UpdateCursor(ImGuiIOPtr io, ImGuiMouseCursor cursor)
        {
            if (io.MouseDrawCursor)
                cursor = ImGuiMouseCursor.None;

            if (_lastCursor == cursor)
                return;
            if ((io.ConfigFlags & ImGuiConfigFlags.NoMouseCursorChange) != 0)
                return;

            _lastCursor = cursor;
            Cursor.visible = cursor != ImGuiMouseCursor.None;                   // hide cursor if ImGui is drawing it or if it wants no cursor
            if (_cursorShapes != null)
                Cursor.SetCursor(_cursorShapes[cursor].Texture, _cursorShapes[cursor].Hotspot, CursorMode.Auto);
        }
    }
}
