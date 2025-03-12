using System.ComponentModel;
using UnityEngine;

namespace ImGuiNET
{
    [Serializable]
    internal struct IOConfig
    {
        [Description("See ImGuiConfigFlags_ enum. Set by user/application. Keyboard/Gamepad navigation options, etc.")]
        public ImGuiConfigFlags ConfigFlags;

        // Font system
        [Description("Global scale all fonts. (default=1.0f)")]
        public float FontGlobalScale;
        [Description("For retina display or other situations where window coordinates are different from framebuffer coordinates. This generally ends up in ImDrawData::FramebufferScale.")]
        public Vector2 DisplayFramebufferScale;
        [Description("Path to .ini file (important: default \"imgui.ini\" is relative to current working dir!). Set NULL to disable automatic .ini loading/saving or if you want to manually call LoadIniSettingsXXX() / SaveIniSettingsXXX() functions.")]
        public string IniFilename;
        [Description("Path to .log file (default parameter to ImGui::LogToFile when no file is specified).")]
        public string LogFilename;

        // Miscellaneous options
        [Description("Request ImGui to draw a mouse cursor for you (if you are on a platform without a mouse cursor). " +
            "Cannot be easily renamed to 'io.ConfigXXX' because this is frequently used by backend implementations. " +
            "(default=false)")]
        public bool DrawCursor;
        [Description("Enable input queue trickling: some types of events submitted during the same frame (e.g. button down + up) will be spread over multiple frames, improving interactions with low framerates.")]
        public bool InputTrickleEventQueue;
        [Description("Enable blinking cursor (optional as some users consider it to be distracting).")]
        public bool InputTextCursorBlink;
        [Description("[BETA] Pressing Enter will keep item active and select contents (single-line only).")]
        public bool InputTextEnterKeepActive;
        [Description("[BETA] Enable turning DragXXX widgets into text input with a simple mouse click-release (without moving). Not desirable on devices without a keyboard.")]
        public bool DragClickToInputText;
        [Description("Enable resizing of windows from their edges and from the lower-left corner. This requires ImGuiBackendFlags_HasMouseCursors for better mouse cursor feedback. (This used to be a per-window ImGuiWindowFlags_ResizeFromAnySide flag)")]
        public bool WindowsResizeFromEdges;
        [Description("Enable allowing to move windows only when clicking on their title bar. Does not apply to windows without a title bar.")]
        public bool WindowsMoveFromTitleBarOnly;
        [Description("[EXPERIMENTAL] CTRL+C copy the contents of focused window into the clipboard. Experimental because: (1) has known issues with nested Begin/End pairs (2) text output quality varies (3) text output is in submission order rather than spatial order.")]
        public bool WindowsCopyContentsWithCtrlC;
        [Description("Enable scrolling page by page when clicking outside the scrollbar grab. When disabled, always scroll to clicked location. When enabled, Shift+Click scrolls to clicked location.")]
        public bool ScrollbarScrollByPage;
        [Description("Timer (in seconds) to free transient windows/tables memory buffers when unused. Set to -1.0f to disable.")]
        public float MemoryCompactTimer;

        // Inputs Behaviors
        [Description("Time for a double-click, in seconds. (default=0.30f)")]
        public float DoubleClickTime;
        [Description("Distance threshold to stay in to validate a double-click, in pixels. (default=6.0f)")]
        public float DoubleClickMaxDist;
        [Description("Distance threshold before considering we are dragging. (default=6.0f)")]
        public float DragThreshold;
        [Description("When holding a key/button, time before it starts repeating, in seconds (for buttons in Repeat mode, etc.).")]
        public float KeyRepeatDelay;
        [Description("When holding a key/button, rate at which it repeats, in seconds.")]
        public float KeyRepeatRate;

        // Docking options (when ImGuiConfigFlags_DockingEnable is set)
        [Description("Simplified docking mode: disable window splitting, so docking is limited to merging multiple windows together into tab-bars.")]
        public bool DockingNoSplit;
        [Description("Enable docking with holding Shift key (reduce visual noise, allows dropping in wider space)")]
        public bool DockingWithShift;
        [Description("[BETA] [FIXME: This currently creates regression with auto-sizing and general overhead] Make every single floating window display within a docking node.")]
        public bool DockingAlwaysTabBar;
        [Description("[BETA] Make window or viewport transparent when docking and only display docking boxes on the target viewport. Useful if rendering of multiple viewport cannot be synced. Best used with ConfigViewportsNoAutoMerge.")]
        public bool DockingTransparentPayload;

        [Description("Store your own data.")]
        [NonSerialized]
        public IntPtr UserData;

