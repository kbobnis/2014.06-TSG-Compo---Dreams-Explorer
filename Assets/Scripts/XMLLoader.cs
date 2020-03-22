using UnityEngine;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

public class XMLLoader  {

	private List<LevelConfig> Levels ;
	private Dictionary<string, CharacterConfig> Characters;
	public Dictionary<string, ItemConfig> Items;

	public XMLLoader() {

		/*
		XmlReaderSettings readerSettings = new XmlReaderSettings();
		readerSettings.IgnoreWhitespace = true;
		readerSettings.IgnoreComments = true;
		readerSettings.CheckCharacters = true;
		readerSettings.CloseInput = true;
		readerSettings.IgnoreProcessingInstructions = false;
		readerSettings.ValidationFlags = System.Xml.Schema.XmlSchemaValidationFlags.None;
		readerSettings.ValidationType = ValidationType.None;

		XmlReader reader = XmlReader.Create ("Assets/Resources/model.xml", readerSettings);
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.Load(reader);
		*/

		XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.LoadXml (Resources.Load<TextAsset>("model").text);

		
		Dictionary<string, SkillConfig> skills = LoadSkills (xmlDoc.GetElementsByTagName ("skills").Item(0).ChildNodes);
		Items = LoadItems (xmlDoc.GetElementsByTagName ("items").Item(0).ChildNodes, skills);

		Dictionary<string, Drop> drops = LoadDrops(xmlDoc.GetElementsByTagName("drops").Item(0).ChildNodes, Items);
		Characters = LoadCharacters (xmlDoc.GetElementsByTagName ("characters").Item(0).ChildNodes, Items, drops);
		Levels = LoadLevels (xmlDoc.GetElementsByTagName ("levels").Item(0).ChildNodes, Characters);

		XmlNode levelUpXml = xmlDoc.GetElementsByTagName ("levelUp").Item (0);
		Character.FirstLevelUp = int.Parse (levelUpXml.Attributes ["first"].Value);
		Character.NextLevelUpPercent = float.Parse (levelUpXml.Attributes ["eachMorePercent"].Value);

		XmlNode skillsXml = xmlDoc.GetElementsByTagName ("skillsParam").Item (0);
		Skill.MinT1 = float.Parse (skillsXml.Attributes ["minT1"].Value);
		Skill.MinT3 = float.Parse (skillsXml.Attributes ["minT3"].Value);

	}

	private Dictionary<string, Drop> LoadDrops(XmlNodeList dropsXml, Dictionary<string, ItemConfig> items){
		Dictionary<string, Drop> drops = new Dictionary<string, Drop> ();

		foreach(XmlNode dropXml in dropsXml){
			string id = dropXml.Attributes["id"].Value;

			List<DropItem> dropItems = new List<DropItem>();
			foreach(XmlNode dropItemXml in dropXml.ChildNodes){
				string dropItemId = dropItemXml.Attributes["id"].Value;
				string dropItemValue = dropItemXml.Attributes["value"].Value;
				dropItems.Add(new DropItem(dropItemId, dropItemValue));
			}
			drops.Add(id, new Drop(dropItems));
		}
		return drops;
	}
	private Dictionary<string, SkillConfig> LoadSkills(XmlNodeList skillsXml){
		Dictionary<string, SkillConfig> skills = new Dictionary<string, SkillConfig> ();

		foreach (XmlNode skillXml in skillsXml) {
			string skillId = skillXml.Attributes["id"].Value;
			string skillType = skillXml.Attributes["type"].Value;
			double t1 = double.Parse ( skillXml.Attributes["t1"].Value );
			double t2 = double.Parse(skillXml.Attributes["t2"].Value );
			double t3 = double.Parse (skillXml.Attributes["t3"].Value);
			double t4 = double.Parse(skillXml.Attributes["t4"].Value);
			double power = double.Parse(skillXml.Attributes["power"].Value );
			double upgradeWith = double.Parse(skillXml.Attributes["upgradeWith"].Value);
			Texture sprite = Resources.Load<Texture>(skillXml.Attributes["imagePath"].Value);
			skills.Add(skillId, new SkillConfig(skillId, skillType, t1, t2, t3, t4, power, upgradeWith, sprite));
		}

		return skills;
	}

	private Dictionary<string, ItemConfig> LoadItems(XmlNodeList itemsXml, Dictionary<string, SkillConfig> skillConfigs){
		Dictionary<string, ItemConfig> items = new Dictionary<string, ItemConfig> ();
		
		foreach (XmlNode itemXml in itemsXml) {
			string itemId = itemXml.Attributes["id"].Value;
			string instantId = itemXml.Attributes["instant"].Value;
			string skillId = itemXml.Attributes["skill"].Value;
			SkillConfig instant = instantId==""?null: skillConfigs[ instantId ];
			SkillConfig skill = skillId==""?null: skillConfigs[ skillId ];
			items.Add(itemId, new ItemConfig(itemId, instant, skill));
		}
		
		return items;
	}

