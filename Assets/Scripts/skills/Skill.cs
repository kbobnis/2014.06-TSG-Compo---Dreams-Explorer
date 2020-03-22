using UnityEngine;
using System.Collections;


public abstract class Skill : Held {

	private double _t1;
	private double _t2;
	private double _t3;
	private double _t4;
	private double _power;
	private double UpgradeWith;
	private float TimeStarted;
	private bool T1Done, T2Done, T3Done, T4Done;
	private bool WithT1;

	protected GameObject Enemy;

	public string Id;
	public const float SCALE_FACTOR_T = 1f;
	public const float WEIGHT = 0.01f;
	public bool Started = false;
	public bool Finished = true;

	public static float MinT1;
	public static float MinT3;
	public Texture Image;

	public double Power{
		set { _power = value; }
		get { return _power + UpgradeWith * GetCharacter ().Strength; }
	}
	public double T1 {
		set { _t1 = value; }
		get {
			double tmpT1 =  _t1 - GetCharacter().Agility * SCALE_FACTOR_T * WEIGHT ; 
			if (tmpT1 < MinT1){ tmpT1 = MinT1; }
			return tmpT1;
		} 
	}
	public double T2 {
		set { _t2 = value; }
		get { return _t2 ; } 
	}
	
	public double T3 {
		set { _t3 = value; } 
		get { 
			double tmpT3 = _t3 - GetCharacter ().Agility * SCALE_FACTOR_T * WEIGHT;
			if (tmpT3 < MinT3) { tmpT3 = MinT3; }
			return tmpT3;
		}
	}
	public double T4 {
		set { _t4 = value; }
		get { 
			double tmpT4 = _t4 - GetCharacter ().Agility * SCALE_FACTOR_T * WEIGHT;
			if (tmpT4 < 0.1) {
					tmpT4 = 0.1;
			}
			return tmpT4;
		}
	}

	public Skill(string id, double t1, double t2, double t3, double t4, double power, double upgradeWith, Texture image)   {
		Power = power;
		UpgradeWith = upgradeWith;
		Id = id;
		if (t1 == 0 || t2 == 0 || t3 == 0) {
			throw new UnityException("t1, t2, t3 can not be zero.");
		}
		T1 = t1;
		T2 = t2;
		T3 = t3;
		T4 = t4;
		Finished = true;
		Image = image;
	}

	public void Update () {
		float delta = Time.time - TimeStarted;

		double t1Time = WithT1 ? T1 : 0;
		double t2Time = t1Time + T2;
		double t3Time = t2Time + T3;
		double t4Time = t3Time + T4;
		if (Started) {

			if (delta < t1Time && WithT1) {
				DoT1 ();
			} else if (delta < t2Time) {
				DoT2 ();
			} else if (delta < t3Time) {
				DoT3 ();
			} else if (delta < t4Time) {
				DoFinish ();
				Started = false;
				DoT4 ();
			}
		} else if(!Finished && delta > t4Time){
			Finished = true;
		}

	}

	private void DoT1(){
		if (!T1Done) {
			GetAnimator().SetTrigger("loading");
			T1Done = true;
		}
	}


	private void DoT2(){
		if (!T2Done) {
			DoAction();
			T2Done = true;
		}

	}

	private void DoT4(){
		if (!T4Done) {
			T4Done = true;
		}
	}

	protected abstract void DoAction();

	private void DoT3 () {
		if (!T3Done) {
			GetAnimator ().SetTrigger ("cooldown");
			T3Done = true;
		}
	}
	private void DoFinish(){
		GetAnimator ().SetTrigger ("stand");
	}

	public void StartAction2(GameObject enemy, bool withT1=false){
		if (Finished) {
			TimeStarted = Time.time;
			Enemy = enemy;
			Started = true;
			T2Done = false;
			T3Done = false;
			T1Done = false;
			T4Done = false;
			WithT1 = withT1;
			Finished = false;
		} else {
			Debug.Log("Cooldown is active");
		}
	}

	
	public override string ToString(){
		return Id +", T1: " + T1.ToString("F3") + ", T2: " + T2.ToString("F3") + ", T3: " + T3.ToString("F3") + ", T4: " + T4.ToString("F3") + " " + Power;
	}


}
