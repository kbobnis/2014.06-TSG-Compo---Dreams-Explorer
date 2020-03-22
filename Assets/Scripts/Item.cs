using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public Skill Instant;
	public Skill Skill;

	public Character Holder;

	// Use this for initialization
	void Start () {
		gameObject.AddComponent<AudioSource> ();
	
	}

	public void InitMe(ItemConfig itemConfig, Character ch){
		Instant = itemConfig.Instant.CreateSkill();
		Instant.Item = this;
		Skill = itemConfig.Skill==null?null:itemConfig.Skill.CreateSkill ();
		if (Skill != null) {
			Skill.Item = this;
		}
		Holder = ch;
	}
	
	// Update is called once per frame
	void Update () {
		if (Instant != null) {
			Instant.Update ();
		}
		if (Skill != null) {
			Skill.Update ();
		}
	
	}

	public void ActionInstant(GameObject enemy, bool withT1=false){
		if (!Holder.IsBusy () && Instant != null) {
			Instant.StartAction2 (enemy, withT1);
		}
	}

	public void ActionSkill(GameObject enemy, bool withT1=false){
		if (!Holder.IsBusy () && Skill != null ) {
			Skill.StartAction2 (enemy, withT1);
		}
	}

	public void PlaySound(AudioClip ac){
		GetComponent<AudioSource> ().clip = ac;
		GetComponent<AudioSource> ().Play ();
	}

}
