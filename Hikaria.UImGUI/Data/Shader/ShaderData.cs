using UnityEngine;

namespace Hikaria.UImGUI
{
	internal class ShaderData
	{
		public Shader Mesh;
		public Shader Procedural;

		public ShaderData Clone()
		{
			return (ShaderData)MemberwiseClone();
		}
	}
}
