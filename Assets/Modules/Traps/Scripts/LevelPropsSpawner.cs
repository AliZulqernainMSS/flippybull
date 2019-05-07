using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPropsSpawner : MonoBehaviour
{
	public float m_PropSpawnPercentage = -1f;
	public float m_NPCSpawnPercentage = -1f;
	public GameObject[] m_TrapsPrefabs;
	public GameObject[] m_NPCPrefabs;
	public Transform m_Container;
	private int m_LastSide = 0;

	void Start ()
	{
		StartCoroutine (SpawnProps (GameManager.Instance.Levels.CurrentStageModel));
	}

	private IEnumerator SpawnProps(GameStageModel stage)
	{
		float levelLength = stage.m_LevelLength;
		float traverseLength = 60f;
		while(traverseLength < levelLength)
		{
			if (Random.Range(0f, 100f) <= m_PropSpawnPercentage) 
			{
				if (Random.Range (0f, 100f) <= m_NPCSpawnPercentage) 
				{
					SpawnProp (m_NPCPrefabs[Random.Range(0, m_NPCPrefabs.Length)], traverseLength);
				} 
				else
				{
					SpawnProp (m_TrapsPrefabs[Random.Range(0, m_TrapsPrefabs.Length)], traverseLength);
				}
			}
			traverseLength += GameConstants.k_CellOffset;
			yield return null;
		}
	}

	private void SpawnProp(GameObject prefab, float length)
	{
		GameObject trap = Instantiate (prefab, m_Container);
		m_LastSide++;
		int side = Mathf.FloorToInt(Mathf.PingPong(m_LastSide, 2)) - 1;
		trap.transform.position = new Vector3(side * GameConstants.k_CellOffset, 0, length); 
		trap.SetActive (true);
	}
}
