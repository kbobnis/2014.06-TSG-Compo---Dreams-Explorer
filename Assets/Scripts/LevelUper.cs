using UnityEngine;
using System.Collections;

public class LevelUper : MonoBehaviour {

	private int MinLife, MinAgility, MinStrength;

	// Use this for initialization
	void Start () {
		Character ch = GetComponent<Character> ();
		MinLife = ch.Life;
		MinAgility = ch.Agility;
		MinStrength = ch.Strength;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){

		//main window
		GUI.BeginGroup (new Rect (GuiHelper.PercentW(0.05), GuiHelper.PercentH(0.1), GuiHelper.PercentW(0.9), GuiHelper.PercentH(0.8)), GuiHelper.CustomButton);
			GUI.BeginGroup (new Rect (GuiHelper.PercentW(0.01), GuiHelper.PercentH(0.01), GuiHelper.PercentW(0.45), GuiHelper.PercentH(1)), GuiHelper.CustomButton);
				LeftSide ();
			GUI.EndGroup ();
			GUI.BeginGroup (new Rect (GuiHelper.PercentW(0.45), GuiHelper.PercentH(0.01), GuiHelper.PercentW(0.45), GuiHelper.PercentH(1)), GuiHelper.CustomButton);
				RightSide ();
			GUI.EndGroup ();
		GUI.EndGroup ();
	}

	void RightSide(){
		Character ch = GetComponent<Character> ();
		//left hand
		GUI.BeginGroup (new Rect (GuiHelper.PercentW(0.01), GuiHelper.PercentH(0.01), GuiHelper.PercentW(0.45), GuiHelper.PercentH(0.4)), GuiHelper.CustomButton);

		PrintHand (ch.leftHand.GetComponent<Item>(), 0.1);

		GUI.EndGroup ();

		GUI.BeginGroup (new Rect (GuiHelper.PercentW(0.01), GuiHelper.PercentH(0.4), GuiHelper.PercentW(0.45), GuiHelper.PercentH(0.4)), GuiHelper.CustomButton);

		PrintHand (ch.rightHand.GetComponent<Item>(), 0.1);
		
		GUI.EndGroup ();
	}

	private void PrintHand(Item item, double y){

		if (item == null) {
			GuiHelper.DrawText ("No item ", GuiHelper.MicroFont, 0, 0);
		} else {
			PrintSkill(item.Instant, y);
			//skill
			PrintSkill(item.Skill, y+=0.2);
		}

	}

	private void PrintSkill(Skill skill, double y){
		//instant 
		if (skill == null){
			GuiHelper.DrawText("No skill", GuiHelper.MicroFont, 0, y);
		} else {
			GuiHelper.DrawText(skill.ToString(), GuiHelper.MicroFont, 0, y);
		}
	}

	void LeftSide(){
		Character ch = GetComponent<Character> ();
		GuiHelper.DrawText ("Exp " + ch.ActualExp, GuiHelper.LittleFont, 0, 0);
		GuiHelper.DrawText ("Life " + ch.Life , GuiHelper.LittleFont, 0, 0.2);

		string plus = "+";// + ch.NextLevelUpExpNeeded;
		string minus = "-";

		if (ch.CanLevelUp() && GUI.Button (new Rect (GuiHelper.PercentW(0.2), GuiHelper.PercentH(0.2), GuiHelper.PercentW(0.1), GuiHelper.PercentH(0.1)), plus, GuiHelper.CustomButton) ) {
			ch.Life++;
		}
		if (ch.Life > MinLife && GUI.Button (new Rect (GuiHelper.PercentW(0.3), GuiHelper.PercentH(0.2), GuiHelper.PercentW(0.1), GuiHelper.PercentH(0.1)), minus, GuiHelper.CustomButton) ) {
			ch.Life--;
		}


		GuiHelper.DrawText ("Strength " + ch.Strength, GuiHelper.LittleFont, 0, 0.3);
		if (ch.CanLevelUp() && GUI.Button (new Rect (GuiHelper.PercentW(0.2), GuiHelper.PercentH(0.3), GuiHelper.PercentW(0.1), GuiHelper.PercentH(0.1)), plus, GuiHelper.CustomButton)) {
			ch.Strength++;
		}
		if (ch.Strength > MinStrength && GUI.Button (new Rect (GuiHelper.PercentW (0.3), GuiHelper.PercentH (0.3), GuiHelper.PercentW (0.1), GuiHelper.PercentH (0.1)), minus, GuiHelper.CustomButton)) {
			ch.Strength --;
		}


		GuiHelper.DrawText ("Agility " + ch.Agility, GuiHelper.LittleFont, 0, 0.4);
		if (ch.CanLevelUp() && GUI.Button (new Rect (GuiHelper.PercentW(0.2), GuiHelper.PercentH(0.4), GuiHelper.PercentW(0.1), GuiHelper.PercentH(0.1)), plus, GuiHelper.CustomButton)) {
			ch.Agility++;
		}
		if (ch.Agility > MinAgility && GUI.Button (new Rect (GuiHelper.PercentW (0.3), GuiHelper.PercentH (0.4), GuiHelper.PercentW (0.1), GuiHelper.PercentH (0.1)), minus, GuiHelper.CustomButton)) {
			ch.Agility --;
		}

		GuiHelper.DrawText ("Health " + GetComponent<HitPoints>().MaxHitPoints, GuiHelper.LittleFont, 0, 0.6);

		if (GUI.Button (new Rect (GuiHelper.PercentW(0.1), GuiHelper.PercentH(0.7), GuiHelper.PercentW(0.2), GuiHelper.PercentH(0.1)), "Apply", GuiHelper.CustomButton)) {
			World.Me.NextLevel();
			Destroy(this);
		}
	}

}
