using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Plugins.Dropbox;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine.Networking;
using System;
public class PostHandler : MonoBehaviour
{
    [SerializeField]
    public string curCategory;

    [SerializeField]
    private GameObject postPrefab;

    [SerializeField]
    private GeneralSharing shareCode;


    private List<GameObject> posts;
    private List<GameObject> displayPost;

    private int inited = 0;
    // Start is called before the first frame update
    void Start()
    {
        posts = new List<GameObject>();
        displayPost = new List<GameObject>();
        

        int childCout = gameObject.transform.childCount;
        for (int i = 0; i < childCout; i++) {
            posts.Add(gameObject.transform.GetChild(i).gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // Initial post loads
        if (inited < 10) {
            if (DropboxHelperBehaviour.Done == true) { // start only if Dropbox downloaded all info
                inited += DisplayPost() == true? 1:0;
            }
        }
    }

    // Display post according to global category
    public bool DisplayPost() {
        displayPost.Clear();
        for (int i = 0; i < posts.Count; i++) {
            posts[i].transform.position = this.transform.position;
            if (posts[i].GetComponent<PostInit>().Activate(curCategory)) { // returns true if posts[i] 
                                                                           // have the same category 
                                                                           // as a global one
                displayPost.Add(posts[i]);

                // Calculate and set position
                Vector3 position = posts[i].transform.position;
                RectTransform rv = (RectTransform)(posts[i].transform);

                float yPosition = (displayPost.Count-1) * (rv.rect.height + 150);
                position.y -= yPosition;

                posts[i].transform.position = position;

            } 
        }
        return displayPost.Count > 0;
    }

    // Hide all posts exept the big one
    public void DisplayOnlyBig(GameObject big) {
        for (int i = 0; i < displayPost.Count; i++) {
            if (displayPost[i].gameObject != big.gameObject) {
                displayPost[i].gameObject.SetActive(false);
            }
        }
    }

    // Show all posts in curCategery
    public void DisplayWholeCategory() {
        for (int i = 0; i < displayPost.Count; i++) {
            displayPost[i].gameObject.SetActive(true);
        }
    }

    public void ChangeCategory(string newCategory) {
        curCategory = newCategory;
        DisplayPost();
    }

    public void CreatePost(Texture2D inputImg, string inputTitle, string inputDescription, string inputCategory, string imgPath) {
        GameObject post = Instantiate(postPrefab, this.transform.position, Quaternion.identity) as GameObject;
        post.transform?.GetComponent<PostInit>()?.SetPost(inputImg, inputTitle, inputDescription, inputCategory, imgPath, shareCode);
        post.transform.SetParent(this.transform);
        posts.Add(post);
    }
}
