using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class FontColorHandler : MonoBehaviour {
    private TextMeshProUGUI text;
    private Button button;
    private Color defauldColor;
    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetChild(0).gameObject.transform.GetComponent<TextMeshProUGUI>();
        button = GetComponent<Button>();
        defauldColor = text.color;

        // Set start window as "Mods"
        if (this.gameObject.name == "Button (2)") {
            text.DOColor(Color.black, 1);
            button.GetComponent<Image>().DOColor(Color.black, 1);
        }
    }

    // Handle bottom bar icon colors
    void Update()
    {
        // Get current selected window button
        GameObject sel = EventSystem.current?.currentSelectedGameObject?.gameObject;
        if (sel == null) return;

        // If selected is this object so change status color
        if (sel == this.gameObject) {
            text.DOColor(Color.black, 1);
            button.GetComponent<Image>().DOColor(Color.black, 1);
        } else {
            // If selected another window button so change status color to default
            if (sel.name[0] == 'B') {
                text.DOColor(defauldColor, 1);
                button.GetComponent<Image>().DOColor(defauldColor, 1);
            }
        }
    }

}
