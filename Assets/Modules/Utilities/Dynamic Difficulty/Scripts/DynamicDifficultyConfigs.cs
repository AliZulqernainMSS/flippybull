using UnityEngine;

[CreateAssetMenu(fileName = "DynamicDifficultyConfigs", menuName = "99Falls/Dynamic Difficulty Configs")]
public class DynamicDifficultyConfigs : ScriptableObject 
{
    public float defaultWinMultiplier = 2;
    public float defaultLossMultiplier = 1;
    public float exponentialMultiplier = 0.01f;
    public float minLossMultiplier = -1f;
    public float maxWinMultiplier = 1;
    public float difficultyFactor = 0.1f;
}
