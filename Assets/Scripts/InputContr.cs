using UnityEngine;
using System.Collections;

public class InputContr : MonoBehaviour {

	private Texture2D Sword;
	private Texture2D Shield;
	private float HandDownTime;
	private bool IsDown;

	public GameObject Enemy;
	private Texture2D ActualTexture;
	private Rect ActualRect;
	private double Px, Py, Pw, Ph;
	private KeyCode KeyCodeInit;

	public void InitMe(Texture2D texture, double px, double py, double pw, double ph, GameObject enemy, KeyCode kc){
		ActualTexture = texture;
		Px = px;
		Py = py;
		Pw = pw;
		Ph = ph;
		Enemy = enemy;
		KeyCodeInit = kc;
	}

	public void HandDown(){
		if (!IsDown) {
			HandDownTime = Time.time;
			IsDown = true;
		}
		GetMeter ().Powering (Time.time - HandDownTime);
	}


	private PoweringUpMeter GetMeter(){
		return GetComponent<PoweringUpMeter> ();
	}

	private Character GetCharacter(){
		return GetComponent<Item> ().Holder;
	}

	public void HandUp(){
		if (Event.current.type != EventType.Repaint) {
			return;
		}
		Item item = GetComponent<Item> ();
		if (item == null) {
			return;
		}
		Skill skill = GetComponent<Item> ().Skill;
		Skill instant = GetComponent<Item> ().Instant;

		if (IsDown) {
			GetMeter ().UnPower ();

			if (skill != null && Time.time - HandDownTime >= skill.T1) {
				GetComponent<Item> ().ActionSkill (Enemy);
			} else if (Time.time - HandDownTime >= instant.T1) {
				GetComponent<Item> ().ActionInstant (Enemy);
			}

		} 
		IsDown = false;

	}

	public GUIStyle myStyle;

	void Start () {
		myStyle = new GUIStyle();
		//this.myStyle.border = new RectOffset(1,1,1,1);
	}

	void OnGUI () {
		if (Input.GetKey(KeyCodeInit) || GUI.RepeatButton (new Rect (GuiHelper.PercentW (Px), GuiHelper.PercentH (Py), GuiHelper.PercentW (Pw), GuiHelper.PercentH (Ph)), ActualTexture) ) {
			HandDown();
		} else if (Event.current.type == EventType.Repaint){
			HandUp();
		}
	}

}
