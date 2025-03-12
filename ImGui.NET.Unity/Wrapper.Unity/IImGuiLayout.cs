namespace ImGuiNET
{
    public interface IImGuiLayout
    {
        string GuiName { get; }
        bool ShouldGuiRender { get; }
        bool ShouldGuiBlockGameInput { get; }

        /// <summary>
        ///     Always invokes before the ImGui layout is done, even if <see cref="UImGui.ShouldRender"/> is false.
        /// </summary>
        void OnGuiUpdate();

        void OnGuiLayout();
    }

}
