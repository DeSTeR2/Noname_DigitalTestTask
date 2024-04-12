using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


public class LoadingPost : MonoBehaviour
{
    bool animate = true;
    float timer = 0;

    [SerializeField]
    float timeForNewText;

    [SerializeField]
    GameObject image;

    [SerializeField]
    TextMeshProUGUI text;

    [SerializeField]
    GameObject Downloading;

    private void Start() {
        UpdateAnimation();
    }


    void Update() {
        if (!animate) return;

        // Rotate image 
        Vector3 vector3 = new Vector3(0, 0, 360f);
        if (image != null)
            image?.gameObject?.transform?.DORotate(vector3, 5f, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart)
                .SetRelative()
                .SetEase(Ease.Linear);

        // Create "writting" effect 
        timer += Time.deltaTime;
        if (timer > timeForNewText) {
            timer = 0;
            string sText = text.text;
            int dots = 0;
            for (int i = sText.Length - 1; i >= 0; i--) {
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

    // Stop download animation
    public void StopAnimate() {
        Downloading.transform?.GetComponent<PostDownloadImg>().CanDownload();
        animate = false;
        UpdateAnimation();
    }

    public bool IsAnimating { get { return animate; } }
}
