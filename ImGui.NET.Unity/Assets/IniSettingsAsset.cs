namespace ImGuiNET
{
    // TODO: ability to save to asset, in player prefs with custom key, custom ini file, etc

    /// <summary>
    /// Used to store ImGui Ini settings in an asset instead of the default imgui.ini file
    /// </summary>
    sealed class IniSettingsAsset
    {
        string _data;

        public void Save(string data)
        {
            _data = data;
        }

        public string Load()
        {
            return _data;
        }
    }
}