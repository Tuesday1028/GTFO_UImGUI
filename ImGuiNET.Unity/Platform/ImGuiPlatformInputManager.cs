using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using System.Text;

#if USING_TMP
using TMPro;
#endif

namespace ImGuiNET
{
    // Implemented features:
    // [x] Platform: Clipboard support.
    // [x] Platform: Mouse cursor shape and visibility. Disable with io.ConfigFlags |= ImGuiConfigFlags.NoMouseCursorChange.
    // [x] Platform: Keyboard arrays indexed using KeyCode codes, e.g. ImGui.IsKeyPressed(KeyCode.Space).
    // [ ] Platform: Gamepad support. Enabled with io.ConfigFlags |= ImGuiConfigFlags.NavEnableGamepad.
    // [x] Platform: IME support.
    // [~] Platform: INI settings support.

    /// <summary>
    /// Platform bindings for ImGui in Unity in charge of: mouse/keyboard/gamepad inputs, cursor shape, timing, windowing.
    /// </summary>
    sealed class ImGuiPlatformInputManager : IImGuiPlatform
    {
        static ImGuiPlatformInputManager()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        readonly KeyCode[] _keyCodes = (KeyCode[])Enum.GetValues(typeof(KeyCode));

        readonly Event _textInputEvent = new Event();                                        // to get text input

        readonly CursorShapesAsset _cursorShapes;                               // cursor shape definitions
        ImGuiMouseCursor _lastCursor = ImGuiMouseCursor.COUNT;                  // last cursor requested by ImGui

        readonly IniSettingsAsset _iniSettings;                                 // ini settings data

        readonly PlatformCallbacks _callbacks = new PlatformCallbacks
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

        public ImGuiPlatformInputManager(CursorShapesAsset cursorShapes, IniSettingsAsset iniSettings)
        {
            _cursorShapes = cursorShapes;
            _iniSettings = iniSettings;
        }

        public bool Initialize(ImGuiIOPtr io, ImGuiPlatformIOPtr platformio)
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

