using System.Runtime.InteropServices;

namespace ImGuiNET
{
    // TODO: should return Utf8 byte*, how to deal with memory ownership?
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate byte* GetClipboardTextCallback(void* user_data);
    delegate string GetClipboardTextSafeCallback(IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate void SetClipboardTextCallback(void* user_data, byte* text);
    delegate void SetClipboardTextSafeCallback(IntPtr user_data, string text);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate void SetImeDataCallback(void* user_data, void* viewport, void* data);
    delegate void SetImeDataSafeCallback(IntPtr user_data, ImGuiViewportPtr viewport, ImGuiPlatformImeDataPtr data);

#if IMGUI_FEATURE_CUSTOM_ASSERT
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    unsafe delegate void LogAssertCallback(byte* condition, byte* file, int line);
    delegate void LogAssertSafeCallback(string condition, string file, int line);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void DebugBreakCallback();

    unsafe struct CustomAssertData
    {
        public IntPtr LogAssertFn;
        public IntPtr DebugBreakFn;
    }
#endif

    unsafe class PlatformCallbacks
    {
        // fields to keep delegates from being collected by the garbage collector
        // after assigning its function pointers to unmanaged code
        GetClipboardTextCallback _getClipboardText;
        SetClipboardTextCallback _setClipboardText;
        SetImeDataCallback _setImeData;
#if IMGUI_FEATURE_CUSTOM_ASSERT
        LogAssertCallback _logAssert;
        DebugBreakCallback _debugBreak;
#endif

        public void Assign(ImGuiIOPtr io, ImGuiPlatformIOPtr platformio)
        {
#if ENABLE_IL2CPP
#else
            platformio.Platform_SetClipboardTextFn = Marshal.GetFunctionPointerForDelegate(_setClipboardText);
            platformio.Platform_GetClipboardTextFn = Marshal.GetFunctionPointerForDelegate(_getClipboardText);
            platformio.Platform_SetImeDataFn = Marshal.GetFunctionPointerForDelegate(_setImeData);
#endif



#if IMGUI_FEATURE_CUSTOM_ASSERT
            io.SetBackendPlatformUserData<CustomAssertData>(new CustomAssertData
            {
                LogAssertFn = Marshal.GetFunctionPointerForDelegate(_logAssert),
                DebugBreakFn = Marshal.GetFunctionPointerForDelegate(_debugBreak),
            });
#endif
        }

        public void Unset(ImGuiIOPtr io, ImGuiPlatformIOPtr platformio)
        {
            platformio.Platform_SetClipboardTextFn = IntPtr.Zero;
            platformio.Platform_GetClipboardTextFn = IntPtr.Zero;
            platformio.Platform_SetImeDataFn = IntPtr.Zero;
#if IMGUI_FEATURE_CUSTOM_ASSERT
            io.SetBackendPlatformUserData<CustomAssertData>(null);
#endif
        }

        public GetClipboardTextSafeCallback GetClipboardText
        {
            set => _getClipboardText = (user_data) =>
            {
                try
                {
                    string managedString = value(new IntPtr(user_data));
                    if (string.IsNullOrEmpty(managedString))
                        return null;

                    // memory leak here?
                    return (byte*)Marshal.StringToCoTaskMemUTF8(managedString).ToPointer();
                }
                catch (Exception ex)
                {
                    return null;
                }
            };
        }

        public SetClipboardTextSafeCallback SetClipboardText
        {
            set => _setClipboardText = (user_data, text) =>
            {
                try { value(new IntPtr(user_data), Util.StringFromPtr(text)); }
                catch (Exception ex) { }
            };
        }

        public SetImeDataSafeCallback SetImeData
        {
            set => _setImeData = (user_data, viewport, data) =>
            {
                try { value(new IntPtr(user_data), new ImGuiViewportPtr(new IntPtr(viewport)), new ImGuiPlatformImeDataPtr(new IntPtr(data))); }
                catch (Exception ex) { }
            };
        }

#if IMGUI_FEATURE_CUSTOM_ASSERT
        public LogAssertSafeCallback LogAssert
        {
            set => _logAssert = (condition, file, line) =>
            {
                try { value(Util.StringFromPtr(condition), Util.StringFromPtr(file), line); }
                catch (Exception ex) { Debug.LogException(ex); }
            };
        }

        public DebugBreakCallback DebugBreak
        {
            set => _debugBreak = () =>
            {
                try { value(); }
                catch (Exception ex) { Debug.LogException(ex); }
            };
        }
#endif
    }
}