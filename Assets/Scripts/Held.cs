using UnityEngine;
using System.Collections;

public class Held
{
	public Item Item;

	public Animator GetAnimator(){
		Animator a = null;
		if (GetCharacter() != null && Item.Holder.GetComponent<Character>().Model != null) {
			a = Item.Holder.GetComponent<Character> ().Model.GetComponent<Animator> ();
		}
		return a;
	}

	public Character GetCharacter(){
		Character ch = null;
		if (Item != null && Item.Holder != null ) {
			ch = Item.Holder.GetComponent<Character> ();
		}
		return ch;
	}
}