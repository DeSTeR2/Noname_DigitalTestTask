using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperateWithBigAndSmallPost : MonoBehaviour
{
    public GameObject parent;
    PopupPostAnim popAnim;

    bool status = false;

    // Get parent(Post in hierachy)
    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        popAnim = GetComponent<PopupPostAnim>();
    }

    void Update()
    {
        // If status was changed and this status == true
        if (popAnim.GetStatus() != status && popAnim.GetStatus() == true) {
            parent.GetComponent<PopUPParentPost>().PopUp(this.gameObject);
        } 
        status = popAnim.GetStatus();
    }

    public void ClosePopUp() {
        popAnim.GoBackAnimation();
    }
}
