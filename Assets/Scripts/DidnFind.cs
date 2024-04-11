using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DidnFind : MonoBehaviour
{
    float timer = 0;
    public float timeToLive;

    private void Awake() {
        timer = 0;
    }
    private void OnEnable() {
        timer = 0;
        
    }

    // Control life time of the pop up
    private void Update() {
        
        timer += Time.deltaTime;

        // After half life time the popup will fade down
        if (timer > timeToLive/2) {
            gameObject.GetComponent<CanvasGroup>().DOFade(0, timeToLive);
        }

        
        if (timer > timeToLive ) {
            DOTween.Clear(gameObject);
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
            gameObject.SetActive( false );
        }
    }
}
