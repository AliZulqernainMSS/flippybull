using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    public Rect rect = new Rect(0, 0.02f, 1, 0.02f);
    public GUIStyle style;
    float deltaTime = 0.0f;

	void Start()
	{
        //Application.targetFrameRate = 60;

        rect.x *= Screen.width;
        rect.y *= Screen.height;

        rect.width *= Screen.width;
        rect.height *= Screen.height;
	}

	void Update()
	{
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
	}

	void OnGUI()
    {
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
		style.normal.textColor = (fps >= 30) ? Color.green : ((fps < 20) ? Color.red : Color.yellow);
		string text = string.Format("{0:0.} fps ({1:0.0} ms)", fps, msec);
		GUI.Label(rect, text, style);
	}
}
