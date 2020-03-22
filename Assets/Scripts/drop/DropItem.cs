using UnityEngine;
using System.Collections;

public class DropItem  {

	protected string Id;
	protected string Value;
	
	public DropItem(string id, string value){
		Id = id;
		Value = value;
		switch (Id) {
			case "exp":
			case "item":
				break;
			default:
				throw new UnityException("There is no drop item with id " + id);
		}
	}

	public void PickedUpBy(Character ch){
		switch (Id) {
			case "exp": 
				ch.ActualExp += int.Parse(Value);
				break;
			case "item":
				DropPickuper dp = ch.gameObject.AddComponent<DropPickuper>();
				dp.InitMe(World.Me.XmlLoader.Items[Value]);
				break;
			default:
				throw new UnityException("There is no wut wut");

		}
	}


}
