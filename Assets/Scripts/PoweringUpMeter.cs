using UnityEngine;
using System.Collections;

public class PoweringUpMeter : MonoBehaviour {

	public Item Item;
	public float Y;

	private float MeterValue;
	private Item LoadingItem;


	public void InitMe(Item item, float y){
		Item = item;
		Y = y;
	}

	public void Powering(float time){
		MeterValue = time;
		//GetComponent<Character> ().Model.GetComponent<Animator> ().SetTrigger ("loading");
	}

	void Update(){
	}

	public void UnPower(){
		MeterValue = 0;
		//GetComponent<Character> ().Model.GetComponent<Animator> ().SetTrigger ("stand");
	}

	private float MaxMeterSeconds = 5f;

	void OnGUI(){
		GuiHelper.DrawElement ("images/ui/ProgressBarEmpty", 0.1, Y, 0.8, 0.1);
		GuiHelper.DrawElement ("images/ui/ProgressBarFull", 0.1, Y, 0.8, 0.1, MeterValue / MaxMeterSeconds);

		DrawSkillOnBar (Item.Instant);
		DrawSkillOnBar (Item.Skill);

	}

	private void DrawSkillOnBar(Skill skill){
		if (skill != null) {
			double leftInstantT1 = skill.T1 / MaxMeterSeconds;
			double sizeX = 0.03;
			float middleX = GuiHelper.PercentW (0.1 + leftInstantT1) - skill.Image.width / 2 * (float)sizeX;
			GUI.DrawTexture (new Rect (middleX, GuiHelper.PercentH (Y), GuiHelper.PercentW (sizeX), GuiHelper.PercentH (0.1)), skill.Image);
		}
	}

}
