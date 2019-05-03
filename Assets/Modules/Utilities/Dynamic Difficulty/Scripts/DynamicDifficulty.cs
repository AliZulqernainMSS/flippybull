using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicDifficulty
{
    private DynamicDifficultyConfigs difficultyConfigs;
    private float winMultiplier;
    private float lossMultiplier;

    public float CurrentDifficultyValue { get; private set; }

    public DynamicDifficulty(DynamicDifficultyConfigs configs, float difficultyValue)
    {
        CurrentDifficultyValue = difficultyValue;
        difficultyConfigs      = configs;
        UpdateMultipliers();
    }

    public DynamicDifficulty(float difficultyVlaue)
    {
        CurrentDifficultyValue = difficultyVlaue;
        difficultyConfigs      = Resources.Load<DynamicDifficultyConfigs>("DynamicDifficultyConfigs");
        UpdateMultipliers();
    }

    private void UpdateMultipliers()
    {
        winMultiplier   = difficultyConfigs.defaultWinMultiplier;
        lossMultiplier = difficultyConfigs.defaultLossMultiplier;
    }

    public void PlayerLost() 
    {
        CurrentDifficultyValue  -= (difficultyConfigs.difficultyFactor * lossMultiplier);
        CurrentDifficultyValue   = Mathf.Max(CurrentDifficultyValue, difficultyConfigs.minLossMultiplier);
        lossMultiplier          += difficultyConfigs.exponentialMultiplier;
        winMultiplier            = difficultyConfigs.defaultWinMultiplier;
    }

    public void PlayerWon()
    {
        CurrentDifficultyValue += (difficultyConfigs.difficultyFactor * winMultiplier);
        CurrentDifficultyValue  = Mathf.Min(CurrentDifficultyValue, difficultyConfigs.maxWinMultiplier);
        winMultiplier          += difficultyConfigs.exponentialMultiplier;
        lossMultiplier          = difficultyConfigs.defaultLossMultiplier;
    }
}
