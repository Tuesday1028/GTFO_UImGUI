using System;
using Hikaria.UImGUI.Texture;

namespace Hikaria.UImGUI
{
	internal sealed class Context
	{
		public IntPtr ImGuiContext;
		public IntPtr ImNodesContext;
		public IntPtr ImPlotContext;
		public TextureManager TextureManager;
	}
}