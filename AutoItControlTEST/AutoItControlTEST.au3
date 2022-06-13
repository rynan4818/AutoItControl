#include <ButtonConstants.au3>
#include <EditConstants.au3>
#include <GUIConstantsEx.au3>
#include <StaticConstants.au3>
#include <WindowsConstants.au3>
Opt("GUIOnEventMode", 1)
#Region ### START Koda GUI section ### Form=d:\program\autoit\autoitcontroltest\form1.kxf
$Form_main = GUICreate("AutoItControl TEST", 481, 470, 421, 156)
GUISetFont(9, 400, 0, "ＭＳ Ｐゴシック")
GUISetOnEvent($GUI_EVENT_CLOSE, "Form_mainClose")
$Input_WinActivate = GUICtrlCreateInput("", 112, 48, 273, 20)
$Checkbox_WinActivate = GUICtrlCreateCheckbox("WinActivate", 24, 48, 81, 25)
$Input_SendKeys = GUICtrlCreateInput("", 112, 88, 273, 20)
$Checkbox_Send = GUICtrlCreateCheckbox("Send", 24, 88, 49, 17)
$Checkbox_SendFlag = GUICtrlCreateCheckbox("flag", 400, 88, 57, 17)
$Checkbox_ControlSend = GUICtrlCreateCheckbox("ControlSend", 24, 128, 81, 17)
$Label1 = GUICtrlCreateLabel("keys", 74, 88, 33, 19)
GUICtrlSetFont(-1, 11, 400, 0, "ＭＳ Ｐゴシック")
$Label2 = GUICtrlCreateLabel("title", 128, 128, 28, 19)
GUICtrlSetFont(-1, 11, 400, 0, "ＭＳ Ｐゴシック")
$Label3 = GUICtrlCreateLabel("text", 127, 160, 29, 19)
GUICtrlSetFont(-1, 11, 400, 0, "ＭＳ Ｐゴシック")
$Label4 = GUICtrlCreateLabel("controlID", 97, 193, 63, 19)
GUICtrlSetFont(-1, 11, 400, 0, "ＭＳ Ｐゴシック")
$Label5 = GUICtrlCreateLabel("string", 121, 221, 39, 19)
GUICtrlSetFont(-1, 11, 400, 0, "ＭＳ Ｐゴシック")
$Input_ControlSendTitle = GUICtrlCreateInput("", 160, 128, 225, 20)
$Input_ControlSendText = GUICtrlCreateInput("", 160, 160, 225, 20)
$Input_ControlSendControlID = GUICtrlCreateInput("", 160, 192, 225, 20)
$Input_ControlSendString = GUICtrlCreateInput("", 160, 224, 225, 20)
$Checkbox_ControlSendFlag = GUICtrlCreateCheckbox("flag", 400, 224, 49, 17)
$Input_StartWait = GUICtrlCreateInput("3", 56, 288, 33, 20)
$Label6 = GUICtrlCreateLabel("Start Wait", 56, 272, 55, 16)
$Label7 = GUICtrlCreateLabel("Sec", 96, 296, 23, 16)
$Input_WinActivateWait = GUICtrlCreateInput("2", 168, 288, 41, 20)
$Label8 = GUICtrlCreateLabel("WinActivate Wait", 168, 272, 91, 16)
$Label9 = GUICtrlCreateLabel("Sec", 216, 288, 23, 16)
$Button_RUN = GUICtrlCreateButton("RUN", 312, 272, 73, 33)
GUICtrlSetOnEvent(-1, "Button_RUNClick")
$Edit_Output = GUICtrlCreateEdit("", 32, 328, 417, 121, BitOR($ES_AUTOVSCROLL,$ES_AUTOHSCROLL,$ES_WANTRETURN,$WS_VSCROLL))
$Label_SendKeyDownDelay = GUICtrlCreateLabel("SendKeyDownDelay", 24, 16, 105, 16)
$Input_SendKeyDownDelay = GUICtrlCreateInput("50", 136, 16, 41, 20)
$Label10 = GUICtrlCreateLabel("msec", 184, 16, 31, 16)
$SendKeyDelay = GUICtrlCreateLabel("SendKeyDelay", 240, 16, 77, 16)
$Input_SendKeyDelay = GUICtrlCreateInput("5", 320, 16, 41, 20)
$msec = GUICtrlCreateLabel("msec", 368, 16, 31, 16)
GUISetState(@SW_SHOW)
#EndRegion ### END Koda GUI section ###

AutoItSetOption("WinWaitDelay",50)

While 1
	Sleep(100)
WEnd

Func Button_RUNClick()
	GUICtrlSetState($Button_RUN, $GUI_DISABLE)
	$mes = ""
	AutoItSetOption("SendKeyDelay",GUICtrlRead($Input_SendKeyDelay))
	AutoItSetOption("SendKeyDownDelay",GUICtrlRead($Input_SendKeyDownDelay))
	$mes &= "START! ... Wait " & GUICtrlRead($Input_StartWait) & " sec" & @CR & @LF
	GUICtrlSetData($Edit_Output,$mes)
	Sleep(GUICtrlRead($Input_StartWait) * 1000)
	if GUICtrlRead($Checkbox_WinActivate) == $GUI_CHECKED Then
		$winActivate = GUICtrlRead($Input_WinActivate)
		if WinWait($winActivate, "", 1) <> 0 Then
			WinActivate($winActivate)
			WinWaitActive($winActivate, "", 3)
			$mes &= "WinActive : " & GUICtrlRead($Input_WinActivate) & @CR & @LF
			GUICtrlSetData($Edit_Output,$mes)
		Else
			$mes &= "WinActive ERROR! : " & GUICtrlRead($Input_WinActivate) & @CR & @LF
			GUICtrlSetData($Edit_Output,$mes)
		EndIf
		$mes &= "Wait " & GUICtrlRead($Input_WinActivateWait) & " sec" & @CR & @LF
		GUICtrlSetData($Edit_Output,$mes)
		Sleep(GUICtrlRead($Input_WinActivateWait) * 1000)
	EndIf
	if GUICtrlRead($Checkbox_Send) == $GUI_CHECKED Then
		$flag = 0
		if GUICtrlRead($Checkbox_SendFlag) == $GUI_CHECKED Then $flag = 1
		$mes &= "Send : " & GUICtrlRead($Input_SendKeys) & @CR & @LF
		GUICtrlSetData($Edit_Output,$mes)
		Send(GUICtrlRead($Input_SendKeys),$flag)
	EndIf
	if GUICtrlRead($Checkbox_ControlSend) == $GUI_CHECKED Then
		$flag = 0
		if GUICtrlRead($Checkbox_ControlSendFlag) == $GUI_CHECKED Then $flag = 1
		$mes &= "ControlSend : " & GUICtrlRead($Input_ControlSendString)
		GUICtrlSetData($Edit_Output,$mes)
		if ControlSend(GUICtrlRead($Input_ControlSendTitle),GUICtrlRead($Input_ControlSendText),GUICtrlRead($Input_ControlSendControlID),GUICtrlRead($Input_ControlSendString),$flag) == 1 Then
			$mes &= " : OK!" & @CR & @LF
			GUICtrlSetData($Edit_Output,$mes)
		Else
			$mes &= " : ERROR! ERROR!" & @CR & @LF
			GUICtrlSetData($Edit_Output,$mes)
		EndIf
	EndIf
	$mes &= "END!" & @CR & @LF
	GUICtrlSetData($Edit_Output,$mes)
	GUICtrlSetState($Button_RUN, $GUI_ENABLE)
EndFunc
Func Form_mainClose()
	Exit
EndFunc
