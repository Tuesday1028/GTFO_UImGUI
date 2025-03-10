using Object = UnityEngine.Object;

namespace ImGuiNET
{
    [Serializable]
    struct FontDefinition
    {
        Object _fontAsset; // to drag'n'drop file from the inspector
        public string FontPath;
        public FontConfig Config;
    }
}