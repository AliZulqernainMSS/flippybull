using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScreenLogger : Singleton<ScreenLogger>
{
	private Func<string>[] logs;
	private int maxLogs = 10; 

	public override void Initialize ()
	{
		base.Initialize ();
		logs 	 = new Func<string> [maxLogs];
		SetUpDrawer ();
	}

	public static void Observe(Func<string> target)
	{
		Instance.AddObserver (target);
	}

	public static void Clear()
	{
		Instance.ClearLogs ();
	}

	private void ClearLogs()
	{
		for (int index = 0; index < logs.Length; index++)
		{
			logs [index] 	 = null;
		}
	}

	private void AddObserver (Func<string> target)
	{
		SiftLogsIndex ();
		int lastIndex = Instance.logs.Length - 1;
		logs 	 [lastIndex] = target;
	}

	private void SiftLogsIndex()
	{
		for (int index = 1; index < logs.Length; index++)
		{
			logs [index - 1] = logs [index];
		}
	}

	private Rect startRect;
	private GUIStyle style;

	private void SetUpDrawer()
	{
		startRect = new Rect (10, 0, 200, 60);
		style = new GUIStyle ();
		style.fontSize = 50;
		style.normal.textColor = Color.black;
	}

	void OnGUI()
	{
		Rect rect = new Rect (startRect);
		for (int logIndex = 0; logIndex < logs.Length; logIndex++)
		{
			var log = logs [logIndex];
			if(log != null)
			{
				rect.y = kScreenLogger.topOffset + kScreenLogger.rowOffest * logIndex;
				GUI.TextArea (rect, log(), 100, style);
			}
		}
	}

	private class kScreenLogger
	{
		public const int topOffset = 150;
		public const int rowOffest = 60;
	}
}
