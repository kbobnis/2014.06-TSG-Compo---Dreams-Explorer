
public class ItemConfig : AbstractConfig 
{
	public SkillConfig Instant, Skill;

	public ItemConfig(string id, SkillConfig instant, SkillConfig skill): base(id){
		Instant = instant;
		Skill = skill;
	}
}
