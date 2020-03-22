using UnityEngine;
using System.Collections;

public class GameOverScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void OnGUI () {
		GuiHelper.InitMe ();
		GuiHelper.DrawText ("GameOver", GuiHelper.SmallFont, 0.3, 0.1, 0.6, 0.8);

		if (GUI.Button (new Rect (GuiHelper.PercentW(0.1), GuiHelper.PercentH(0.7), GuiHelper.PercentW(0.8), GuiHelper.PercentH(0.2)), "Retry", GuiHelper.CustomButton)) {
			Application.LoadLevel("mainScene");
		}
	}
}
