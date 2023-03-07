using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Key : MonoBehaviour
{
    public TextMeshProUGUI textObject;
    public float timeDelay;
    public bool hasKey = false;
    public GameObject card;

    // Update is called once per frame

    void Start()
    {
        textObject.enabled = false;
    }

    void OnTriggerStay ()
    {
        if(Input.GetKey(KeyCode.E))
        {
            hasKey = true;
            StartCoroutine(Wait());
        }
    }

    //UI Text Delay
    IEnumerator Wait()
    {
        textObject.enabled = true;
        textObject.text = "YOU HAVE OBTAINED A KEYCARD";
        timeDelay = 3f;
        yield return new WaitForSeconds(timeDelay);
        textObject.enabled = false;
        
    }
}