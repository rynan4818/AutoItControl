# AutoItControl
BeatSaberで[AutoIt](https://www.autoitscript.com/site/autoit/)を使って外部ソフトをコントロールするmodです。

# インストール方法
1. [リリース](https://github.com/rynan4818/AutoItControl/releases)から最新のzipファイルをダウンロードして下さい。
2. BeatSaberのゲームフォルダに以下をコピーして下さい。
    - Pluginフォルダに`AutoItControl.dll`をコピーして下さい。
    - Libsフォルダに`AutoItX3.Assembly.dll` と `AutoItX3_x64.dll` をコピーして下さい。

# 使用方法
現状は曲専用に作られた、指定曲時間でキーボード送信するスクリプトに対応しています。

譜面フォルダに`SongAutoIt.json`と言う名前で下記内容のJSONファイルを作成します。

とりあえず、[バーチャルモーションキャプチャー](https://vmc.info/)の表情制御をしてみます。

例として、こんな感じでショートカットキーで表情を設定した場合。

![image](https://user-images.githubusercontent.com/14249877/173230744-45c5503b-56ec-41c6-9c96-758be11a92d6.png)

下記スクリプトを作成すると
- 譜面スタート時に`VirtualMotionCapture`というタイトルで始まるウィンドウをアクティブ（前面にする）
- 曲時間5.5秒の時にキーボードで`1`を送信
- 曲時間6.5秒の時にキーボードで`2`を送信
- 曲時間7.5秒の時にキーボードで`3`を送信
- 曲時間8.5秒の時にキーボードで`+`を送信

となります。

		{
		  "WinActivate": "VirtualMotionCapture",
		  "TimeScript": [
		    {
		      "SongTime": "5.5",
		      "Send": {
		        "keys": "1"
		      }
		    },
		    {
		      "SongTime": "6.5",
		      "Send": {
		        "keys": "2"
		      }
		    },
		    {
		      "SongTime": "7.5",
		      "Send": {
		        "keys": "3"
		      }
		    },
		    {
		      "SongTime": "8.5",
		      "Send": {
		        "keys": "+",
		        "flag": "1"
		      }
		    }
		  ]
		}

上の例では適当に1,2,3,4とかにしましたが、あまり影響の無いショートカットキーに設定することをおすすめします。

JSONフォーマットのチェックには、[JSONきれい](https://tools.m-bsys.com/development_tooles/json-beautifier.php)とか整形＆チェックしてくれるサイトも多いので、事前チェックされることをオススメします。

## JSONの説明

- **WinActivate** : 譜面のスタート時にこの名前で始まるタイトルのウィンドウをアクティブ（前面）にします。
    - この機能を使用しない場合は、項目自体を削除して下さい。VirtualMotionCaptureはアクティブでなくても反応しますので、特に指定しなくても大丈夫ですが、現在アクティブになっているソフトにキー送信されるので、アクティブになっているソフトによっては誤った動作が起きることがあります。
- **TimeScript** : 曲時間ごとに送信するキーのリストを配列にします。
- **SongTime** : キー送信したい曲時間(秒)です。
    - 現状は00:50 のような表記に対応していませんので1:10.5の場合は70.5と指定して下さい。
- **Send** : AutoItの[Send関数](https://www.autoitscript.com/autoit3/docs/functions/Send.htm) を使用します。
    - パラメータをJSONオブジェクトで指定します。
    - [Send関数の日本語訳(但し古いので注意)](https://open-shelf.appspot.com/AutoIt3.3.6.1j/html/functions/Send.htm)
- **keys** : Send関数のkeysパラメータに相当します。
    - 入力した内容がキーとして送信されます。CTRLやSHIFTなどを押しながらは特殊文字で指定します。※例:CTRL+ALT+a → "^!a"
- **flag** : Send関数のflagパラメータに相当します。
    - "1"を指定すると、`!` `+` `^` `#` `{` `}`などのAutoIt用の特殊文字をそのまま送信した場合に指定します。項目がない場合はデフォルト値"0"になります。flagはあまり使わないと思います。


とりあえず[Send](https://www.autoitscript.com/autoit3/docs/functions/Send.htm)と[ControlSend](https://www.autoitscript.com/autoit3/docs/functions/ControlSend.htm)コマンドは実装済みです。

ControlSendはデバッグ中なので説明を省きます。とりあえずバーチャルモーションキャプチャーでは使えません。

