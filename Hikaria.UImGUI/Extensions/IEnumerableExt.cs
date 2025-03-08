namespace Hikaria.UImGUI
{
    internal static class IEnumerableExt
    {
        public static Il2CppSystem.Collections.Generic.List<T> ToIl2CppList<T>(this IEnumerable<T> enumerable)
        {
            var il2CppList = new Il2CppSystem.Collections.Generic.List<T>();
            foreach (var t in enumerable)
            {
                il2CppList.Add(t);
            }

            return il2CppList;
        }
    }
}
