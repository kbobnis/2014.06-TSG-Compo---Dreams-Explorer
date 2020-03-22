using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

	public float Delay;
	public int DoInstantChance;
	public int DoSkillChance;
	public GameObject Enemy;

	private bool CanAttack = true;
	private float LastAttack ;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - LastAttack > Delay) {
			CanAttack = true;
		}

		if (CanAttack) {
			int ticket = Mathf.RoundToInt(Random.value * 100);

			GameObject rightHandGM = GetComponent<Character>().rightHand;
			Item rightHandItem = rightHandGM==null?null:rightHandGM.GetComponent<Item>();

			if (rightHandGM != null){
				if (ticket < DoInstantChance){
					rightHandItem.ActionInstant(Enemy, true);
				} else if (ticket < (DoInstantChance + DoSkillChance) ){
					rightHandItem.ActionSkill(Enemy, true);
				}
			}
			CanAttack = false;
			LastAttack = Time.time;
		}
	}

	public void SetChances(int doInstant, int doSkill){
		DoInstantChance = doInstant;
		DoSkillChance = doSkill;
	}

}
