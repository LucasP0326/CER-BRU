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

    void OnTriggerStay (Collider other)
    {
        if(Input.GetKey(KeyCode.E) && other.gameObject.tag == "Player")
        {
            hasKey = true;
            StartCoroutine(Wait());
            transform.position = new Vector3(100.0f, 100.0f, 100.0f);
        }
        if (Input.GetButtonDown("Interact") && other.gameObject.tag == "Player") //CONTROLLER
        {
             hasKey = true;
            StartCoroutine(Wait());
            transform.position = new Vector3(100.0f, 100.0f, 100.0f);
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