using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdministrationEventManager : MonoBehaviour
{

    public TextMeshProUGUI dialogue;
    public TextMeshProUGUI caller;
    public GameObject transmission;
    public float timeDelay;
    public float timeDelay2;
    public float timeDelay3;

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

    IEnumerator Wait()
    {
        timeDelay = 5f;
        yield return new WaitForSeconds(timeDelay);
        transmission.SetActive(true);
        caller.text = "CERAEBRU SECURITY DISPATCH";
        dialogue.text = "You have reached the administrative offices, operative.";
        yield return new WaitForSeconds(timeDelay);
        dialogue.text = "Your objective here remains the same.  Clear the area, and wipe as much information pertinent to CERAEBRU operations as possible.";
        yield return new WaitForSeconds(timeDelay);
        transmission.SetActive(false);
    }
}
