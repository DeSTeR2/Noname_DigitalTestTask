using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Post;
using Plugins.Dropbox;
using System.Threading.Tasks;
using UnityEditor;
using System;

public class JsonDownload : MonoBehaviour
{
    private bool inited = false;

    [SerializeField]
    private Posts post;

    [SerializeField]
    private GameObject gPosts; // object post

    [SerializeField]
    private GameObject gCategories;

    [SerializeField]
    private GameObject gSearch;

    [SerializeField]
    private Slider slider;

    float sliderStatus = 0;

    private void Start() {
        // Initiate DropboxHelperBehaviour
        Init(); 
    }

    private void Update() {
        if (!inited) {
            slider.value = sliderStatus;
           // Update loading slider 
            if (sliderStatus < 0.8f)
                sliderStatus += 0.04f * Time.deltaTime;

            // Start only if Dropbox have downloaded all info
            if (DropboxHelperBehaviour.Done == true) { 
                DownloadFromJson();
                SetCategory();
                SetPosts();
                sliderStatus = 1f;
                slider.value = sliderStatus;
                gSearch.GetComponent<Search>().SetCategory(post.categories);
                inited = true;
            }
        }
    }

    private void Init() {
       DropboxHelperBehaviour.InitDropBox();

    }

    // Get all info from mods.json
    public void DownloadFromJson() {
        string path = Application.persistentDataPath + "/mods.json";
        string data = System.IO.File.ReadAllText(path);
        post = JsonUtility.FromJson<Posts>(data);
    }
    
    // Initiate all posts from mods.json
    public void SetPosts() {
        Mods[] mods = post.mods;
        for (int i=0; i< mods.Length; i++) {
            string category = mods[i].category;
            string imgPath = mods[i].preview_path;
            string title = mods[i].title;
            string description = mods[i].description;

            // Create Textrure2D from .png
            string fullPath = Application.persistentDataPath + imgPath;
            Texture2D imgTexture = new Texture2D(2, 2)

            if (File.Exists(fullPath)) {
                var rawData = System.IO.File.ReadAllBytes(fullPath);
                imgTexture.LoadImage(rawData);
            }

            imgPath = imgPath.Remove(0, 1);
            // Create new post
            gPosts.GetComponent<PostHandler>().CreatePost(imgTexture, title, description, category, imgPath);
        }
    }

    // Initiate all categories from mods.json
    public void SetCategory() {
        string[] categories = post.categories;
        for (int i = 0; i < categories.Length; i++) {
            string category = categories[i];

            // Create new category
            gCategories.GetComponent<Cutegories>().CreateCategory(category);
        }
    }

}