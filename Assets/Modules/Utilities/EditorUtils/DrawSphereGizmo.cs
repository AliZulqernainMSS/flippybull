using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSphereGizmo : MonoBehaviour 
{
    public bool drawOnlyWhenSelected = false;
    public float sphereSize = 1f;

    private void OnDrawGizmosSelected()
    {
        if(drawOnlyWhenSelected)
        {
            DrawSphere();
        }
    }

    private void OnDrawGizmos()
    {
        if (drawOnlyWhenSelected == false)
        {
            DrawSphere();
        }
    }

    private void DrawSphere()
    {
        Gizmos.DrawSphere(transform.position, sphereSize);
    }
}
