using AutoItControl.HarmonyPatches;
using AutoItControl.Configuration;
using AutoIt;
using System;
using System.Threading;
using Zenject;

namespace AutoItControl.Models
{
    /// <summary>
    /// Monobehaviours (scripts) are added to GameObjects.
    /// For a full list of Messages a Monobehaviour can receive from the game, see https://docs.unity3d.com/ScriptReference/MonoBehaviour.html.
    /// </summary>

    public class AutoItController : IInitializable, IDisposable
    {
        private bool _disposedValue;
        private IAudioTimeSource _audioTimeSource;
        private AutoItData _data;
        private Thread _thread;

        public AutoItController(IAudioTimeSource audioTimeSource, AutoItData _data)
        {
            this._audioTimeSource = audioTimeSource;
            this._data = _data;
        }
        public void Initialize()
        {
            var scriptCheck = false;
            if (PluginConfig.Instance.songSpecificScript && SongTimeEventScriptBeatmapPatch.customLevelPath != String.Empty)
                scriptCheck = _data.LoadAutoItData(SongTimeEventScriptBeatmapPatch.customLevelPath);
            else
                scriptCheck = _data.LoadAutoItData();
            if (!scriptCheck)
                return;
            _data.ResetEventID();
            this._thread = new Thread(new ThreadStart(() =>
            {
                if (_data.winActivate != null)
                {
                    if (AutoItX.WinWait(_data.winActivate, "", 1) == 0)
                        Plugin.Log.Warn($"WinWait Timeout:{_data.winActivate}");
                    else
                    {
                        AutoItX.WinActivate(_data.winActivate);
                        AutoItX.WinWaitActive(_data.winActivate, "", 3);
#if DEBUG
                        Plugin.Log.Info($"WinActive:{_data.winActivate}");
#endif
                    }
                }
                while (!this._disposedValue)
                {
                    try
                    {
                        this.UpdateCurrentSongTime();
                    }
                    catch (Exception e)
                    {
                        Plugin.Log.Error(e);
                    }
                    finally
                    {
                        Thread.Sleep(16);
                    }
                }
            }));
            this._thread.Start();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposedValue)
            {
                if (disposing)
                {
#if DEBUG
                    Plugin.Log.Info("AutoItController Dispose");
#endif
                }
                this._disposedValue = true;
            }
        }
        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private void UpdateCurrentSongTime()
        {
            var timeEvent = _data.UpdateEvent(_audioTimeSource.songTime);
            if (timeEvent == null)
                return;
            if (timeEvent.Send != null)
            {
                var keys = "";
                var flag = 0;
                if (timeEvent.Send.keys != null)
                    keys = timeEvent.Send.keys;
                if (timeEvent.Send.flag == true)
                    flag = 1;
                AutoItX.Send(keys, flag);
#if DEBUG
                Plugin.Log.Info($"{timeEvent.SongTime}:{keys}");
#endif
            }
            if (timeEvent.ControlSend != null)
            {
                var title = "";
                var text = "";
                var controlID = "";
                var stringText = "";
                var flag = 0;
                if (timeEvent.ControlSend.title != null)
                    title = timeEvent.ControlSend.title;
                if (timeEvent.ControlSend.text != null)
                    text = timeEvent.ControlSend.text;
                if (timeEvent.ControlSend.controlID != null)
                    controlID = timeEvent.ControlSend.controlID;
                if (timeEvent.ControlSend.stringText != null)
                    stringText = timeEvent.ControlSend.stringText;
                if (timeEvent.ControlSend.flag == true)
                    flag = 1;
                if (AutoItX.ControlSend(title, text, controlID, stringText, flag) == 0)
                {
                    Plugin.Log.Warn($"ControlSend Error :{timeEvent.SongTime}:{title}:{controlID}:{stringText}");
                }
                else
                {
#if DEBUG
                    Plugin.Log.Info($"{timeEvent.SongTime}:{title}:{controlID}:{stringText}");
#endif
                }
            }
        }


    }
}
