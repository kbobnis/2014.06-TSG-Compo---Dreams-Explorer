
public class EnemyInLevelConfig
{
	public CharacterConfig CharacterConfig;
	public int WaitForSeconds;
	//enemies.Add( new EnemyInLevelConfig( characters[enemyId],  waitForSeconds) );
	public EnemyInLevelConfig(CharacterConfig cc, int waitForSeconds){
		CharacterConfig = cc;
		WaitForSeconds = waitForSeconds;
	}
}
