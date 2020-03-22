using UnityEngine;
using System.Collections;

public class ShieldBlock : Skill
{
	public ShieldBlock(string id, double t1, double t2, double t3, double t4, double power, double upgradeWith, Texture image): base(id, t1, t2, t3, t4, power, upgradeWith, image){
	}

	protected override void DoAction()
	{
		DamageReducer dmg = Item.Holder.gameObject.AddComponent<DamageReducer>();
		dmg.BlockTime = T2;
		dmg.Resistance = Power;
		dmg.ActivateBlock ();

		Item.PlaySound (Sounds.Shield);

		if (GetAnimator ()) {
			GetAnimator ().SetTrigger ("block");
		}
	}


}