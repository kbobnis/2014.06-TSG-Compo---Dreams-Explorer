using UnityEngine;
using System.Collections;

public class DropPickuper : MonoBehaviour {

	private ItemConfig ItemConfig;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void InitMe(ItemConfig itemConfig){
		ItemConfig = itemConfig;
	}

	void OnGUI(){
		if (ItemConfig != null) {
			GuiHelper.DrawText ("drop pickuper, item: " + ItemConfig.Id, GuiHelper.LittleFont, 0.1, 0.1);
		}
	}
}