	private Dictionary<string, CharacterConfig> LoadCharacters(XmlNodeList charactersXml, Dictionary<string, ItemConfig> itemConfigs, Dictionary<string, Drop> drops){
		Dictionary<string, CharacterConfig> chs = new Dictionary<string, CharacterConfig> ();
		
		foreach (XmlNode itemXml in charactersXml) {
			string itemId = itemXml.Attributes["id"].Value;
			int life = int.Parse(itemXml.Attributes["life"].Value);
			int agility = int.Parse(itemXml.Attributes["agility"].Value);
			int strenght = int.Parse(itemXml.Attributes["strength"].Value);

			string leftHandItemId = itemXml.Attributes["leftHand"].Value;
			ItemConfig leftHandItemConfig = leftHandItemId==""?null: itemConfigs[ leftHandItemId ];
			string rightHandItemId = itemXml.Attributes["rightHand"].Value;
			ItemConfig rightHandItemConfig = rightHandItemId==""?null: itemConfigs[ rightHandItemId ];

			string dropId = itemXml.Attributes["dropId"].Value;
			Drop drop = null; 
			if (dropId != ""){
				drop = drops[dropId];
			}
			
			chs.Add(itemId, new CharacterConfig(itemId, life, agility, strenght, leftHandItemConfig, rightHandItemConfig, drop));
		}
		
		return chs;
	}

	private List<LevelConfig> LoadLevels(XmlNodeList levelsXml, Dictionary<string, CharacterConfig> characters){
		List<LevelConfig> items = new List<LevelConfig> ();


		foreach (XmlNode itemXml in levelsXml) {
			List<EnemyInLevelConfig> enemies = new List<EnemyInLevelConfig>();

			foreach(XmlNode enemyXml in itemXml.ChildNodes){
				string enemyId = enemyXml.Attributes["id"].Value;
				int waitForSeconds =  int.Parse( enemyXml.Attributes["waitForSeconds"].Value );
				enemies.Add( new EnemyInLevelConfig( characters[enemyId],  waitForSeconds) );
			}
			items.Add(new LevelConfig(enemies));
		}
		
		return items;
	}

	public GameObject CreatePlayer(){
		CharacterConfig charConfig = Characters ["player"];
		RuntimeAnimatorController rac= Resources.Load<RuntimeAnimatorController> ("characterAnimator");

		GameObject playerGM = CreateCharacter (charConfig, rac);

		FollowGm fg = Camera.main.gameObject.AddComponent<FollowGm> ();
		fg.Following = playerGM;
		fg.Offset = 1f;

		playerGM.AddComponent<Mover> ();

		Character character = playerGM.GetComponent<Character> ();
		playerGM.AddComponent<DropPickuper> ();

		//input controller to the right hand
		InputContr inputRight = character.rightHand.AddComponent<InputContr> ();
		Texture2D sword = Resources.Load<Texture2D> ("images/ui/SwordButton");
		inputRight.InitMe (sword, 0.5, 0.8, 0.40, 0.15, null, KeyCode.D);
		//powering up meter right hand
		PoweringUpMeter meterRight = character.rightHand.AddComponent<PoweringUpMeter> ();
		meterRight.InitMe (character.rightHand.GetComponent<Item> (), 0.6f);

		//input controller to the left hand
		InputContr inputLeft = character.leftHand.AddComponent<InputContr> ();
		Texture2D shield = Resources.Load<Texture2D> ("images/ui/ShieldButton");
		inputLeft.InitMe (shield, 0.05, 0.8, 0.40, 0.15, null, KeyCode.A);
		//powering up meter left hand
		PoweringUpMeter meter = character.leftHand.AddComponent<PoweringUpMeter> ();
		meter.InitMe (character.leftHand.GetComponent<Item> (), 0.7f);




		return playerGM;
	}


	private GameObject CreateCharacter(CharacterConfig charConfig, RuntimeAnimatorController rac){

		GameObject playerGM = new GameObject (); 
		playerGM.name = charConfig.Id;
		Debug.Log ("creating: " + playerGM.name);
		playerGM.AddComponent<AudioSource> ();
		playerGM.AddComponent<InGamePosition> ();

		GameObject playerModel = new GameObject ();
		playerModel.name = "model";
		playerModel.transform.parent = playerGM.transform;
		SpriteRenderer sR = playerModel.AddComponent<SpriteRenderer> ();
		sR.sortingLayerName = "Second2";
		Animator animator = playerModel.AddComponent<Animator> ();
		animator.runtimeAnimatorController = rac;

		Character character = playerGM.AddComponent<Character> ();
		playerGM.AddComponent<HitPoints> ();
		character.InitMe (charConfig);


		return playerGM;
	}

	public GameObject CreateEnemy(GameObject playerGM, int enemyNumber, int levelNumber){
		GameObject enemyGM = null;

		if (levelNumber < Levels.Count) {
			LevelConfig level = Levels [levelNumber];
			if (enemyNumber < level.ListOfEnemies.Count) {
					CharacterConfig charConfig = level.ListOfEnemies [enemyNumber].CharacterConfig;
					RuntimeAnimatorController rac = Resources.Load<RuntimeAnimatorController> ("enemyAnimation");

					enemyGM = CreateCharacter (charConfig, rac);
					enemyGM.GetComponent<InGamePosition> ().X = playerGM.GetComponent<InGamePosition> ().X + 5;

					Character playerCh = playerGM.GetComponent<Character> ();
					playerCh.leftHand.GetComponent<InputContr> ().Enemy = enemyGM;
					playerCh.rightHand.GetComponent<InputContr> ().Enemy = enemyGM;

					AI ai = enemyGM.AddComponent<AI> ();

					ai.Delay = 1f;
					ai.SetChances (60, 20);
					ai.Enemy = playerGM;
			}
		}

		return enemyGM;
	}

	public float NextEnemyTime(int enemyNumber, int levelNumber, float timeSinceLastDied){
		float res = 0;

		if (levelNumber < Levels.Count) {
			LevelConfig level = Levels [levelNumber];
			if (enemyNumber < level.ListOfEnemies.Count) {
				EnemyInLevelConfig charConfig = level.ListOfEnemies [enemyNumber];
				res = charConfig.WaitForSeconds - timeSinceLastDied;
			}
		}
		return res;
	}
}

