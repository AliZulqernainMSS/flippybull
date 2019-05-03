using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PrettyLogger
{
	public static void Log (string log, LogColor color = LogColor.Gray)
	{
		log = string.Format ("<color={1}>{0}</color>", log, color.ToString ());
		Debug.Log (log);
	}
}

public enum LogColor
{
	White,
	Gray,
	Red,
	Green,
	Blue,
	Cyan
}
