using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PopupPostAnim : MonoBehaviour
{
    [SerializeField]
    float scaleDuration;

    bool status = false; // false - if small, true - if big

    private Transform transform;
    Vector3 acnhorPosition, sizeDelta;

    RectTransform rectTransform;
    void Start()
    {
        transform = GetComponent<Transform>();
        rectTransform = transform.GetComponent<RectTransform>();
    }

    // Animation to make post bigger
    public void Animate() {
        if (status == false) {
            rectTransform.DOAnchorPos(new Vector3(1.15f, -214f, 5f), scaleDuration);
            rectTransform.DOSizeDelta(new Vector2(rectTransform.sizeDelta.x, 1300), scaleDuration);
            
            acnhorPosition = rectTransform.anchoredPosition;
            sizeDelta = rectTransform.sizeDelta;
        }

        status = true;
    }

    // Animation to make post default size and pos
    public void GoBackAnimation() {
        rectTransform.DOAnchorPos(acnhorPosition, scaleDuration);
        rectTransform.DOSizeDelta(sizeDelta, scaleDuration);

        status = false;
    }

    public bool GetStatus() {
        return status;
    }

}
