namespace Hikaria.UImGUI
{
    internal static class ArrayExt
    {
        public static List<T> ToIl2CppList<T>(this T[] array)
        where T : Il2CppSystem.Object
        {
            var il2CppList = new List<T>();
            foreach (var item in array)
                il2CppList.Add(item);

            return il2CppList;
        }
    }
}
