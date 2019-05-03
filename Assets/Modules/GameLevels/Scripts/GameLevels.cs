using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameLevels
{
    public GameLevelModel CurrentLevelModel { get; private set; }
    public GameStageModel CurrentStageModel { get; private set; }
    public int CurrentLevelNumber { get; private set; }
    public int CurrentStageNumber { get; private set; }
    public int LastLevelNumber { get; private set; }
    public int LastStageNumber { get; private set; }
    public bool IsLastLevelUp { get; private set; }

    private GameLevelConfigs m_LevelsData;

    public GameLevels(int level, int stage, GameLevelConfigs levelConfigs)
    {
        CurrentLevelNumber = level;
        CurrentStageNumber = stage;
            
        m_LevelsData = levelConfigs;

        CurrentLevelModel = GetLevelModel(CurrentLevelNumber);
        CurrentStageModel = GetStageModel(CurrentStageNumber);

        IsLastLevelUp = false;
    }

	public void StageCompleted()
	{
        CurrentStageNumber++;
        IsLastLevelUp = false;

        if (CurrentStageNumber > CurrentLevelModel.m_Stages.Length)
        {
            LevelUp();
            IsLastLevelUp = true;
        }
        CurrentStageModel = GetStageModel(CurrentStageNumber);

        LastLevelNumber = CurrentLevelNumber <= 1 ? LastLevelNumber = CurrentLevelNumber : LastLevelNumber = CurrentLevelNumber - 1;
        LastStageNumber = CurrentStageNumber <= 1 ? LastStageNumber = CurrentStageNumber : LastStageNumber = CurrentStageNumber - 1;
    }

    public void LevelUp()
    {
        CurrentLevelNumber++;

        CurrentStageNumber = 1;

        GameLevelModel level = GetLevelModel(CurrentLevelNumber);
        level.m_LevelNumber = CurrentLevelNumber;
        CurrentLevelModel = level;
    }

    public GameLevelModel GetLevelModel (int levelNumber)
	{
		int levelIndex = levelNumber - 1;
		if (levelIndex < m_LevelsData.m_Levels.Length)
		{
			return m_LevelsData.m_Levels[levelIndex];
		}
		PrettyLogger.Log (string.Format("Level {0} is out of bound at index {1}", levelNumber, levelIndex), LogColor.Red);
		return m_LevelsData.m_Levels[m_LevelsData.m_Levels.Length - 1];
	}

    public GameStageModel GetStageModel(int stageNumber)
    {
        int stageIndex = stageNumber - 1;
        if(stageIndex >= CurrentLevelModel.m_Stages.Length)
        {
            PrettyLogger.Log(string.Format("Stage {0} is out of bound at index {1}", stageNumber, stageIndex), LogColor.Red);
            stageIndex = CurrentLevelModel.m_Stages.Length - 1;
        }
        return CurrentLevelModel.m_Stages[stageIndex];
    }
}
