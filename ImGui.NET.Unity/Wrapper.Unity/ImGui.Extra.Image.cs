using System.Runtime.CompilerServices;
using UnityEngine;

namespace ImGuiNET
{
    // ImGui extra functionality related with Images
    public static partial class ImGuiUn
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Image(Texture tex)
        {
            ImGui.Image((IntPtr)GetTextureId(tex), tex.texelSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Image(Texture tex, Vector2 size)
        {
            ImGui.Image((IntPtr)GetTextureId(tex), size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Image(Sprite sprite)
        {
            SpriteInfo info = GetSpriteInfo(sprite);
            ImGui.Image((IntPtr)GetTextureId(info.texture), info.size, info.uv0, info.uv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Image(Sprite sprite, Vector2 size)
        {
            SpriteInfo info = GetSpriteInfo(sprite);
            ImGui.Image((IntPtr)GetTextureId(info.texture), size, info.uv0, info.uv1);
        }
    }
}