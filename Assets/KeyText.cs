using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyText : MonoBehaviour
{
    public GameObject UiObject;
    public GameObject TextObject;
    public GameObject cube;
    void Start()
    {
        UiObject.SetActive(false);
        TextObject.SetActive(false);
    }

    void OnTiggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UiObject.SetActive(true);
            TextObject.SetActive(true);
        }
    }

    void OnTiggerExit(Collider other)
    {
        UiObject.SetActive(false);
        TextObject.SetActive(false);
        Destroy(cube);
    }
}