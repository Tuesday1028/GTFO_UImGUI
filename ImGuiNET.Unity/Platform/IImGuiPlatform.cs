using UnityEngine;

namespace ImGuiNET
{
    /// <summary>
    /// Platform bindings for ImGui in Unity in charge of: mouse/keyboard/gamepad inputs, cursor shape, timing, windowing.
    /// </summary>
    interface IImGuiPlatform
    {
        bool Initialize(ImGuiIOPtr io, ImGuiPlatformIOPtr platformio);
        void Shutdown(ImGuiIOPtr io, ImGuiPlatformIOPtr platformio);
        void PrepareFrame(ImGuiIOPtr io, Rect displayRect);
    }
}