
public class CharacterConfig : AbstractConfig 
{
	public int Life, Agility, Strength, DropExp;
	public ItemConfig LeftItem, RightItem;
	public string Id;
	public Drop Drop;

	public CharacterConfig(string id, int life, int agility, int strength, ItemConfig leftItem, ItemConfig rightItem, Drop drop) : base(id){
		Id = id;
		Life = life;
		Agility = agility;
		Strength = strength;
		LeftItem = leftItem;
		RightItem = rightItem;
		Drop = drop;
	}
}
