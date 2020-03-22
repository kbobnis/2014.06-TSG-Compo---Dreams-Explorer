using UnityEngine;
using System.Collections;

public class FollowGm : MonoBehaviour {

	public GameObject Following;
	public float Offset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Following != null) {
			GetComponent<InGamePosition>().X = Following.GetComponent<InGamePosition>().X + Offset;
		}
	}
}
