using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;

public class PopUPParentPost : MonoBehaviour
{
    [SerializeField]
    private GameObject categories;

    [SerializeField]
    private GameObject closeButton;

    GameObject popedUp;

    // Hide all ui and play animation
    public void PopUp(GameObject popedUp) {
        ClosePopUp();
        closeButton.SetActive(true);
        categories.SetActive(false);
        this.popedUp = popedUp;

        transform.GetComponent<PostHandler>().DisplayOnlyBig(popedUp);
    }

    // Show all ui and play back animation
    public void ClosePopUp() {
        categories.SetActive(true);
        closeButton.SetActive(false);
        transform.GetComponent<PostHandler>().DisplayWholeCategory();
        if (popedUp != null) 
            popedUp.GetComponent<OperateWithBigAndSmallPost>().ClosePopUp();
        popedUp = null;
    }
}
