using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CategoryActive : MonoBehaviour {

    private GameObject active;
    private GameObject inactive;

    void Start() {
        active = gameObject.transform.GetChild(0).gameObject;
        inactive = gameObject.transform.GetChild(1).gameObject;
    }

    public string GetName() {
        return active.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text.ToString();
    }

    public void SetActive() {
        active.SetActive(true);
        inactive.SetActive(false);
    }

    public void SetInctive() {
        active.SetActive(false);
        inactive.SetActive(true);
    }

    public void SetCategory(string name, UnityEngine.Events.UnityAction onClick) {
        active = gameObject.transform.GetChild(0).gameObject;
        inactive = gameObject.transform.GetChild(1).gameObject;

        //Set names
        active.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = name;
        inactive.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = name;

        //Set onClick
        active.GetComponent<Button>().onClick.AddListener(onClick);
        inactive.GetComponent<Button>().onClick.AddListener(onClick);
    }
}
