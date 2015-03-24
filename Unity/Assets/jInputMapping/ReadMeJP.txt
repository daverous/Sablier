

### jInput Mapping ###

キーコンフィグ機能をあなたのゲーム内に！
Input Mapping / key Assign / Key Config



================== まずはデモを動かしてみる ==================

インポートされたファイル郡は、全てjInputMappingフォルダ内にあります
全てのインポートが正常に行われていれば、InputMappingDemoをプレイしてそのままデモとして動作を確認できます


========== あなたのゲーム内で使用するには ==========

インポートしたあと、

UnityのHierarchyウインドウで'jInputMappingSet'オブジェクトをデモからコピーしてきて、あなたのゲームのプレイヤーがキーコンフィグするシーンに加えてください
まずはこのInspectorウインドウで、あなたのゲームに合わせて基本的な設定を行う必要があります
最大プレイヤー人数やデフォルトキーなどです(後述します)

基本設定が終わったら一度そのシーンをPlayしてエラーが出ないかチェックしてください

キャラクターを動かすスクリプト内などでの使い方の基本は、デモのCubeの動きと'DemoCube.cs'を参考にしてください
キー設定された項目は'Mapper.InputArray[]'配列に格納されます(Player1の場合)
通常はInput.Get...と記述する場所を'jInput.Get...'としてください
例: var v = jInput.GetAxis(Mapper.InputArray[0]);
これでPlayer1の配列の0番目に設定された入力を得られます

Joystick方向, Joystickボタン, キーボード, マウス, どれでもマッピングでき、
GetKey&Button, KeyUp, KeyDown, GetAxis&Raw, どれででも取得することができます


========== 新しいVersionにアップデートする ==========

新しいVersionをインポートした直後、必ず'jInputMappingSet'を配置しているシーンを一度開いてください
これにより、アップデートの変更点が機能に適合されます
これを行わない場合、既存のデータ部分とアップデートした部分が食い違って正常に動かない可能性があるので注意してください


===============================================================


 * Escapeキーは恒久的にマッピングできないようになっています
	これはマッピングウインドウでのキャンセルキーとして常に動作させるためです

 * 基本的な設定はjInputMappingSetオブジェクトのInspectorにて入力するだけで自動で行われます
	-Menu Item Headingsではキー入力の項目数と名称を設定します
		項目数に応じてHerarchyウインドウのjInputMappingSet/InMapperMenuItems内に
		自動で欄が追加削除されます
		ゲーム内で実際に使用しているInputArray[]の数未満に設定すると
		プレイ時にエラーになることに注意してください
		例えばデモではInputArray[]の0～6番目を使用しているのでそれ未満の数だとエラーとなります

	-Max Players in Same Placeは同じ場所のゲームプログラムに同時接続してプレイする最大人数です
		ネットを介すのではなく、友達の部屋に集まって一つのゲーム機に
		いくつかのゲームパッドをつなぐような場合です
		２～４人目はそれぞれ
		'Mapper.InputArray2p[]'、'Mapper.InputArray3p[]'、'Mapper.InputArray4p[]'配列に
		格納されています
		例: var jump = jInput.GetKeyDown(Mapper.InputArray2p[5]);
		これでPlayer2の配列の5番目に設定された入力を得られます

	-Used Method for Open&Close Windowはマッピングウインドウの開閉動作の方法です
		'SetActive()'はマッピングウインドウの開閉をSetActive(true/false)で行う場合に使います
		'LoadLevel()'はあなたのゲームのシーンとマッピングウインドウのシーンを
		Application.LoadLevel()でシーンごと切り替えて開閉を行う場合に使います
		'LoadLevelAdditive()'はあなたのゲームのシーンにマッピングウインドウのシーンを
		Application.LoadLevelAdditive()で追加で読み込んで開き、
		閉じる際はjInputだけをDestroyしてゲームのシーンを続行する場合に使います
		どの方法が最良かはあなたのゲームがどういうフローかによって違うので、
		よく判らない方はこのテキスト最後にある説明サイトを見ることをお勧めします

	-Default Input Mappingはプレイヤーがキー設定をしていない場合のデフォルトキーを設定します
		KeyCodeがあるキーの場合はそれを記入します
		例: A / LeftShift / Mouse0 / Joystick1Button1
		JoystickのAxisは以下の規則的な名前を使います
		'Joystick1Axis1'...'Joystick1Axis20'...'Joystick4Axis20'の末尾に'+'か'-'.
		例: Joystick1Axis1+
		マウスホイールも同様に'MouseWheel'の末尾に'+'か'-'.
		例: MouseWheel-
		デモで実際にキーを入力してみればそのキーの設定名がすぐに分かります
		末尾に'+'か'-'が必要なものは忘れないように注意してください
		入力の設定名が空白だったり誤っている場合はシーンをプレイした際にエラーで示唆します
		
	-ExcludeDecisionFncではマッピングウインドウにて決定動作から除外する入力を指定します
		例えば方向キーとして使用する要素をこれに設定してください
		マッピングのウインドウでは設定がしやすいようにほぼすべてのキーが
		決定動作するようになっているので、そのまま方向キーが決定動作もしてしまうと
		方向入力と同時に決定してしまうためその方向キーではカーソルを動かすことが
		できないからです
		
	-AxesAdvanceSettingsではAxisのDeadZone,Gravity,Sensitivityを設定します
		これらは全てのAxis入力に一括で適用されます
		通常のUnityのInspectorと違いここでの値はシーンPlay中に変更した値が
		Play終了後もそのまま保持されるので、あなたのキャラクターの動作が出来上がったら
		実際に動かしつつ最適な設定をするとよいでしょう

 * スタンドアロン版およびエディタでのセーブデータは暗号化バイナリファイルで作られます
	Unityゲームが動いているのと同じディレクトリに'SaveData'フォルダが作られ、
	そのフォルダ内に'InputMapping.dat'として保存されます
	レジストリは使用しません
	PCゲームにおいてプレイヤーがキーコンフィグ設定ファイルを別途保持する事も容易です
	その他のプラットフォームではUnity準拠のPlayerPrefsによってセーブされます



更なる設定やより詳しい説明についてはサイトを見てください!
http://jinput.webcrow.jp/jinput/index.html

製作者: Myouji



内包されているOrvitronフォントは SIL Open Font License v1.10に準じて使用しています

