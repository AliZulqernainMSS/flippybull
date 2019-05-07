using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHuman : MonoBehaviour
{
	public Collider m_Collider;
	public Transform m_AvatarContainer;
	public GameObject[] m_Avatars;
	public GameObject[] m_Ragdolls;
	private int m_Index;

	private Animator m_Animator;

	public void Start()
	{
		LoadAvatar ();
	}

	void OnEnable()
	{
		m_Collider.AddOnTiggerEnterListener (CollisionDetected);
	}

	void OnDisable()
	{
		m_Collider.RemoveOnTiggerEnterListener (CollisionDetected);
	}

	public void LoadAvatar()
	{
		m_Index = Random.Range (0, m_Avatars.Length);
		LoadAvatar (m_Avatars[m_Index]);
	}

	public void LoadAvatar(GameObject avatar)
	{
		GameObject clone = Instantiate (avatar, m_AvatarContainer);
		clone.transform.localPosition = Vector3.zero;
		clone.transform.localEulerAngles = Vector3.zero;
		m_Animator = clone.GetComponent<Animator> ();
		m_Animator.Play ("Idle" + Random.Range(1, 5));
	}

	private void CollisionDetected(Collider other)
	{
		if (other.CompareTag ("Player")) 
		{
			Die (other.transform);
		}
	}

	public void Die(Transform other)
	{
		gameObject.SetActive (false);
		GameObject clone = Instantiate (m_Ragdolls[m_Index]);
		Vector3 direction = (transform.position - other.GetComponentInParent<Player> ().m_LastPosition).normalized;
		clone.transform.position = transform.position;
		clone.GetComponent<Rigidbody> ().AddForce (direction * Random.Range(900f, 1500f), ForceMode.Impulse);

		Destroy (gameObject, 1);
		Destroy (clone, 5);
	}

}
