namespace ImGuiNET
{
    sealed class FontAtlasConfigAsset
    {
        public FontRasterizerType Rasterizer;
        public uint RasterizerFlags;
        public FontDefinition[] Fonts;
    }

    enum FontRasterizerType
    {
        StbTrueType,
        FreeType,
    }
}