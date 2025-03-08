using TheArchive.Core;
using TheArchive.Core.Attributes;
using TheArchive.Core.FeaturesAPI;

namespace Hikaria.UImGUI
{
    [ArchiveModule(PluginInfo.GUID, PluginInfo.NAME, PluginInfo.VERSION)]
    internal class EntryPoint : IArchiveModule
    {
        public bool ApplyHarmonyPatches => false;

        public bool UsesLegacyPatches => false;

        public ArchiveLegacyPatcher Patcher { get; set; }

        public string ModuleGroup => FeatureGroups.GetOrCreateModuleGroup("UImGUI");

        public void Init()
        {
            Logs.LogMessage("OK");
        }

        public void OnExit()
        {
        }

        public void OnLateUpdate()
        {
        }

        public void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
        }
    }
}
