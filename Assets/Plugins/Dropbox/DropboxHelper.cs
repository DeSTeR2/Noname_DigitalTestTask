using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

using Post;
using UnityEditor;
using System.IO;

namespace Plugins.Dropbox
{
    public static class DropboxHelper
    {
        // paste from dropbox console
        private const string AppKey = "grr8w8pnylo5onj";
        private const string AppSecret = "jlv4xmimt72motr";

#if UNITY_EDITOR
        // paste auth code from your browser, or given from production department, here. Only valid for about 10 minutes.
        // You can remove it after getting refreshToken
        private const string AuthCode = "kOdeSrJjs08AAAAAAAAAL5J2TumE_dI3vTCtfYbB4w8";
#endif

        // paste from GetRefreshToken() result, value of "refresh_token"
        private const string RefreshToken = "xjs_rWL6s_QAAAAAAAAAAaJCOKiGz7InW9DRHAs24psZYFR4xx1f18nWBPeqlne5";


        private static string _tempRuntimeToken = null;

#if UNITY_EDITOR
        // First, call this method to get an authCode, then paste it in the appropriate field above.
        public static void GetAuthCode()
        {
            var url = $"https://www.dropbox.com/oauth2/authorize?client_id={AppKey}&response_type=code&token_access_type=offline";
            Application.OpenURL(url);
        }

        // After you have pasted an AuthCode, call this method to get refreshToken.
        public static async void GetRefreshToken()
        {
            Debug.Log("Requesting refreshToken...");

            var form = new WWWForm();
            form.AddField("code", AuthCode);
            form.AddField("grant_type", "authorization_code");

            var base64Authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{AppKey}:{AppSecret}"));

            using var request = UnityWebRequest.Post("https://api.dropbox.com/oauth2/token", form);
            request.SetRequestHeader("Authorization", $"Basic {base64Authorization}");

            var sendRequest = request.SendWebRequest();

            while (!sendRequest.isDone)
            {
                await Task.Yield();
            }

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                Debug.LogError(request.downloadHandler.text);
                return;
            }
            
            var parsedAnswer = JObject.Parse(request.downloadHandler.text);
            var refreshTokenString = parsedAnswer["refresh_token"]?.Value<string>();

           Debug.Log("Copy this string to RefreshToken: " + refreshTokenString);
        }
#endif

        /// <summary>
        /// Call initialization before you start download any files and await it's completion.
        /// To wait inside a coroutine you can use:
        /// var task = DropboxHelper.Initialize();
        /// yield return new WaitUntil(() => task.IsCompleted);
        /// </summary>
        public static async Task Initialize()
        {
            if (string.IsNullOrEmpty(RefreshToken))
            {
                Debug.LogError("refreshToken should be defined before runtime");
            }

            var form = new WWWForm();
            form.AddField("grant_type", "refresh_token");
            form.AddField("refresh_token", RefreshToken);

            var base64Authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{AppKey}:{AppSecret}"));

            using var request = UnityWebRequest.Post("https://api.dropbox.com/oauth2/token", form);
            request.SetRequestHeader("Authorization", $"Basic {base64Authorization}");

            var sendRequest = request.SendWebRequest();

            while (!sendRequest.isDone)
            {
                await Task.Yield();
            }

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                Debug.LogError(request.downloadHandler.text);
            }

            Debug.Log("Success! Full message from dropbox: " + request.downloadHandler.text);

           var data = JObject.Parse(request.downloadHandler.text);
           _tempRuntimeToken = data["access_token"]?.Value<string>();

            Debug.Log("Token: " + _tempRuntimeToken);
        }

        /// <summary>
        /// Creating a request for file download.
        /// To wait inside a coroutine you can use:
        /// var task = DropboxHelper.GetRequestForFileDownload();
        /// yield return new WaitUntil(() => task.IsCompleted);
        /// </summary>
        /// <param name="relativePathToFile">Pass a relative path from a root folder. Example: "images/image1"</param>
        /// <returns>WebRequest that you should send and then process it's result</returns>
        public static UnityWebRequest GetRequestForFileDownload(string relativePathToFile)
        {
            var request = UnityWebRequest.Get("https://content.dropboxapi.com/2/files/download");
            request.SetRequestHeader("Authorization", $"Bearer {_tempRuntimeToken}");
            request.SetRequestHeader("Dropbox-API-Arg", $"{{\"path\": \"/{relativePathToFile}\"}}");
            
            return request;
        }
        
        public static async Task DownloadAndSaveFile(string relativePathToFile)
        {
            // Create a download request
            UnityWebRequest downloadRequest = GetRequestForFileDownload(relativePathToFile);

            // Send the download request
            var operation = downloadRequest.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Delay(100); // Wait until the request is completed
            }


            if (downloadRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to download file: " + downloadRequest.error);
            }
            else
            {
                //Save the downloaded file to the persistent data path
                string filePath = Application.persistentDataPath + "/" + relativePathToFile;
                System.IO.File.WriteAllBytes(filePath, downloadRequest.downloadHandler.data);
                Debug.Log("File saved to: " + filePath);
            }
        }

        public static async Task DownloadPhotos() {
            string path = Application.persistentDataPath + "/mods.json";
            string data = System.IO.File.ReadAllText(path);
            Posts post = JsonUtility.FromJson<Posts>(data);

            string saveFolder = Path.Combine(Application.persistentDataPath, "mods");
            
            if (!Directory.Exists(saveFolder))
                Directory.CreateDirectory(saveFolder);

            int imageNumber = post.mods.Count();

            for (int i=0; i<imageNumber; i++) {
                string pathImg = post.mods[i].preview_path;
                pathImg = pathImg.Remove(0,1);
                Debug.Log(pathImg);
                await DownloadAndSaveFile(pathImg);
            }
        }

    }

}