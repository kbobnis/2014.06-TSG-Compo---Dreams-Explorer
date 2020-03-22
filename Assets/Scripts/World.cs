using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {


	public GameObject PlayerGM;
	private GameObject _enemy;
	public GameObject Enemy{
		get { return _enemy; }
		set { 
			if (value == null){
				LastEnemyDied = Time.time;
			}
			_enemy = value; 
		}
	}

	public static World Me;

	public int NextEnemyInt = 0;
	public int NextLevelInt = 0;
	public float LastEnemyDied = 0;

	public XMLLoader XmlLoader;

	void Start () {
		Sounds.Pain1 = Resources.Load<AudioClip>("music/sounds/bol");
		Sounds.Pain2 = Resources.Load<AudioClip>("music/sounds/bol2");
		Sounds.Step = Resources.Load<AudioClip> ("music/sounds/krok1");
		Sounds.Shield = Resources.Load<AudioClip> ("music/sounds/tarcza");
		Sounds.Hit = Resources.Load<AudioClip>("music/sounds/hit");
		Sounds.Heal = Resources.Load<AudioClip> ("music/sounds/lvlUp");
		Sounds.HitBlocked = Resources.Load<AudioClip> ("music/sounds/hit_blocked");

		XmlLoader = new XMLLoader ();
		Me = this;

		PlayerGM = XmlLoader.CreatePlayer ();
	}

	void Update(){
		if (Enemy == null && PlayerGM.GetComponent<Character>().CanEnemyBeSpawn() && XmlLoader.NextEnemyTime(NextEnemyInt, NextLevelInt, Time.time - LastEnemyDied) <= 0) {
			Enemy = XmlLoader.CreateEnemy(PlayerGM, NextEnemyInt++, NextLevelInt);
			if (Enemy == null && PlayerGM.GetComponent<LevelUper>() == null){
				EndLevel();
			}
		}

		if (Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit(); 
		}
	}

	void EndLevel(){
		PlayerGM.GetComponent<Mover> ().enabled = false;
		PlayerGM.GetComponent<Character> ().leftHand.GetComponent<InputContr> ().enabled = false;
		PlayerGM.GetComponent<Character> ().leftHand.GetComponent<PoweringUpMeter> ().enabled = false;
		PlayerGM.GetComponent<Character> ().rightHand.GetComponent<InputContr> ().enabled = false;
		PlayerGM.GetComponent<Character> ().rightHand.GetComponent<PoweringUpMeter> ().enabled = false;
		PlayerGM.AddComponent<LevelUper> ();
	}

	public void NextLevel(){
		PlayerGM.GetComponent<Mover> ().enabled = true;
		NextLevelInt ++;
		NextEnemyInt = 0;
		PlayerGM.GetComponent<Character> ().leftHand.GetComponent<InputContr> ().enabled = true;
		PlayerGM.GetComponent<Character> ().leftHand.GetComponent<PoweringUpMeter> ().enabled = true;
		PlayerGM.GetComponent<Character> ().rightHand.GetComponent<InputContr> ().enabled = true;
		PlayerGM.GetComponent<Character> ().rightHand.GetComponent<PoweringUpMeter> ().enabled = true;
	}

	void OnGUI(){
		GuiHelper.InitMe ();
	}

	public void ShowGameOver(){
		Application.LoadLevel("gameOverScene");
	}

	public static float FindNearestEnemyDistance(){
		float dist = Mover.MIN_DISTANCE + 0.1f;
		if (World.Me.Enemy != null ) {
			dist = Mathf.Abs (World.Me.PlayerGM.GetComponent<InGamePosition> ().X - World.Me.Enemy.GetComponent<InGamePosition> ().X);
		}
		return dist;
	}

}
