using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PostInit : MonoBehaviour {

    [SerializeField]
    private RawImage image;

    [SerializeField]
    private TextMeshProUGUI title;

    [SerializeField]
    private TextMeshProUGUI description;

    [SerializeField]
    private string category;

    private GameObject postDownload;

    [SerializeField]
    PostDownloadImg sPostDownload;

    private Texture2D inputImg;
    private string inputTitle;
    private string inputDescription;
    private string inputCategory;

    public bool active = false;
    void Start() {
        image = gameObject.transform.GetChild(1).GetComponent<RawImage>();
        title = gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        description = gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        image.texture = inputImg;
        title.text = inputTitle;
        description.text = inputDescription;
        category = inputCategory;
    }


    public void SetPost(Texture2D inputImg, string inputTitle, string inputDescription, string inputCategory, string imgPath, GeneralSharing shareCode) {
        this.inputImg = inputImg;
        this.inputTitle = inputTitle;
        this.inputDescription = inputDescription;
        this.inputCategory = inputCategory;

        sPostDownload.SetImgPath(imgPath);
        sPostDownload.SetShare(shareCode);
    }

    // Returns true if this.category == category, else - no 
    // Sets activate status to all childrens according to "active"
    public bool Activate(string category) {
        active = (bool)(this.category == category);
        gameObject.GetComponent<Image>().enabled = active;
        for (int i=0; i<gameObject.transform.childCount;i++) {
            gameObject.transform.GetChild(i).gameObject.SetActive(active);
        }
        return active;
    }
}
