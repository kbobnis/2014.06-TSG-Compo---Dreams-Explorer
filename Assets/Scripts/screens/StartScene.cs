using UnityEngine;
using System.Collections;

public class StartScene : MonoBehaviour {

	void OnGUI () {
		GuiHelper.InitMe ();

		if (GUI.Button (new Rect (GuiHelper.PercentW(0.2), GuiHelper.PercentH(0.70), GuiHelper.PercentW(0.60), GuiHelper.PercentH(0.20)), "Start Game", GuiHelper.CustomButton)) {
			Application.LoadLevel("mainScene");
		}
	}
}
