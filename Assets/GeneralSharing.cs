using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

// Share
public class GeneralSharing : MonoBehaviour
{
    public void SharePhoto(string path) {
        StartCoroutine(TakeScreenshotAndShare(path));
    }

    private IEnumerator TakeScreenshotAndShare(string path) {
        yield return new WaitForEndOfFrame();

        var rawData = System.IO.File.ReadAllBytes(Application.persistentDataPath + "/" + path);
        Texture2D ss = new Texture2D(2, 2); 
        ss.LoadImage(rawData);

        path = path.Replace("mods/", "");
        string filePath = Path.Combine(Application.temporaryCachePath, path);
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        new NativeShare().AddFile(filePath)
            .SetSubject("Subject goes here").SetText("Hello world!").SetUrl("https://github.com/yasirkula/UnityNativeShare")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();
    }
}
