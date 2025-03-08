namespace Hikaria.UImGUI
{
    internal static class ListExt
    {
        public static Il2CppSystem.Collections.Generic.List<T> ToIl2CppList<T>(this List<T> list)
        {
            var il2CppList = new Il2CppSystem.Collections.Generic.List<T>();
            foreach (var item in list)
                il2CppList.Add(item);

            return il2CppList;
        }
    }
}