        public void Shutdown(ImGuiIOPtr io, ImGuiPlatformIOPtr platformio)
        {
            _callbacks.Unset(io, platformio);
            io.SetBackendPlatformName(null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PrepareFrame(ImGuiIOPtr io, Rect displayRect)
        {
            io.DisplaySize = displayRect.size;// setup display size (every frame to accommodate for window resizing)
            // TODO: dpi aware, scale, etc

            io.DeltaTime = Time.unscaledDeltaTime;                              // setup timestep

            // input
            UpdateKeyboard(io);                                                 // update keyboard imGuiContext
            UpdateMouse(io);                                                    // update mouse imGuiContext
            UpdateCursor(io, ImGui.GetMouseCursor());                           // update Unity cursor with the cursor requested by ImGui

            // ini settings
            if (_iniSettings != null && io.WantSaveIniSettings)
            {
                _iniSettings.Save(ImGui.SaveIniSettingsToMemory());
                io.WantSaveIniSettings = false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool TryMapKeys(KeyCode key, out ImGuiKey imguikey)
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static ImGuiKey KeyToImGuiKeyShortcut(KeyCode keyToConvert, KeyCode startKey1, ImGuiKey startKey2)
            {
                int changeFromStart1 = (int)keyToConvert - (int)startKey1;
                return startKey2 + changeFromStart1;
            }

            imguikey = key switch
            {
                >= KeyCode.F1 and <= KeyCode.F12 => KeyToImGuiKeyShortcut(key, KeyCode.F1, ImGuiKey.F1),
                >= KeyCode.Keypad0 and <= KeyCode.Keypad9 => KeyToImGuiKeyShortcut(key, KeyCode.Keypad0, ImGuiKey.Keypad0),
                >= KeyCode.A and <= KeyCode.Z => KeyToImGuiKeyShortcut(key, KeyCode.A, ImGuiKey.A),
                >= KeyCode.Alpha0 and <= KeyCode.Alpha9 => KeyToImGuiKeyShortcut(key, KeyCode.Alpha0, ImGuiKey._0),

                KeyCode.LeftShift => ImGuiKey.LeftShift,
                KeyCode.RightShift => ImGuiKey.RightShift,
                KeyCode.LeftControl => ImGuiKey.LeftCtrl,
                KeyCode.RightControl => ImGuiKey.RightCtrl,
                KeyCode.LeftAlt => ImGuiKey.LeftAlt,
                KeyCode.RightAlt => ImGuiKey.RightAlt,
                KeyCode.LeftWindows => ImGuiKey.LeftSuper,
                KeyCode.RightWindows => ImGuiKey.RightSuper,

                KeyCode.Menu => ImGuiKey.Menu,
                KeyCode.UpArrow => ImGuiKey.UpArrow,
                KeyCode.DownArrow => ImGuiKey.DownArrow,
                KeyCode.LeftArrow => ImGuiKey.LeftArrow,
                KeyCode.RightArrow => ImGuiKey.RightArrow,
                KeyCode.Return => ImGuiKey.Enter,
                KeyCode.Escape => ImGuiKey.Escape,
                KeyCode.Space => ImGuiKey.Space,
                KeyCode.Tab => ImGuiKey.Tab,
                KeyCode.Backspace => ImGuiKey.Backspace,
                KeyCode.Insert => ImGuiKey.Insert,
                KeyCode.Delete => ImGuiKey.Delete,
                KeyCode.PageUp => ImGuiKey.PageUp,
                KeyCode.PageDown => ImGuiKey.PageDown,
                KeyCode.Home => ImGuiKey.Home,
                KeyCode.End => ImGuiKey.End,
                KeyCode.CapsLock => ImGuiKey.CapsLock,
                KeyCode.ScrollLock => ImGuiKey.ScrollLock,
                KeyCode.Print => ImGuiKey.PrintScreen,
                KeyCode.Pause => ImGuiKey.Pause,
                KeyCode.Numlock => ImGuiKey.NumLock,
                KeyCode.KeypadDivide => ImGuiKey.KeypadDivide,
                KeyCode.KeypadMultiply => ImGuiKey.KeypadMultiply,
                KeyCode.KeypadMinus => ImGuiKey.KeypadSubtract,
                KeyCode.KeypadPlus => ImGuiKey.KeypadAdd,
                KeyCode.KeypadPeriod => ImGuiKey.KeypadDecimal,
                KeyCode.KeypadEnter => ImGuiKey.KeypadEnter,
                KeyCode.KeypadEquals => ImGuiKey.KeypadEqual,
                KeyCode.Tilde => ImGuiKey.GraveAccent,
                KeyCode.Minus => ImGuiKey.Minus,
                KeyCode.Plus => ImGuiKey.Equal,
                KeyCode.LeftBracket => ImGuiKey.LeftBracket,
                KeyCode.RightBracket => ImGuiKey.RightBracket,
                KeyCode.Semicolon => ImGuiKey.Semicolon,
                KeyCode.Quote => ImGuiKey.Apostrophe,
                KeyCode.Comma => ImGuiKey.Comma,
                KeyCode.Period => ImGuiKey.Period,
                KeyCode.Slash => ImGuiKey.Slash,
                KeyCode.Backslash => ImGuiKey.Backslash,
                _ => ImGuiKey.None
            };

            return imguikey != ImGuiKey.None;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void UpdateKeyboard(ImGuiIOPtr io)
        {
            foreach (KeyCode keyCode in _keyCodes)
            {
                if (TryMapKeys(keyCode, out ImGuiKey imguikey))
                {
                    io.AddKeyEvent(imguikey, Input.GetKey(keyCode));
                }
            }

            io.AddKeyEvent(ImGuiKey.ModCtrl, Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl));
            io.AddKeyEvent(ImGuiKey.ModAlt, Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt));
            io.AddKeyEvent(ImGuiKey.ModShift, Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
            io.AddKeyEvent(ImGuiKey.ModSuper, Input.GetKey(KeyCode.LeftWindows) || Input.GetKey(KeyCode.RightWindows));

#if USING_TMP
            var isAnyInputSelected = EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null &&
                                      (EventSystem.current.currentSelectedGameObject.GetComponent<InputField>() != null ||
                                       EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>() != null);
#else
            var isAnyInputSelected =  EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null &&
                                      EventSystem.current.currentSelectedGameObject.GetComponent<InputField>() != null;
#endif

            // Text input.
            while (!isAnyInputSelected && Event.PopEvent(_textInputEvent))
            {
                if (_textInputEvent.rawType == EventType.KeyDown &&
                    _textInputEvent.character != 0 && _textInputEvent.character != '\n')
                {
                    io.AddInputCharacter(_textInputEvent.character);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void UpdateMouse(ImGuiIOPtr io)
        {
            Vector2 mousePosition = ImGuiUn.ScreenToImGui(Input.mousePosition);
            io.AddMousePosEvent(mousePosition.x, mousePosition.y);
            io.AddMouseButtonEvent(0, Input.GetMouseButton(0));
            io.AddMouseButtonEvent(1, Input.GetMouseButton(1));
            io.AddMouseButtonEvent(2, Input.GetMouseButton(2));
            io.AddMouseWheelEvent(Input.mouseScrollDelta.x, Input.mouseScrollDelta.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void UpdateCursor(ImGuiIOPtr io, ImGuiMouseCursor cursor)
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
                Cursor.SetCursor(_cursorShapes[cursor].texture, _cursorShapes[cursor].hotspot, CursorMode.Auto);
        }
    }
}