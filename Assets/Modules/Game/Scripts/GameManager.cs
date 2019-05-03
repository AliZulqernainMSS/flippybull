using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public GameLevels Levels { get; private set;}

	void Awake()
	{
		Levels = new GameLevels(1,1, Resources.Load<GameLevelConfigs>("Levels"));
	}

	#region level logic
	public void PlayerDied()
	{
		
	}

	public void LevelComplete()
	{
		
	}
	#endregion

}
