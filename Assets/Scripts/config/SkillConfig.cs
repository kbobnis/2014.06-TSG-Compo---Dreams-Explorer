using UnityEngine;

public class SkillConfig : AbstractConfig
{
	public string Type;
	public double T1, T2, T3, T4, Power, UpgradeWith;
	public Texture Image;

	public SkillConfig(string id, string type, double t1, double t2, double t3, double t4, double power, double upgradeWith, Texture image) : base(id){
		Type = type;
		T1 = t1;
		T2 = t2;
		T3 = t3;
		T4 = t4;
		Power = power;
		UpgradeWith = upgradeWith;
		Image = image;
	}

	public Skill CreateSkill(){
		Skill skill;
		switch (Type) {
			case "swordAttack":
				skill = new SwordAttack(Type, T1, T2, T3, T4, Power, UpgradeWith, Image);
				break;
			case "shieldBlock":
				skill = new ShieldBlock(Type, T1, T2, T3, T4, Power, UpgradeWith, Image);
				break;
			case "heal":
				skill = new Heal(Type, T1, T2, T3, T4, Power, UpgradeWith, Image);
				break;
			default:
				throw new UnityException("Skill type not recognized");	
		}

		return skill;
	}
}
