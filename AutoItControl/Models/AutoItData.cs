using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using AutoItControl.Configuration;
using Zenject;

namespace AutoItControl.Models
{
    public class SongTimeScript
    {
        public float SongTime;
        public Send Send;
        public ControlSend ControlSend;
    }
    public class AutoItData : IInitializable
    {
        public string scriptPath = "";
        public List<SongTimeScript> _timeScript = new List<SongTimeScript>();
        public string winActivate;
        public int eventID;

        public void Initialize()
        {
            if (!File.Exists(PluginConfig.Instance.autoItScriptPath))
            {
                if (!Directory.Exists(Path.GetDirectoryName(PluginConfig.DefaultScriptPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(PluginConfig.DefaultScriptPath));
                }
                PluginConfig.Instance.autoItScriptPath = PluginConfig.DefaultScriptPath;
            }
        }
        public bool LoadFromJson(string jsonString)
        {
            _timeScript.Clear();
            winActivate = null;
            AutoItScriptJson autoItScriptJson = null;
            string sep = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            string sepCheck = (sep == "." ? "," : ".");
            try
            {
                autoItScriptJson = JsonConvert.DeserializeObject<AutoItScriptJson>(jsonString);
            }
            catch (Exception ex)
            {
                Plugin.Log.Error($"JSON file syntax error. {ex.Message}");
            }
            if (autoItScriptJson == null)
                return false;
            winActivate = autoItScriptJson.WinActivate;
            foreach (JSONTimeScript jsonTimeScript in autoItScriptJson.JsonTimeScript)
            {
                SongTimeScript newScript = new SongTimeScript();
                newScript.SongTime = float.Parse(jsonTimeScript.SongTime.Contains(sepCheck) ? jsonTimeScript.SongTime.Replace(sepCheck, sep) : jsonTimeScript.SongTime);
                newScript.Send = jsonTimeScript.Send;
                newScript.ControlSend = jsonTimeScript.ControlSend;
                _timeScript.Add(newScript);
            }
            _timeScript = _timeScript.OrderBy(x => x.SongTime).ToList();
            return true;
        }
        public bool LoadAutoItData(string path = null)
        {
            if (path == null)
                path = PluginConfig.Instance.autoItScriptPath;
            if (!File.Exists(path))
                return false;
            if (scriptPath == path)
                return true;
            string jsonText = File.ReadAllText(path);
            if (!LoadFromJson(jsonText))
                return false;
            if (_timeScript.Count == 0)
            {
                Plugin.Log.Notice("No AutoIt data!");
                return false;
            }
            Plugin.Log.Notice($"Found {_timeScript.Count} entries in: {path}");
            scriptPath = path;
            return true;
        }
        public void ResetEventID()
        {
            eventID = 0;
        }
        public SongTimeScript UpdateEvent(float songtime)
        {
            if (eventID >= _timeScript.Count)
                return null;
            if (_timeScript[eventID].SongTime <= songtime)
            {
#if DEBUG
                Plugin.Log.Info($"EventID:{eventID}");
#endif
                return _timeScript[eventID++];
            }
            else
                return null;
        }
    }
}
