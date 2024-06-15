//すのー(Snow1226)さんのCameraPlusの HarmonyPatches/CustomLevelLoaderPatch.cs をコピーさせていただきました。
//https://github.com/Snow1226/CameraPlus/blob/master/CameraPlus/HarmonyPatches/CustomLevelLoaderPatch.cs
//CameraPlusライセンス:MIT License (https://github.com/Snow1226/CameraPlus/blob/master/LICENSE)

using HarmonyLib;

namespace AutoItControl.HarmonyPatches
{

    [HarmonyPatch(typeof(CustomLevelLoader), nameof(CustomLevelLoader.Awake))]
    internal static class CustomLevelLoaderPatch
    {
        public static CustomLevelLoader Instance { get; set; } = null;
        static void Postfix(CustomLevelLoader __instance)
        {
            Instance = __instance;
#if DEBUG
            Plugin.Log.Notice($"CustomLevelLoader Loaded");
#endif
        }

    }
}