        public IOConfig()
        {
            ConfigFlags = ImGuiConfigFlags.DockingEnable;

            IniFilename = "imgui.ini";
            LogFilename = "imgui_log.txt";

            FontGlobalScale = 1.0f;
            DisplayFramebufferScale = Vector2.one;

            DrawCursor = false;
            InputTrickleEventQueue = true;
            InputTextCursorBlink = true;
            InputTextEnterKeepActive = false;
            DragClickToInputText = false;
            WindowsResizeFromEdges = true;
            WindowsMoveFromTitleBarOnly = false;
            WindowsCopyContentsWithCtrlC = false;
            ScrollbarScrollByPage = true;
            MemoryCompactTimer = 60f;

            DoubleClickTime = 0.30f;
            DoubleClickMaxDist = 6.0f;
            DragThreshold = 6.0f;
            KeyRepeatDelay = 0.275f;
            KeyRepeatRate = 0.05f;

            DockingNoSplit = false;
            DockingWithShift = true;
            DockingAlwaysTabBar = false;
            DockingTransparentPayload = false;

            UserData = IntPtr.Zero;
        }

        public void SetDefaults()
        {
            IntPtr context = ImGui.CreateContext();
            ImGui.SetCurrentContext(context);
            SetFrom(ImGui.GetIO());
            ImGui.DestroyContext(context);
        }

        public void ApplyTo(ImGuiIOPtr io)
        {
            io.ConfigFlags = ConfigFlags;
            io.FontGlobalScale = FontGlobalScale;

            io.FontGlobalScale = FontGlobalScale;
            io.DisplayFramebufferScale = DisplayFramebufferScale;

            io.MouseDoubleClickTime = DoubleClickTime;
            io.MouseDoubleClickMaxDist = DoubleClickMaxDist;
            io.MouseDragThreshold = DragThreshold;
            io.KeyRepeatDelay = KeyRepeatDelay;
            io.KeyRepeatRate = KeyRepeatRate;

            io.MouseDrawCursor = DrawCursor;
            io.ConfigInputTrickleEventQueue = InputTrickleEventQueue;
            io.ConfigInputTextCursorBlink = InputTextCursorBlink;
            io.ConfigInputTextEnterKeepActive = InputTextEnterKeepActive;
            io.ConfigDragClickToInputText = DragClickToInputText;
            io.ConfigWindowsResizeFromEdges = WindowsResizeFromEdges;
            io.ConfigWindowsMoveFromTitleBarOnly = WindowsMoveFromTitleBarOnly;
            io.ConfigWindowsCopyContentsWithCtrlC = WindowsCopyContentsWithCtrlC;
            io.ConfigScrollbarScrollByPage = ScrollbarScrollByPage;
            io.ConfigMemoryCompactTimer = MemoryCompactTimer;

            io.ConfigDockingNoSplit = DockingNoSplit;
            io.ConfigDockingWithShift = DockingWithShift;
            io.ConfigDockingAlwaysTabBar = DockingAlwaysTabBar;
            io.ConfigDockingTransparentPayload = DockingTransparentPayload;

            io.UserData = UserData;
        }

        public void SetFrom(ImGuiIOPtr io)
        {
            ConfigFlags = io.ConfigFlags;
            FontGlobalScale = io.FontGlobalScale;

            FontGlobalScale = io.FontGlobalScale;
            DisplayFramebufferScale = io.DisplayFramebufferScale;

            DoubleClickTime = io.MouseDoubleClickTime;
            DoubleClickMaxDist = io.MouseDoubleClickMaxDist;
            DragThreshold = io.MouseDragThreshold;
            KeyRepeatDelay = io.KeyRepeatDelay;
            KeyRepeatRate = io.KeyRepeatRate;

            DrawCursor = io.MouseDrawCursor;
            InputTrickleEventQueue = io.ConfigInputTrickleEventQueue;
            InputTextCursorBlink = io.ConfigInputTextCursorBlink;
            InputTextEnterKeepActive = io.ConfigInputTextEnterKeepActive;
            WindowsResizeFromEdges = io.ConfigWindowsResizeFromEdges;
            WindowsMoveFromTitleBarOnly = io.ConfigWindowsMoveFromTitleBarOnly;
            WindowsCopyContentsWithCtrlC = io.ConfigWindowsCopyContentsWithCtrlC;
            ScrollbarScrollByPage = io.ConfigScrollbarScrollByPage;
            MemoryCompactTimer = io.ConfigMemoryCompactTimer;

            DockingNoSplit = io.ConfigDockingNoSplit;
            DockingWithShift = io.ConfigDockingWithShift;
            DockingAlwaysTabBar = io.ConfigDockingAlwaysTabBar;
            DockingTransparentPayload = io.ConfigDockingTransparentPayload;

            UserData = io.UserData;
        }
    }
}