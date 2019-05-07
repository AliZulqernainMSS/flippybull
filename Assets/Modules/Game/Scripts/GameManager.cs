using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
		this.Invoke (ReloadGame, 1.5f);
	}

	public void LevelComplete()
	{
		this.Invoke (ReloadGame, 2f);
		PlayConfetti ();
	}

	public void ReloadGame()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
	#endregion

	public void PlayConfetti()
	{
		Destroy (GameObject.Instantiate (Resources.Load ("Confetti"), GameObject.FindGameObjectWithTag("MainCamera").transform), 5);
	}

}
