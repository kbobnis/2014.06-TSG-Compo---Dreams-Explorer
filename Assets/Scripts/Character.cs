using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	private int _life;
	public int _strength;
	public int _agility;

	public static int FirstLevelUp;
	public static float NextLevelUpPercent;
	public int NextLevelUpExpNeeded;
	public int _actualExp;
	public int TotalExp;


	public GameObject leftHand;
	public GameObject rightHand;

	public GameObject Model;
	public Drop Drop;


	public int Strength {
		set { 
			StatChange( value - _strength );
			_strength = value; } 
		get { return _strength; } 
	}
	public int Agility {
		set { 
			StatChange( value - _agility );
			_agility = value; }
		get { return _agility; } 

	}
	public int Life {
		set {
			StatChange( value - _life);
			_life = value; 
			GetComponent<HitPoints>().Actualize();
		}
		get { return _life; }
	}
	public int ActualExp {
		set { 
			_actualExp = value;
			if (value > 0){
				TotalExp = value;
			}
		}
		get { return _actualExp; }
	}

	private void StatChange(int increase){
		for(int i=0; i < Mathf.Abs(increase); i++){
			OneStatChange(increase>0);
		}
	}

	private void OneStatChange(bool increase){
		int expChange = (int)((increase ? -1 : 1) * NextLevelUpExpNeeded);
		ActualExp += expChange;
		NextLevelUpExpNeeded = (int)Mathf.Abs (expChange * (1 + NextLevelUpPercent / 100));
	}

	public bool CanLevelUp(){
		return ActualExp > NextLevelUpExpNeeded;
	}


	public void InitMe(CharacterConfig ch){
		NextLevelUpExpNeeded = FirstLevelUp;

		ActualExp = 15000;

		Life = ch.Life;
		Agility = ch.Agility;
		Strength = ch.Strength;

		ActualExp = 15000;

		if (ch.LeftItem != null) {
			leftHand = new GameObject ();
			leftHand.name = "leftHand";
			leftHand.transform.parent = gameObject.transform;
			Item leftHandItem = leftHand.AddComponent<Item> ();
			leftHandItem.InitMe (ch.LeftItem, this);
		}

		if (ch.RightItem != null) {
			rightHand = new GameObject ();
			rightHand.name = "rightHand";
			rightHand.transform.parent = gameObject.transform;
			Item rightHandItem = rightHand.AddComponent<Item> ();
				rightHandItem.InitMe (ch.RightItem, this);
		}

		Drop = ch.Drop;

		foreach (Transform child in gameObject.transform) {
			GameObject el = child.gameObject;
			if (el.name == "model"){
				Model = el;
			}
		}
	}

	public bool IsBusy(){
		//check both hands if actually skill is not activated

		bool isBusy = false;

		if (leftHand != null ) {
			Item leftItem = leftHand.GetComponent<Item> ();
			if (leftItem.Instant != null && leftItem.Instant.Started) {
					isBusy = true;
			}
			if (leftItem.Skill != null && leftItem.Skill.Started) {
					isBusy = true;
			}
		}

		if (rightHand != null) {
			Item rightItem = rightHand.GetComponent<Item>();
			if (rightItem.Instant != null && rightItem.Instant.Started){
				isBusy = true;
			}
			if (rightItem.Skill != null && rightItem.Skill.Started){
				isBusy = true;
			}
		}
		return isBusy;
	}

	public bool HasBlock(){
		return GetComponent<DamageReducer> () != null;
	}

	void OnGUI(){

		Vector3 screen = Camera.main.WorldToScreenPoint (gameObject.GetComponent<Character>().Model.transform.position);
		float px = ( screen.x  )/ (float)Screen.width ;
		float py = 0.2f; 
    }

	public bool CanEnemyBeSpawn(){
		return true;
	}




}