using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float lifeTime = 0;

	void Start () 
    {
        Destroy(gameObject, lifeTime);
	}
	
}
