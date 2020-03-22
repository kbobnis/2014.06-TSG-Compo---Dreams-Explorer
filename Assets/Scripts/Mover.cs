using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public const int MIN_DISTANCE = 2;
	public float Speed = 2;

	public void InitMe(float speed){
		Speed = speed;
	}


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		float x = World.FindNearestEnemyDistance();
		if (Mathf.Abs (x) - Mathf.Abs (MIN_DISTANCE) < 0) {
			GetComponent<Character> ().Model.GetComponent<Animator> ().SetTrigger ("stand");
			Speed = 0;
		} else {
			GetComponent<Character> ().Model.GetComponent<Animator> ().SetTrigger ("walk");
			Speed = 2;
		}



		if (Speed > 0) {
			GetComponent<InGamePosition> ().X += Speed * Time.deltaTime;

			if (!GetComponent<AudioSource> ().isPlaying) {
				GetComponent<AudioSource> ().clip = Sounds.Step;
				GetComponent<AudioSource> ().Play ();
			}
		}
	}
}
