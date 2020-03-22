using UnityEngine;
using System.Collections;

public class HitPoints : MonoBehaviour {

	private double _hitPoints;


	public double MaxHitPoints;
	public double HitPoint{
		get { return _hitPoints; } 
		set { 
			_hitPoints = value; 
			if (_hitPoints <= 0){
				Die();
			}
			if (_hitPoints >= MaxHitPoints){
				_hitPoints = MaxHitPoints;
			}
		}
	}

	// Use this for initialization
	void Start () {
		Actualize ();
	}

	public void Actualize() {
		Character ch = GetComponent<Character> ();
		if (ch != null) {
			HitPoint = MaxHitPoints = ch.Life * 10;
			MaxHitPoints *= 2;
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI () {

		Vector3 screen = Camera.main.WorldToScreenPoint (gameObject.GetComponent<Character>().Model.transform.position);
		float px = screen.x / (float)Screen.width;
		float py = 1 -  screen.y / (float)Screen.height;

		double pt = HitPoint / (float)MaxHitPoints;

		GuiHelper.DrawElement ("images/healthBarFg", px, py, 0.1, 0.1, pt*0.1 );
		GuiHelper.DrawElement ("images/healthBarBg", px, py, 0.1, 0.1);
	}

	private void Die(){

		if (gameObject.name == "player") {
			World.Me.ShowGameOver();
		} else {
			World.Me.Enemy = null;

			Character ch = World.Me.PlayerGM.GetComponent<Character>();
			foreach(DropItem dropItem in GetComponent<Character>().Drop.Elements){
				dropItem.PickedUpBy(ch);
			}
		}

		Destroy (GetComponent<Character> ());
		Destroy (GetComponent<InputContr> ());
		Destroy (GetComponent<AI> ());
		Destroy (gameObject);
		Destroy (this);

	}

}
