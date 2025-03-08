using UnityEngine;

namespace Hikaria.UImGUI.Assets
{
	// TODO: Ability to save to asset, in player prefs with custom key, custom ini file, etc
	/// <summary>
	/// Used to store ImGui Ini settings in an asset instead of the default imgui.ini file
	/// </summary>
	internal sealed class IniSettingsAsset
	{
		//[TextArea(3, 20)]
		//[SerializeField]
		private string _data;

		//private string _iniPath;

		public void Save(string data)
		{
			_data = data;
		}

		public string Load()
		{
			return _data;
		}

		//public void SaveToDisk()
		//{
		//	ImGuiNET.ImGui.SaveIniSettingsToDisk(_iniPath);
		//}

		//public void LoadFromDisk()
		//{
		//	ImGuiNET.ImGui.LoadIniSettingsFromDisk(_iniPath)
		//}
	}
}
