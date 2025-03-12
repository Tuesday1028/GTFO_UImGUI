using System;

namespace ImNodesNET
{
	public static unsafe partial class ImNodes
	{
		public delegate void ImNodesMiniMapNodeHoveringCallback(int value, IntPtr ptr);

		public delegate IntPtr ImNodesMiniMapNodeHoveringCallbackUserData();
    }
}
