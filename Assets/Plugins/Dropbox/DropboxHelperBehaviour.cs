using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Plugins.Dropbox
{
    public class DropboxHelperBehaviour : MonoBehaviour {

        public static bool Done = false;
        private static bool Downloaded = false;

        public static async void InitDropBox() {
            await DropboxHelper.Initialize();
            await DropboxHelper.DownloadAndSaveFile("mods.json");
            await DropboxHelper.DownloadPhotos();
            Done = true;
        }

        public static async void Download(string file) {
            await DropboxHelper.DownloadAndSaveFile(file);
            Downloaded = true;
        }

        public static bool GetDownloadStatus() {
            if (Downloaded) {
                Downloaded = false;
                return true;
            }
            Downloaded = false;
            return false;
        }
#if UNITY_EDITOR

        // You can place DropboxHelperBehaviour on any scene and call those methods by RMB on a components. For easier method use

        [ContextMenu("GetAuthCode")]
        private void GetAuthCode()
        {
            DropboxHelper.GetAuthCode();
        }

        [ContextMenu("GetRefreshToken")]
        private void GetRefreshToken()
        {
            DropboxHelper.GetRefreshToken();
        }
#endif
    }
}