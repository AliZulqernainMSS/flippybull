using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentPatch : MonoBehaviour
{
    public float m_PatchLength = -1;
    public float m_PatchAngle = -1;
    public Transform m_PatchEnd;
	private Transform[] m_Waypoints;

    [ContextMenu("Load References")]
    private void LoadReferences()
    {
        //Find Patch End Point
        m_PatchEnd = transform.Find("EndPoint");
        
        //Load Waypoints
        Transform waypointsContainer = transform.Find("Waypoints");
        if (waypointsContainer != null)
        {
            m_Waypoints = new Transform[waypointsContainer.childCount + 1];
            for (int childIndex = 0; childIndex < m_Waypoints.Length - 1; childIndex++)
            {
                m_Waypoints[childIndex] = waypointsContainer.GetChild(childIndex);
                m_Waypoints[childIndex].name = string.Format("Point ({0})", childIndex + 1);
            }
        }
        m_Waypoints[m_Waypoints.Length - 1] = m_PatchEnd;

        //Finding Angle
        // m_PatchAngle = Vector3.Angle(transform.forward, (m_PatchEnd.position - transform.position).normalized) * 2f;

        //Find Patch Length
        CalculatePatchLength();
    }

    private void CalculatePatchLength()
    {
        m_PatchLength = 0;
        for (int childIndex = 1; childIndex < m_Waypoints.Length; childIndex++)
        {
            m_PatchLength += Vector3.Distance(m_Waypoints[childIndex].position.ResetHeight(), m_Waypoints[childIndex - 1].position.ResetHeight());
        }
        m_PatchLength += Vector3.Distance(transform.position.ResetHeight(), m_Waypoints[0].position.ResetHeight());
        m_PatchLength += Vector3.Distance(m_PatchEnd.position.ResetHeight(), m_Waypoints[m_Waypoints.Length - 1].position.ResetHeight());
    }

}
