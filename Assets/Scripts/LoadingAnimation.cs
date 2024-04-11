using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingAnimation : MonoBehaviour
{
    private CanvasGroup CanvasGroup;
    private Slider slider;

    private float timeToWait = 0.8f;
    void Start()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
        slider = gameObject.transform.GetChild(1).transform.gameObject.GetComponent<Slider>();
    }

    
    void Update()
    {
        // If info have been loaded enter the app
        if (slider.value == slider.maxValue) {
            timeToWait -= Time.deltaTime;
            // Wait a little bit
            if (timeToWait < 0) {
                Animation();
                if (CanvasGroup.alpha < 0.1f) {
                    gameObject.SetActive(false);
                }
            }
        }
    }

    void Animation() {
        CanvasGroup.DOFade(0, 1f);
    }
}
