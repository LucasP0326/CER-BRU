using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public GameObject globalVariables;
    public TextMeshProUGUI dialogue;
    public TextMeshProUGUI caller;
    public RawImage callerID;
    public float timeDelay;
    public int slideNumber;
    public bool dialoguePlaying;
    public bool playedCutscene = false;

    public AudioSource music;
    public AudioSource goodMorning;
    public AudioSource haventHeard;
    public AudioSource threeDaysAgo;
    public AudioSource perCompanyStandards;
    public AudioSource instructions;
    public bool musicLowered = false;

    // Start is called before the first frame update
    void Start()
    {
        dialoguePlaying = false;
        dialogue.enabled = false;
        caller.enabled = false;
        callerID.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (globalVariables.GetComponent<GlobalVariables>().labsWon == false && playedCutscene == false)
        {
            playedCutscene = true;
           StartCoroutine(IntroCutscene());
        }

        if (globalVariables.GetComponent<GlobalVariables>().labsWon == true && playedCutscene == false)
        {
            playedCutscene = true;
            StartCoroutine(ExitCutscene());
        }

        if (dialoguePlaying == true)
        {
            if (musicLowered == false)
                music.GetComponent<AudioSource>().volume = (music.GetComponent<AudioSource>().volume / 5);
            musicLowered = true;
        }
        if (dialoguePlaying == false)
        {
            if (musicLowered == true)
                music.GetComponent<AudioSource>().volume = (music.GetComponent<AudioSource>().volume * 5);
            musicLowered = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (globalVariables.GetComponent<GlobalVariables>().labsWon == false)
                SceneManager.LoadScene("Reception");
            if (globalVariables.GetComponent<GlobalVariables>().labsWon == true)
                SceneManager.LoadScene("Main Menu");
        }
    }

    IEnumerator IntroCutscene()
    {
        dialoguePlaying = true;
        dialogue.enabled = true;
        caller.enabled = true;
        callerID.enabled = true;
        timeDelay = 5f;
        yield return new WaitForSeconds(timeDelay);
        goodMorning.Play();
        caller.text = "SECURITY DISPATCH";
        dialogue.text = "Good Morning Operative.  Apologies for activating you on such short notice, but corporate was very clear about wanting you on this one.";
        timeDelay = 6f;
        yield return new WaitForSeconds(timeDelay);
        slideNumber = 1;
        haventHeard.Play();
        dialogue.text = "You won’t have heard about this on the news as it hasn’t reached the public eye yet, and you’re going to help keep it that way.  Nevada Protocol has been enacted.";
        timeDelay = 11f;
        yield return new WaitForSeconds(timeDelay);
        slideNumber = 2;
        threeDaysAgo.Play();
        dialogue.text = "Three days ago, the CERAEBRU cerebral Implant lab outside of Redfield, South Dakota was put under code red.  Recovered security footage preceding total facility blackout suggests an experimental failure and facility-wide outbreak.";
        timeDelay = 15f;
        yield return new WaitForSeconds(timeDelay);
        slideNumber = 3;
        perCompanyStandards.Play();
        dialogue.text = "Per company security standards, the security wing of the facility has been secured, and will act as a base of operations for you and your team.  You are being deployed in advance however as forward recon.";
        timeDelay = 15f;
        yield return new WaitForSeconds(timeDelay);
        slideNumber = 4;
        instructions.Play();
        dialogue.text = "Your instructions are as follows: Obtain access to the facility’s lower levels, clear out any hostiles, and in accordance with Nevada Protocol, delete all information; leave no trace.";
        timeDelay = 15f;
        yield return new WaitForSeconds(timeDelay);
        slideNumber = 0;
        dialogue.enabled = false;
        caller.enabled = false;
        callerID.enabled = false;
        dialoguePlaying = false;
        SceneManager.LoadScene("Reception");
    }

    IEnumerator ExitCutscene()
    {
        dialoguePlaying = true;
        dialogue.enabled = true;
        caller.enabled = true;
        callerID.enabled = true;
        timeDelay = 5f;
        yield return new WaitForSeconds(timeDelay);
        caller.text = "TITAN OBSERVER";
        if (globalVariables.GetComponent<GlobalVariables>().informationDeleted >= 100)
        {
            dialogue.text = "This is Katie Sanfield with the Titan Observer, coming to you live from the CERAEBRU Neurogenics facility outside of Redfield, South Dakota where it is believed that the facility suffered from an unexpected power outage and server crash that left facilities in a state of disrepair and has, to our knowledge, resulted in some staff casualties.";
            timeDelay = 15f;
            yield return new WaitForSeconds(timeDelay);
            dialogue.text = "We are currently attempting to retrieve data from the scene, and corporate security which, has since contained the damage, has provided access to the facility as well as pertinent public records as per shareholder demands.";
            timeDelay = 15f;
            yield return new WaitForSeconds(timeDelay);
            dialogue.text = "It seems, however, that the vast majority of pertinent information was lost in the server crash, and this is sure to be a blow to this site’s corporate proceedings.  If the Redfield branch is able to hold on after this, it will be a miracle to everyone, but for now, it seems that this event can be chalked up to a simple infrastructure malfunction.";
            timeDelay = 20f;
            yield return new WaitForSeconds(timeDelay);
            dialogue.text = "This is Katie Sanfield, signing off, and this was the Titan Observer.";
            dialogue.enabled = false;
            caller.enabled = false;
            callerID.enabled = false;
            timeDelay = 5f;
            yield return new WaitForSeconds(timeDelay);
            dialogue.enabled = true;
            caller.enabled = true;
            callerID.enabled = true;
            caller.text = "Security Dispatch";
            dialogue.text = "Well done, operative.  You’ve accomplished your mission admirably, and corporate has taken note of your performance.  You can expect great things in your future here.";
            timeDelay = 8f;
            yield return new WaitForSeconds(timeDelay);
            dialogue.enabled = false;
            caller.enabled = false;
            callerID.enabled = false;
        }
        if (globalVariables.GetComponent<GlobalVariables>().informationDeleted < 100)
        {
            dialogue.text = "This is Katie Sanfield with the Titan Observer, coming to you live from the CERAEBRU Neurogenics facility outside of Redfield, South Dakota where we have learned that the facility has suffered from what seems to be an outbreak.";
            timeDelay = 15f;
            yield return new WaitForSeconds(timeDelay);
            dialogue.text = "It is believed that a number of test subjects from within the facility have “gone rogue” and rampaged throughout the facility.";
            timeDelay = 10f;
            yield return new WaitForSeconds(timeDelay);
            dialogue.text = "Security was just barely able to retake the facility, and our teams were able to enter ground zero.  Data recovered from the scene as well as pertinent files obligatorily released to shareholders reveals that numerous cybernetic experiments did, in fact, go awry, resulting in a mass outbreak.";
            timeDelay = 20f;
            yield return new WaitForSeconds(timeDelay);
            dialogue.text = "Surely, this means nothing good for the Redfield branch of CERAEBRU as well as the company at large.  Already, share prices have dropped 15% and are expected to continue to plummet, but we’ve seen incidents similar to this before, and as one of the key titans, it is expected that in due time, CERAEBRU Neurogenics will recover.  That day, however, is far off.";
            timeDelay = 25f;
            yield return new WaitForSeconds(timeDelay);
            dialogue.text = "This is Katie Sanfield, signing off, and this was the Titan Observer.";
            dialogue.enabled = false;
            caller.enabled = false;
            callerID.enabled = false;
            timeDelay = 5f;
            yield return new WaitForSeconds(timeDelay);
            dialogue.enabled = true;
            caller.enabled = true;
            callerID.enabled = true;
            caller.text = "Security Dispatch";
            dialogue.text = "The facility is secure, Operative, but I must say that your performance failed to meet our hopes.  The company is going to take a hit from this, and we all know who corporate is going to want to take it out on first.  I can’t protect you any more, operative.  You’re on your own.";
            timeDelay = 15f;
            yield return new WaitForSeconds(timeDelay);
            dialogue.enabled = false;
            caller.enabled = false;
            callerID.enabled = false;
        }
        if (globalVariables.GetComponent<GlobalVariables>().informationSaved >= 75)
        {
            dialogue.text = "This is Katie Sanfield with the Titan Observer, coming to you live from the CERAEBRU Neurogenics facility outside of Redfield, South Dakota where we have learned that the facility has suffered from what seems to be an outbreak.";
            timeDelay = 15f;
            yield return new WaitForSeconds(timeDelay);
            dialogue.text = "It is believed that a number of test subjects from within the facility have “gone rogue” and rampaged throughout the facility.";
            timeDelay = 10f;
            yield return new WaitForSeconds(timeDelay);
            dialogue.text = "Security was just barely able to retake the facility, and our teams were able to enter ground zero.  Data recovered from the scene as well as pertinent files obligatorily released to shareholders reveals that numerous cybernetic experiments did, in fact, go awry, resulting in a mass outbreak.";
            timeDelay = 20f;
            yield return new WaitForSeconds(timeDelay);
            dialogue.text = "Already, share prices have dropped 35% and are expected to continue to plummet.  We predict that it is only a matter of time too before our anti-corporative Federal government begins to move in to implicate CERAEBRU Neurogenics for their malfeasance.  One way or another, the business world is being shaken, and CERAEBRU is on the edge.";
            timeDelay = 23f;
            yield return new WaitForSeconds(timeDelay);
            dialogue.text = "This is Katie Sanfield, signing off, and this was the Titan Observer.";
            dialogue.enabled = false;
            caller.enabled = false;
            callerID.enabled = false;
            timeDelay = 5f;
            yield return new WaitForSeconds(timeDelay);
            dialogue.enabled = true;
            caller.enabled = true;
            callerID.enabled = true;
            caller.text = "Security Dispatch";
            dialogue.text = "Operative.  We need to talk.";
            timeDelay = 7f;
            yield return new WaitForSeconds(timeDelay);
            dialogue.enabled = false;
            caller.enabled = false;
            callerID.enabled = false;
        }
        dialoguePlaying = false;
        SceneManager.LoadScene("MainMenu");
    }
}
