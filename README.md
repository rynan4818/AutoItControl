# AutoItControl
BeatSaberで[AutoIt](https://www.autoitscript.com/site/autoit/)を使って外部ソフトをコントロールするmodです。

# インストール方法
1. [リリース](https://github.com/rynan4818/AutoItControl/releases)から最新のzipファイルをダウンロードして下さい。
2. BeatSaberのゲームフォルダに以下をコピーして下さい。
    - Pluginフォルダに`AutoItControl.dll`をコピーして下さい。
    - Libsフォルダに`AutoItX3.Assembly.dll` と `AutoItX3_x64.dll` をコピーして下さい。

# 使用方法
現状は曲専用に作られた、指定曲時間でキーボード送信するスクリプトに対応しています。

譜面フォルダに`SongAutoIt.json`と言う名前でスクリプトのJSONファイルを作成します。

## Sendコマンド

Sendコマンドは、AutoItの[Send](https://www.autoitscript.com/autoit3/docs/functions/Send.htm)関数を使用します。

※[Send関数の日本語訳(但し古いので注意)](https://open-shelf.appspot.com/AutoIt3.3.6.1j/html/functions/Send.htm)

これは、現在アクティブ（前面に出ている）なウィンドウを対象にキーボード送信します。要するに、手でキーボードを打つのと全く一緒です。

これは、注意しないと想定外のソフトにキーボード送信する可能性があり、誤作動の危険があります。

とりあえず、[バーチャルモーションキャプチャー](https://vmc.info/)の表情制御をしてみます。

例として、こんな感じでショートカットキーで表情を設定した場合。

![image](https://user-images.githubusercontent.com/14249877/173230744-45c5503b-56ec-41c6-9c96-758be11a92d6.png)

下記スクリプトを作成すると
- 譜面スタート時に`VirtualMotionCapture`というタイトルで始まるウィンドウをアクティブ（前面)にする
- 曲時間5.5秒の時にキーボードで`1`を送信して表情をFunに設定
- 曲時間6.5秒の時にキーボードで`2`を送信して表情をSorrowに設定
- 曲時間7.5秒の時にキーボードで`3`を送信して表情をBlinkに設定
- 曲時間8.5秒の時にキーボードで`4`を送信して表情をJoyに設定

となります。

バーチャルモーションキャプチャーはウィンドウが非アクティブでも反応しますので、特にアクティブにしなくても大丈夫ですが、想定外のソフトがアクティブになっていた場合にキー送信されるので、誤作動防止の為アクティブ化の処理を入れています。

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
            "keys": "4"
          }
        }
      ]
    }

上の例では適当に1,2,3,4とかにしましたが、あまり影響の無いショートカットキーに設定することをおすすめします。

JSONフォーマットのチェックには、[JSONきれい](https://tools.m-bsys.com/development_tooles/json-beautifier.php)とか整形＆チェックしてくれるサイトも多いので、事前チェックされることをオススメします。

### flagパラメータを使用する場合のTimeScriptのSendコマンド
    {
      "SongTime": "8.5",
      "Send": {
        "keys": "+",
        "flag": "1"
    }

### SendコマンドのJSONの説明

- **WinActivate** : 譜面のスタート時にこの名前で始まるタイトルのウィンドウをアクティブ（前面）にします。
    - この機能を使用しない場合は、項目自体を削除して下さい。
- **TimeScript** : 曲時間ごとに送信するキーのリストを配列にします。
- **SongTime** : キー送信したい曲時間(秒)です。
    - 現状は00:50 のような表記に対応していませんので1:10.5の場合は70.5と指定して下さい。
- **Send** : AutoItの[Send関数](https://www.autoitscript.com/autoit3/docs/functions/Send.htm) を使用します。
    - パラメータをJSONオブジェクトで指定します。
    - 
- **keys** : Send関数のkeysパラメータに相当します。
    - 入力した内容がキーとして送信されます。CTRLやSHIFTなどを押しながらは特殊文字で指定します。
    - 例:CTRL+ALT+a → "^!a"
- **flag** : Send関数のflagパラメータに相当します。
    - "1"を指定すると、`!` `+` `^` `#` `{` `}`などのAutoIt用の特殊文字をそのまま送信したい場合に指定します。
    - 項目がない場合はデフォルト値"0"になります。flagはあまり使わないと思います。

## ControlSendコマンド

ControlSendは、AutoItの[ControlSend](https://www.autoitscript.com/autoit3/docs/functions/ControlSend.htm)関数を使用します。

※[ControlSend関数の日本語訳(但し古いので注意)](https://open-shelf.appspot.com/AutoIt3.3.6.1j/html/functions/ControlSend.htm)

これは、ウィンドウのアクティブ状態は関係なく、指定したウィンドウの指定したコントロール（テキストボックスとか）に狙い撃ちでキーボード送信します。そのため、他のソフトが誤作動する危険性が無く、信頼性の高い制御方法になります。但し、そのためにはcontrolIDと呼ぶ固有のIDを調べる必要があります。

ただし、ControlSendはUnityで作られれたバーチャルモーションキャプチャーなどは制御できませんので、その場合はSendコマンドを使用して下さい。

OBS Studioを例に説明します。

[リリース](https://github.com/rynan4818/AutoItControl/releases)のAssetsにmodと一緒に`AutoItControlTEST.zip`が登録されています。こちらのツールを使って、controlIDの調査と事前テストが可能です。

`AutoItControlTEST.zip`を解凍すると、`Au3Info_x64.exe`が出てきますので適当な場所において実行して下さい。

そうすると、下の画像の様なツールが開きますので、Finder Toolの丸い的マークをマウスドラッグでcontrolIDを調べたい対象のソフト(この場合はOBS)の適当な場所に持っていくと、色々情報が取得できます。この時にAutoIt InfoツールのControlタブのAdvanced Modeの項目が表示されれば、そのソフトはControlSendを使えます。

Advanced Modeをダブルクリックすると内容がコピーできますので、コピーします。

コピーできたら、JSONのTimeScriptを以下の様に設定します。

    {
      "SongTime": "5.5",
      "ControlSend": {
        "title": "OBS",
        "controlID": "[CLASS:Qt5152QWindowIcon]",
        "string": "1"
      }
    }

### ControlSendコマンドのJSONの説明

- **title** は、ウィンドウのタイトルで、AutoIt Infoツールで表示されたTitle欄がそうです。前方一致なのでバージョンまで含めると将来的に動かなくなりそうなので、"OBS"だけにします。

- **controlID** は、先程調べたAdvanced Modeの内容ですが、コピーした内容は`[CLASS:Qt5152QWindowIcon; INSTANCE:2]`の様になっていたと思います。今回はOBSのショートカットキー設定を使う予定なので、制御対象がウィンドウであれば良いため`; INSTANCE:2`の部分は削除しています。

- **string** は、送信するキーボードの内容です。SendコマンドのKeysと同じです。

- **text** 例:`"text": "obs64"` 上記例はで指定していませんが、textパラメータも指定可能です。対象ウィンドウをTitleだけでは絞り込めない場合に指定して下さい。

- **flag** 例:`"flag": "1"` 上記例では指定していませんが、Send関数のflagと同じ意味です。

ControlSendコマンドは、対象のウィンドウのTitleやcontrolIDを指定するため、正しく動作するか事前確認を行いたいと思います。そのため、`AutoItControlTEST.zip`に同封してあった`AutoItControlTEST.exe`が役立ちます。

これは、このソフトでAutoItControlの動作を模擬してくれます。使いたいコマンドにチェックを入れて内容を入力してRUNを押すと、キーボード送信動作を模擬してくれます。スクリプト作成前に事前確認を行うのに便利だと思います。



SendコマンドとControlSendコマンドはTimeScript上で混在して使用可能です。
