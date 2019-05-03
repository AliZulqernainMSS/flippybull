using System.Collections;
using UnityEngine;

public class GameEnvironment : MonoBehaviour
{
    public Transform m_PatchesContainer;
    public Transform m_LastEndPoint;
    public EnvironmentPatch[] m_SimplePatchPrefabs;

    private System.Random m_PatchNumberGenerator;
    private float m_PatchViewsAngle = 0;
    private EnvironmentType m_EnvironmentType;
    private int m_LastPatchIndex = -1;
    private EnvironmentPatch[] m_Patches;

    private void Start()
    {
        DynamicGI.UpdateEnvironment();
        m_PatchNumberGenerator = new System.Random();
        StartLevelGeneration();
    }

    public void StartLevelGeneration()
    {
        StartCoroutine(GenerateLevelEnvironment());
    }

    private IEnumerator GenerateLevelEnvironment()
    {
		GameStageModel CurrentStageModel = GameManager.Instance.Levels.CurrentStageModel;
        LoadPatches(m_EnvironmentType);
        float levelLength = CurrentStageModel.m_LevelLength;
        levelLength -= CreateFirstPatch();
        while (levelLength > 0)
        {
            levelLength -= CreatePatch(0);
        }
		yield return null;

		CreatePatch(1);
        //Add Extra Patch at End
		CreatePatch(0);

    }

    private void LoadPatches(EnvironmentType type)
    {
        m_Patches = m_SimplePatchPrefabs;
    }

    private float CreateFirstPatch()
    {
        return CreatePatch(0);
    }

    private float CreatePatch(int patchIndex)
    {
        EnvironmentPatch patch = LoadPatchClone(m_LastEndPoint.position.ResetHeight(), patchIndex);
		m_LastEndPoint = patch.m_PatchEnd;
		m_PatchViewsAngle += patch.m_PatchAngle;

        return patch.m_PatchLength;
    }

    private EnvironmentPatch LoadPatchClone(Vector3 position, int patchIndex)
    {
        EnvironmentPatch patch = Instantiate(m_Patches[patchIndex], m_PatchesContainer);
        patch.gameObject.SetActive(true);
        patch.transform.position = position;
        Vector3 patchAngles = patch.transform.localEulerAngles;
        patchAngles.y = m_PatchViewsAngle;
        patch.transform.localEulerAngles = patchAngles;
        return patch;
    }

    private int GetNextPatchIndex()
    {
        int patchIndex = m_PatchNumberGenerator.Next(m_Patches.Length);
        while (Mathf.Abs(m_PatchViewsAngle + m_Patches[patchIndex].m_PatchAngle) > 165f || m_LastPatchIndex == patchIndex)
        {
            patchIndex = m_PatchNumberGenerator.Next(m_Patches.Length);
        }
        m_LastPatchIndex = patchIndex;
        return patchIndex;
    }

}
