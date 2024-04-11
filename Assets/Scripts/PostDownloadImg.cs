using TMPro;
using UnityEngine;
using DG.Tweening;

public class PostDownloadImg : MonoBehaviour
{
    bool animate = false;
    float timer = 0;
    string imgPath;
    

    [SerializeField]
    float timeForNewText;

    [SerializeField]
    GameObject image;

    [SerializeField]
    TextMeshProUGUI text;

    GeneralSharing share;
    private void Start() {
        UpdateAnimation();
    }

    
    void Update()
    {
        if (!animate) return;

        if (Plugins.Dropbox.DropboxHelperBehaviour.GetDownloadStatus() == true) {
            StopAnimate();
        }

        // Rotate image 
        Vector3 vector3 = new Vector3(0,0,360f);
        if (image != null)
            image?.gameObject?.transform?.DORotate(vector3, 5f, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart)
                .SetRelative()
                .SetEase(Ease.Linear);

        // Create "writting" effect 
        timer += Time.deltaTime;
        if (timer >  timeForNewText) {
            timer = 0;
            string sText = text.text;
            int dots = 0;
            for (int i=sText.Length-1; i>=0;i--) {
                if (sText[i] == '.') {
                    dots++;
                } else break;
            }

            if (dots == 3) {
                sText = sText.Remove(sText.Length - 3, 3);
            } else {
                sText += ".";
            }

            text.text = sText;
        }
    }
    
    // Change download animations according to status "Download"\"No download"
    private void UpdateAnimation() {
        for (int i = 0; i < gameObject.transform.childCount; i++) {
            gameObject.transform.GetChild(i).gameObject.SetActive(animate);
        }
    }

    // Set UnityNativeShare
    public void SetShare(GeneralSharing share) {
        this.share = share;
    }

    // Start download animation
    public void Animate() {
        animate = true;
        UpdateAnimation();
        Plugins.Dropbox.DropboxHelperBehaviour.Download(imgPath);
    }

    // Stop download animation
    public void StopAnimate() {
        animate = false;
        share.SharePhoto(imgPath);
        UpdateAnimation();
    }

    public void SetImgPath(string path) {
        this.imgPath = path;
    }
}
