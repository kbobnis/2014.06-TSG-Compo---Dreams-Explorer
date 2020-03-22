using UnityEngine;
using System.Collections;

public class SwordAttack : Skill
{
	public SwordAttack(string id, double t1, double t2, double t3, double t4, double power, double upgradeWith, Texture image): base(id, t1, t2, t3, t4, power, upgradeWith, image){
	}

	protected override void DoAction()
	{
		if (Enemy != null){
			if (Enemy.GetComponent<Character> ().HasBlock ()) {
				Item.PlaySound (Sounds.HitBlocked);
			} else { 
				Item.PlaySound (Sounds.Hit);
			}

			if (Enemy != null) {
				DamageTaker dmg = Enemy.AddComponent<DamageTaker> ();
				dmg.HowMuch = Power;
			}
		}
		if (GetAnimator ()) {
			GetAnimator ().SetTrigger ("attack");
		}
	}

}