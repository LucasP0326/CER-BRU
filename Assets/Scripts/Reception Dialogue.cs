using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReceptionDialogue : MonoBehaviour
{

    public TextMeshProUGUI dialogue;
    public TextMeshProUGUI caller;
    public GameObject transmission;
    public float timeDelay;

    // Start is called before the first frame update
    void Start()
    {
        transmission.SetActive(false);
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //UI Text Delay
    IEnumerator Wait()
    {
        timeDelay = 5f;
        yield return new WaitForSeconds(timeDelay);
        transmission.SetActive(true);
        caller.text = "CERAEBRU SECURITY DISPATCH";
        dialogue.text = "Good work, operative.  You are inside the facility and have reached the reception area.";
        yield return new WaitForSeconds(timeDelay);
        dialogue.text = "As all other entrances have been sealed for containment purposes, we could not deploy you closer to the security wing.  You will need to reach it on your own.";
        yield return new WaitForSeconds(timeDelay);
        dialogue.text = "Make your way through the reception area, and remember your objective: clear out any hostiles and erase any sensitive information from the terminals.";
        yield return new WaitForSeconds(timeDelay);
        transmission.SetActive(false);
    }
}
