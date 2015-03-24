

### jInput Mapping ###

Add input mapping system in your game !
 key Assign / Key Config



===== First play the Demo =====

There are all imported files located in 'jInputMapping'folder.
All import operation successful completion, you can play the InputMappingDemo
and confirm operation as Demo.


===== How to use in your game =====

after imported,

The HierarchyWindow in Unity, copy and add 'jInputMappingSet'object from Demo
to the scene that your game player will set input mapping.
First of all you need to assign the basic settings in the InspectorWindow of this for fit your game.
The most number of players, default keys, and so on.(described later)

When basic settings have completed, Play the scene once and check for whether any errors
appear or not.

The basic usage in script such as move the character, move of Cube in Demo and 'DemoCube.cs' as reference.
Input setting elements are stored array 'Mapper.InputArray[]' (case of Player1).
To write 'jInput.Get...' usually write Input.Get....
Example: var v = jInput.GetAxis(Mapper.InputArray[0]);
This get the input 0th array of Player1.

Can be mapping to any, JoystickAxis, JoystickButton, KeyBoard, Mouse.
And can be perception to any, GetKey&Button, KeyUp, KeyDown, GetAxis&Raw.


========== Update to a new Version ==========

Immediately after you have imported the new Version, please be sure to open once the scene that is put 'jInputMappingSet'.
The changes of the update will be adapted to function.
Please note if you do not do this, there is a possibility that does not work normally, because the part of update disagree the data until that time.


===============================================================


 * Escape Key can not be mapping permanently.
	This always operate to cancel key in the mapping window.

 * Most of basic settings are automatically by simply assign in the InspectorWindow
  of jInputMappingSet object.
	-'Menu Item Headings' set the number and name of items of key mapping.
		Items at jInputMappingSet/InMapperMenuItems in the HierarchyWindow
		will be added or deleted automatically according to the number.
		If set to less than the number of be actually used elements in InputArray[],
		it is an error when you play the scene.
		For example, the Demo scene is using 0-6 in InputArray[], so it is an error that less than 7.

	-'Max Players in Same Place' is maximum number of players to connection
	 one game program in the same place at the same time.
	 	For example, frends meet in one room and connect some GamePad
	 	to the one game machine and not use internet.
		Each 2-4 Players input setting elements are stored array
		'Mapper.InputArray2p[]'、'Mapper.InputArray3p[]'、'Mapper.InputArray4p[]'.
		Example: var jump = jInput.GetKeyDown(Mapper.InputArray2p[5]);
		This get the input 5th array of Player2.

	-'Used Method for Open&Close Window' is selection the way of open and close the mapping window.
		'SetActive()'is used when open and close by SetActive(true/false).
		'LoadLevel()'is used when open and close by Application.LoadLevel(), switching whole scene
		between your game scene and mapping window scene.
		'LoadLevelAdditive()'is used when open and close by Application.LoadLevelAdditive(),
		add mapping window scene to your game scene in order to open, and destroy jInput
		in order to close, be kept your game scene.
		Which method is the best, it is depending on the flow of your game.
		If you cannot grasp, I recommend to see the description site at the end of this text.

	-'Default Input Mapping' set the default key, when the player
	 do not set the input mapping yet.
		To write KeyCode name
		Example: A / LeftShift / Mouse0 / Joystick1Button1
		Or Joystick Axis regularity name
		'Joystick1Axis1'...'Joystick1Axis20'...'Joystick4Axis20' add the end of it '+'or'-'.
		Example: Joystick1Axis1+
		MouseWheel same so 'MouseWheel' add the end of it '+'or'-'.
		Example: MouseWheel-
		Actually entered in the demo, can know the setting name of the key.
		Be careful in case of add the end of it '+'or'-'.
		If the setting name is incorrect or a blank, it is suggested in error log
		when you play the scene.

	-'ExcludeDecisionFnc' specify to exclude input from the decision work
	 in the mapping window.
		For example, the elements of direction keys set this.
		Almost all keys are adapted to decision work in the mapping window to operate easily,
		if the direction keys also work as decision, can not move cursor by those keys
		because it would determine at the same time as direction move.

	-'AxesAdvanceSettings' set DeadZone, Gravity, Sensitivity of Axes.
		These are applied all Axis input.
		Those values are hold as it is after Play the scene unlike InspectorWindow normally,
		so it is a good idea to the optimal settings while actually moving your game character.

 * SaveData is made encrypted binary file in standalone and in Unity editor.
	'SaveData'folder is created in same directory of Unity game execution,
	and be saved as 'InputMapping.dat' in the folder.
	It Not use Directory.
	It is also easy that the player holds different input mapping data files in a PC game.
	On other platforms, it is saved by PlayerPrefs of Unity compliant.



Please look at the site that More!
http://jinput.webcrow.jp/jinput/index_en.html

producer: Myouji



This pacage embed OrvitronFont in accordance with SIL Open Font License v1.10.

