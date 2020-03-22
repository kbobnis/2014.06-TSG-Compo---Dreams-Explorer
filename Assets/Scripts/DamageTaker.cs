using UnityEngine;
using System.Collections;

public class DamageTaker : MonoBehaviour {

	public double HowMuch;
	
	void Update () {
		double resistance = 0;
		if (GetComponent<DamageReducer> ()) {
			resistance = gameObject.GetComponent<DamageReducer> ().Resistance;

		}
		if (gameObject != null) {
			AudioSource asi = GetComponent<AudioSource>();
			if (!asi.isPlaying){
				if (resistance < 0.5 ){
					asi.clip = Sounds.Pain2;
					if (gameObject.name == "player"){
						Handheld.Vibrate();
						GetComponent<AudioSource>().Play();
					}
				}

			}

			gameObject.GetComponent<HitPoints> ().HitPoint -= HowMuch * (1 - (float)resistance);
			Destroy (this);
		}

	}
}
