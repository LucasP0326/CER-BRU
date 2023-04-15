using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OfficeEventManager : MonoBehaviour
{
    public TextMeshProUGUI dialogue;
    public TextMeshProUGUI caller;
    public GameObject transmission;
    public float timeDelay;
    public float timeDelay2;
    public float timeDelay3;
    public bool playedLockerTutorial = false;
    public bool playedSecurityTutorial = false;
    public GameObject globalVariables;
    public bool dialogueActive = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogueActive = false;
        transmission.SetActive(false);
        if (globalVariables.GetComponent<GlobalVariables>().receptionWon == true && globalVariables.GetComponent<GlobalVariables>().adminWon == false)
        {
            StartCoroutine(ReceptionWon());
        }
        if (globalVariables.GetComponent<GlobalVariables>().receptionWon == true && globalVariables.GetComponent<GlobalVariables>().adminWon == true && globalVariables.GetComponent<GlobalVariables>().labsWon == false)
        {
            StartCoroutine(AdministrationWon());
        }
        if (globalVariables.GetComponent<GlobalVariables>().labsWon == true)
        {
            StartCoroutine(LabsWon());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LockerDialogue()
    {
        if (playedLockerTutorial == false && dialogueActive == false)
        {
            StartCoroutine(Locker());
            playedLockerTutorial = true;
        }
    }

    public void SecurityDialogue()
    {
        if (playedSecurityTutorial == false && dialogueActive == false)
        {
            StartCoroutine(Security());
            playedSecurityTutorial = true;
        }
    }

    IEnumerator ReceptionWon()
    {
        dialogueActive = true;
        timeDelay = 5f;
        yield return new WaitForSeconds(timeDelay);
        transmission.SetActive(true);
        caller.text = "CERAEBRU SECURITY DISPATCH";
        dialogue.text = "Good work, operative.  You have cleared the reception area and have reached the security wing.";
        yield return new WaitForSeconds(timeDelay);
        dialogue.text = "This is your security office, and will act as your base of operations as you continue your work through the CERAEBRU facility.";
        yield return new WaitForSeconds(timeDelay);
        dialogue.text = "Take your time to explore and get acquainted with your resources.";
        yield return new WaitForSeconds(timeDelay);
        transmission.SetActive(false);
        dialogueActive = false;
    }

    IEnumerator AdministrationWon()
    {
        dialogueActive = true;
        timeDelay = 5f;
        yield return new WaitForSeconds(timeDelay);
        transmission.SetActive(true);
        caller.text = "CERAEBRU SECURITY DISPATCH";
        dialogue.text = "Good work, operative.  You handled yourself admirably there.";
        yield return new WaitForSeconds(timeDelay);
        if (globalVariables.GetComponent<GlobalVariables>().informationDeleted < 50)
        {
            dialogue.text = "I’ve noticed however, operative, that you’ve been neglecting your mission to wipe information clean from our terminals.";
            yield return new WaitForSeconds(timeDelay);
            dialogue.text = "Remember your mission, operative.  You may only be an advance scout, but we’re trusting you to wipe all information before anybody else has the chance to retrieve it.";
        }
        if (globalVariables.GetComponent<GlobalVariables>().informationDeleted > 50)
        {
            dialogue.text = "Good work as well in properly wiping information from company terminals.  Corporate will be pleased.  Continue your mission with such diligence, and there’ll be good things coming your way.";
        }
        yield return new WaitForSeconds(timeDelay);
        transmission.SetActive(false);
        dialogueActive = false;
    }

    IEnumerator LabsWon()
    {
        dialogueActive = true;
        timeDelay = 5f;
        yield return new WaitForSeconds(timeDelay);
        transmission.SetActive(true);
        caller.text = "CERAEBRU SECURITY DISPATCH";
        dialogue.text = "Operative,…connection is…spotty…right now.";
        yield return new WaitForSeconds(timeDelay);
        dialogue.text = "Something is…interfering with…transmission.";
        yield return new WaitForSeconds(timeDelay);
        dialogue.text = "I’m reading…multiple signatures…honing in on your location.  The security wing has been…breached.";
        yield return new WaitForSeconds(timeDelay);
        dialogue.text = "Operative, run.";
        yield return new WaitForSeconds(timeDelay);
        transmission.SetActive(false);
        dialogueActive = false;
    }

    IEnumerator Locker()
    {
        dialogueActive = true;
        timeDelay = 5f;
        transmission.SetActive(true);
        caller.text = "CERAEBRU SECURITY DISPATCH";
        dialogue.text = "This is where your equipment is stored.  Whether weapons or special supplies you’ve acquired while here, it will be stored here for you to bring on future expeditions into the facility.";
        yield return new WaitForSeconds(timeDelay);
        dialogue.text = "You can also restock on ammunition here.  Be aware, however, that you can only carry so much supplies on you at a given time, so while you may have a near endless stock here, don’t expect to never run out when in the field.";
        yield return new WaitForSeconds(timeDelay);
        transmission.SetActive(false);
        dialogueActive = false;
    }

    IEnumerator Security()
    {
        dialogueActive = true;
        timeDelay = 5f;
        transmission.SetActive(true);
        caller.text = "CERAEBRU SECURITY DISPATCH";
        dialogue.text = "This is your surveillance office.  From here, you can assess the status of where you’ve been in the facility and what still is left to be cleared.";
        yield return new WaitForSeconds(timeDelay);
        dialogue.text = "As you can see, the Reception area is marked as ‘cleared.’  Access the terminal to select the Administrative Offices so you can continue your mission there.";
        yield return new WaitForSeconds(timeDelay);
        transmission.SetActive(false);
        dialogueActive = false;
    }
}
