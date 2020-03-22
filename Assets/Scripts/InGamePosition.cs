using UnityEngine;
using System.Collections;

public class InGamePosition : MonoBehaviour {

	public float X;
	public float Z;
	public float Y;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 oldPos = transform.position;
		float newZ = Z;
		if (newZ == 0) {
				newZ = oldPos.z;
		} 
		float newY = Y;
		if (newY == 0) {
			newY = oldPos.y;
		}

		transform.position = new Vector3( X, newY, newZ);
	}
}
