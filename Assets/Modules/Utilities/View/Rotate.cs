using UnityEngine;

[ExecuteInEditMode]
public class Rotate : MonoBehaviour
{
	public Vector3 axis = Vector3.forward;
	public float speed = 5;

	void Update ()
	{
		transform.Rotate (axis, -speed * Time.deltaTime);
	}
}
