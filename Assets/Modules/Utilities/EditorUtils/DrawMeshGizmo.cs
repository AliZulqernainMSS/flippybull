using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMeshGizmo : MonoBehaviour 
{
    public Mesh mesh;
    public bool drawOnlyWhenSelected = false;

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
        Gizmos.DrawMesh(mesh, transform.position, transform.rotation, transform.lossyScale);
    }
}
