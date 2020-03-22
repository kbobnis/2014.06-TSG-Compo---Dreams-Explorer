using UnityEngine;
using System.Collections;

public class Heal : Skill {

	public Heal(string id, double t1, double t2, double t3, double t4, double power, double upgradeWith, Texture image) : base(id, t1, t2, t3, t4, power, upgradeWith, image) {
	}
	
	protected override void DoAction()
	{
		Healer heal = Item.Holder.gameObject.AddComponent<Healer>();
		heal.HowMuch = Power;

		Item.PlaySound (Sounds.Heal);

		if (GetAnimator ()) {
			GetAnimator ().SetTrigger ("block");
		}
	}

}
