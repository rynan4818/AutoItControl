//すのー(Snow1226)さんのCameraPlusの HarmonyPatches/CustomPreviewBeatmapLevelPatch.cs をコピーさせていただきました。
//https://github.com/Snow1226/CameraPlus/blob/master/CameraPlus/HarmonyPatches/CustomPreviewBeatmapLevelPatch.cs
//CameraPlusライセンス:MIT License (https://github.com/Snow1226/CameraPlus/blob/master/LICENSE)

using System.IO;
using HarmonyLib;

namespace AutoItControl.HarmonyPatches
{
    [HarmonyPatch(typeof(CustomPreviewBeatmapLevel), nameof(CustomPreviewBeatmapLevel.GetCoverImageAsync))]
    internal class CustomPreviewBeatmapLevelPatch
    {
        public const string songAutoItScript = "SongAutoIt.json";
        public static string customLevelPath = string.Empty;
        static void Postfix(CustomPreviewBeatmapLevel __instance)
        {
#if DEBUG
            Plugin.Log.Notice($"Selected CustomLevel Path :\n {__instance.customLevelPath}");
#endif
            if (File.Exists(Path.Combine(__instance.customLevelPath, songAutoItScript)))
                customLevelPath = Path.Combine(__instance.customLevelPath, songAutoItScript);
            else
                customLevelPath = string.Empty;
        }
    }
}

