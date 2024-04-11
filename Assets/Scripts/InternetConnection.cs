using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternetConnection : MonoBehaviour
{
    [SerializeField]
    bool connected = false;
    [SerializeField]
    private GameObject connectionLost;
    void Update()
    {
        CheckInternet();
        connectionLost.SetActive(!connected);
    }

    public void CheckInternet() {
        if (Application.internetReachability == NetworkReachability.NotReachable) {
            connected = false;
        } else {
            connected = true;
        }
    }
}
