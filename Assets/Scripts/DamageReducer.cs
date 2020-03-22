using UnityEngine;
using System.Collections;

public class DamageReducer : MonoBehaviour {

	private double _resistance;

	public double BlockTime;
	public double Resistance {
		set { 
			if (value > 1) {
					value = 1;
			} 
			_resistance = value;
		}
		get { return _resistance; }
	}

	public void ActivateBlock ()
	{
		Destroy(this,(float)BlockTime);
	}
}
