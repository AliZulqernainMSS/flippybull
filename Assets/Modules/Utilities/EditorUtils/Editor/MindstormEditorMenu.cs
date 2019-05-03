using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MindstormEditorMenu : MonoBehaviour 
{
	// Add menu item to menu bar.
	[MenuItem("MindStorm/Reset Game")]
	static void ResetGame() {
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.Save ();
	}

	[MenuItem("MindStorm/Tools/Show Hidden Game Objects")]
	static void ShoeHiddenGameObjects() {
		Object[] objects = GameObject.FindObjectsOfType<Object>();

		for (int i = 0; i < objects.Length; ++i) {
			objects[i].hideFlags = HideFlags.None;
		}
	}

	[MenuItem("GameObject/ActiveToggle _=")]
	static void ToggleActivationSelection()
	{
		foreach(var go in Selection.gameObjects)
		{
			go.SetActive(!go.activeSelf);
		}
	}

	[MenuItem("GameObject/Disable Raycast On Images %#r")]
	static void DisableRayCastOnImageChildren()
	{
		GameObject parent = Selection.activeGameObject;
		Image[] imgObjects = parent.GetComponentsInChildren<Image> ();
		if (imgObjects != null) 
		{
			for (int i = 0; i < imgObjects.Length; i++) 
			{
				imgObjects [i].raycastTarget = false;
			}
		}
	}

	[MenuItem("MindStorm/Fill Keystore Path %&f")]
	static void FillKeyStorePath()
	{
		UnityEditor.PlayerSettings.Android.keystoreName = System.Environment.GetFolderPath (System.Environment.SpecialFolder.MyDocuments) + "/Documents/Keystore/MindstormKeystore.keystore";
		UnityEditor.PlayerSettings.Android.keystorePass = "M1ndSt0rm@lw@y5";
		UnityEditor.PlayerSettings.Android.keyaliasName = "mindstorm android";
		UnityEditor.PlayerSettings.Android.keyaliasPass = "M1ndSt0rm@lw@y5";
	}
}