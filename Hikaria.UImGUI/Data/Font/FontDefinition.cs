using System.ComponentModel;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Hikaria.UImGUI
{

	internal struct FontDefinition
	{
		//[SerializeField]
		private Object _fontAsset;

		[Description("Path relative to Application.streamingAssetsPath.")]
		public string Path;
		public FontConfig Config;

	}
}
