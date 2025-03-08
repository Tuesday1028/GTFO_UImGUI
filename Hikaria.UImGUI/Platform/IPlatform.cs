using ImGuiNET;
using UnityEngine;

namespace Hikaria.UImGUI.Platform
{
	/// <summary>
	/// Platform bindings for ImGui in Unity in charge of: mouse/keyboard/gamepad inputs, cursor shape, timing, windowing.
	/// </summary>
	internal interface IPlatform
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="io"></param>
		/// <returns></returns>
		bool Initialize(ImGuiIOPtr io, ImGuiPlatformIOPtr platformio, UIOConfig config, string platformName);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="io"></param>
		void Shutdown(ImGuiIOPtr io, ImGuiPlatformIOPtr platformio);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="io"></param>
		/// <param name="displayRect"></param>
		void PrepareFrame(ImGuiIOPtr io, Rect displayRect);
	}
}