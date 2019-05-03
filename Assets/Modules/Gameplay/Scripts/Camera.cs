using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
	public Transform m_Player;

	void Start ()
	{
		
	}
	
	void Update ()
	{
		Vector3 finalPosition = transform.position;
        finalPosition.z = m_Player.position.z;
		transform.position = Vector3.Lerp(transform.position, finalPosition, Time.deltaTime * 5);
	}
}
