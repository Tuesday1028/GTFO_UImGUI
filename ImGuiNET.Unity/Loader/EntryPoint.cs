using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;

namespace Hikaria.UImGUI
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.NAME, PluginInfo.VERSION)]
    internal class EntryPoint : BasePlugin
    {
        public override void Load()
        {
            Logs.Init(Log);

            UImGUIHooks.Init();

            m_Harmony = new Harmony(PluginInfo.GUID);
            m_Harmony.PatchAll();

            Logs.LogMessage("OK");
        }

        private Harmony m_Harmony;
    }
}
