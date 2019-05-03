using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadGradient : MonoBehaviour
{
    public Color startColor = Color.red;
    public Color endColor = Color.blue;

    void Start()
    {
        UpdateColors();
    }

    [ContextMenu("UpdateColors")]
    private void UpdateColors()
    {
        var mesh = GetComponent<MeshFilter>().sharedMesh;
        var colors = new Color[mesh.vertices.Length];
        colors[0] = startColor;
        colors[1] = endColor;
        colors[2] = startColor;
        colors[3] = endColor;
        mesh.colors = colors;
    }

}
