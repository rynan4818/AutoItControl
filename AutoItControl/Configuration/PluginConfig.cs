using System.IO;
using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace AutoItControl.Configuration
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; set; }
        /// <summary>
        /// デフォルトのAutoItスクリプトファイルパスです。
        /// </summary>
        public static readonly string DefaultScriptPath = Path.Combine(IPA.Utilities.UnityGame.UserDataPath, "AutoItControl", "DefaultAutoIt.json");

        // BSIPAが値の変更を検出し、自動的に設定を保存したい場合は、'virtual'でなければなりません。

        /// <summary>
        /// AutoItスクリプトのファイルパス
        /// </summary>
        public virtual string autoItScriptPath { get; set; } = DefaultScriptPath;

        /// <summary>
        /// 曲専用AutoItスクリプトの有効・無効
        /// </summary>
        public virtual bool songSpecificScript { get; set; } = true;

        /// <summary>
        /// キーストローク中のキーを押してから離すまでの時間を変更します。単位は[ミリ秒]
        /// </summary>
        public virtual int sendKeyDownDelay { get; set; } = 50;

        /// <summary>
        /// ウィンドウ関連関数成功後の停止時間を変更します。単位は[ミリ秒]
        /// </summary>
        public virtual int winWaitDelay { get; set; } = 50;

        /// <summary>
        /// これは、BSIPAが設定ファイルを読み込むたびに（ファイルの変更が検出されたときを含めて）呼び出されます。
        /// </summary>
        public virtual void OnReload()
        {
            // 設定ファイルを読み込んだ後の処理を行う。
        }

        /// <summary>
        /// これを呼び出すと、BSIPAに設定ファイルの更新を強制します。 これは、ファイルが変更されたことをBSIPAが検出した場合にも呼び出されます。
        /// </summary>
        public virtual void Changed()
        {
            // 設定が変更されたときに何かをします。
        }

        /// <summary>
        /// これを呼び出して、BSIPAに値を<paramref name ="other"/>からこの構成にコピーさせます。
        /// </summary>
        public virtual void CopyFrom(PluginConfig other)
        {
            // このインスタンスのメンバーは他から移入されました
        }
    }
}
