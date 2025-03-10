using Hikaria.UImGUI;
using UnityEngine;

namespace ImGuiNET
{
    sealed class ShaderResourcesAsset
    {
        [Serializable]
        public class Shaders
        {
            public Shader mesh = AssetsHelper.GetAssets<Shader>("assets/resources/unityimgui/shaders/dearimgui-mesh.shader", false);
            public Shader procedural = AssetsHelper.GetAssets<Shader>("assets/resources/unityimgui/shaders/dearimgui-procedural.shader", false);

            public Shaders Clone()
            {
                return (Shaders)MemberwiseClone();
            }
        }

        [Serializable]
        public class PropertyNames
        {
            public string texture = "_Texture";
            public string vertices = "_Vertices";
            public string baseVertex = "_BaseVertex";

            public PropertyNames Clone()
            {
                return (PropertyNames)MemberwiseClone();
            }
        }

        public Shaders shaders = new();
        public PropertyNames propertyNames = new();
    }
}
