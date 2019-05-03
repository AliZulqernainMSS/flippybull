using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsSpawner : MonoBehaviour
{

	public GameObject[] m_TrapPrefab;
	public Transform m_Container;

	void Start ()
	{
		StartCoroutine (SpawnTraps ());
	}

	private IEnumerator SpawnTraps()
	{
		yield return null;
		float levelLength = GameManager.Instance.Levels.CurrentStageModel.m_LevelLength;
		float traverseLength = 40f;
		while(traverseLength < levelLength)
		{
			if (Random.value < 0.1f) 
			{
				SpawnTrap (traverseLength);
			}
			traverseLength += GameConstants.k_CellOffset;
			yield return null;
		}
	}

	private void SpawnTrap(float length)
	{
		GameObject trap = Instantiate (m_TrapPrefab[Random.Range(0, m_TrapPrefab.Length)], m_Container);
		trap.transform.position = new Vector3(Random.Range (-1, 2) * GameConstants.k_CellOffset, 0, length); 
	}
}
