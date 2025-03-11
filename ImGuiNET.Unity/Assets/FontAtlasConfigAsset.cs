namespace ImGuiNET
{
    sealed class FontAtlasConfigAsset
    {
        public FontRasterizerType Rasterizer = FontRasterizerType.StbTrueType;
        public ImFreetype.FontBuilderFlags FontBuilderFlags = ImFreetype.FontBuilderFlags.None;
        public FontDefinition[] Fonts;
    }

    enum FontRasterizerType
    {
        StbTrueType,
        FreeType,
    }
}