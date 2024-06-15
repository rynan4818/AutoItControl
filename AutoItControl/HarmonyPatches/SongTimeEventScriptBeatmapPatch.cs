//すのー(Snow1226)さんのCameraPlusの HarmonyPatches/SongScriptBeatmapPatch.cs をコピーさせていただきました。
//https://github.com/Snow1226/CameraPlus/blob/master/CameraPlus/HarmonyPatches/SongScriptBeatmapPatch.cs
//CameraPlusライセンス:MIT License (https://github.com/Snow1226/CameraPlus/blob/master/LICENSE)

using System.IO;
using HarmonyLib;

namespace AutoItControl.HarmonyPatches
{
    [HarmonyPatch(typeof(LevelSelectionNavigationController), nameof(LevelSelectionNavigationController.HandleLevelCollectionNavigationControllerDidChangeLevelDetailContent))]
    internal class SongTimeEventScriptBeatmapPatch
    {
        private static string _latestSelectedLevelPath = string.Empty;
        private static string _customLevelRoot = CustomLevelPathHelper.customLevelsDirectoryPath;
        public const string songAutoItScript = "SongAutoIt.json";
        public static string customLevelPath = string.Empty;

        static void Postfix(LevelSelectionNavigationController __instance)
        {
            if (CustomLevelLoaderPatch.Instance._loadedBeatmapSaveData != null && __instance.beatmapLevel != null)
            {
                if (CustomLevelLoaderPatch.Instance._loadedBeatmapSaveData.ContainsKey(__instance.beatmapLevel.levelID))
                {
                    string currentLevelPath = CustomLevelLoaderPatch.Instance._loadedBeatmapSaveData[__instance.beatmapLevel.levelID].customLevelFolderInfo.folderPath;
                    if (currentLevelPath != _latestSelectedLevelPath)
                    {
                        _latestSelectedLevelPath = currentLevelPath;
#if DEBUG
                        Plugin.Log.Notice($"Selected CustomLevel Path :\n {currentLevelPath}");
#endif
                        if (File.Exists(Path.Combine(currentLevelPath, songAutoItScript)))
                        {
                            customLevelPath = Path.Combine(currentLevelPath, songAutoItScript);
                            Plugin.Log.Notice($"Found SongScript path : \n{currentLevelPath}");
                        }
                        else
                        {
                            customLevelPath = string.Empty;
                        }
                    }
                }
                else
                    customLevelPath = string.Empty;
            }
        }
    }
}