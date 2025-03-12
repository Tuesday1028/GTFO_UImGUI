using BepInEx.Logging;

namespace Hikaria.UImGUI
{
    internal static class Logs
    {
        private static ManualLogSource _logSource;

        public static void Init(ManualLogSource logSource)
        {
            _logSource = logSource;
        }

        public static void LogDebug(object data)
        {
            _logSource.LogDebug(data.ToString());
        }

        public static void LogError(object data)
        {
            _logSource.LogError(data.ToString());
        }

        public static void LogInfo(object data)
        {
            _logSource.LogInfo(data.ToString());
        }

        public static void LogMessage(object data)
        {
            _logSource.LogMessage(data.ToString());
        }

        public static void LogWarning(object data)
        {
            _logSource.LogWarning(data.ToString());
        }

        public static void LogFatal(object data)
        {
            _logSource.LogFatal(data.ToString());
        }
    }
}
