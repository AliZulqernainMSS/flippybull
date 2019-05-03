using UnityEngine;
using System;
using System.IO;

[CreateAssetMenu(fileName = "Levels", menuName = "Game/Levels")]
public class GameLevelConfigs : ScriptableObject
{
	public GameLevelModel[] m_Levels;
}

[System.Serializable]
public struct GameLevelModel
{
	public int              m_LevelNumber;
    public GameStageModel[] m_Stages;
}

[System.Serializable]
public struct GameStageModel
{
    public float   m_LevelLength;

}
