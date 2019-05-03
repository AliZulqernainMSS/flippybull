using UnityEngine;
using UnityEditor;
using System.IO;

public static class TakeScreenshotInEditor
{
    private const string screenshotsDirectory = "Unity Screenshot";
    private static readonly string screenshotsDirectoryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments)+"/Documents";

    [MenuItem("MindStorm/Tools/Take Screenshot of Game View %^s")]
    static void TakeScreenshot()
    {
        string path = Path.Combine(screenshotsDirectoryPath, screenshotsDirectory);
        path = Path.Combine(path, Application.productName);
        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }
        int number = 1;

        while (File.Exists(Path.Combine(path, number + ".png")))
        {
            number++;
        }

        ScreenCapture.CaptureScreenshot(Path.Combine(path, number + ".png"), 1);
    }
}
