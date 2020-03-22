using UnityEngine;
using System.Collections;

public class Healer : MonoBehaviour {

	public double HowMuch;
	
	void Update () {
		gameObject.GetComponent<HitPoints> ().HitPoint += HowMuch;
		Destroy (this);
	}
}
