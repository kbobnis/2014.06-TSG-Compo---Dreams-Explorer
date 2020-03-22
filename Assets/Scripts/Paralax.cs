using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Paralax : MonoBehaviour {

	public Sprite[] SpriteTemplates;
	public float ScrollSpeed;
	public float GenerateChance; //value from 0 do 1
	public string LayerName;

	private float LastCameraWasAt ;
	private List<GameObject> Sprites = new List<GameObject>();
	private int TileNumber = 32;


	private float SumOfSize = 0;
	private GameObject Main;

	// Use this for initialization
	void Start () {
		Main = new GameObject ();
		Main.name = "paralax." + LayerName;

		LastCameraWasAt = Camera.main.gameObject.GetComponent<InGamePosition> ().X;

		float lastInserted = -TileNumber / 2;
		float size = 1;//SpritePixelsToUnitsSize * 400;
		for (int i=-TileNumber/2 ; i < TileNumber/2 ; i++) {

			if (WillGenerateSprite()){
				GameObject tile = PrepareSprite(lastInserted);
				Sprites.Add(tile);
				Bounds b = tile.GetComponent<SpriteRenderer>().bounds;
				size = b.max.x - b.min.x;
			}
			lastInserted += size ;
			SumOfSize += Mathf.Abs( size );
		}
	}

	// Update is called once per frame
	void Update () {

		float nowCamera = Camera.main.gameObject.GetComponent<InGamePosition> ().X;

		//lets check the first
		if (Sprites.Count > 0){
			GameObject sprite = Sprites[0];

			Vector3 screen = Camera.main.WorldToScreenPoint (sprite.transform.position + sprite.GetComponent<SpriteRenderer>().sprite.bounds.max);
			if (screen.x < 0){
				sprite.GetComponent<InGamePosition>().X += SumOfSize;
				Sprites.Remove(sprite);
				Sprites.Add(sprite);
			}
		}

		//update all elements
		foreach (GameObject sprite in Sprites) {
			sprite.GetComponent<InGamePosition>().X += ( nowCamera - LastCameraWasAt) * ScrollSpeed;
		}

		LastCameraWasAt = nowCamera;
	}

	private bool WillGenerateSprite(){
		return Random.value < GenerateChance;
	}

	private GameObject PrepareSprite(float insertAt){
		
		GameObject tile = new GameObject ();
		tile.name = "paralax."+LayerName;
		SpriteRenderer r = tile.AddComponent<SpriteRenderer>();
		r.sortingLayerName = LayerName;
		r.sprite = GetRandomSprite ();
		InGamePosition igp3 = tile.AddComponent<InGamePosition> ();
		igp3.X = insertAt ;
		igp3.Z = 0;
		igp3.Y = -2;
		tile.transform.parent = Main.transform;
		return tile;
	}

	private Sprite GetRandomSprite(){
		int rnd = (int)(Random.value * SpriteTemplates.Length);
		return SpriteTemplates[rnd] as Sprite;
	}
	



}
