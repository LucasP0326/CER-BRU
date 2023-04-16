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

    // Start is called before the first frame update
    void Start()
    {
        dialogue.enabled = false;
        caller.enabled = false;
        callerID.enabled = false;
        if (globalVariables.GetComponent<GlobalVariables>().labsWon == false)
        {
           StartCoroutine(IntroCutscene());
        }

        if (globalVariables.GetComponent<GlobalVariables>().labsWon == true)
        {
            StartCoroutine(ExitCutscene());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator IntroCutscene()
    {
        dialogue.enabled = true;
        caller.enabled = true;
        callerID.enabled = true;
        timeDelay = 5f;
        yield return new WaitForSeconds(timeDelay);
        caller.text = "SECURITY DISPATCH";
        dialogue.text = "Good Morning Operative.  Apologies for activating you on such short notice, but corporate was very clear about wanting you on this one.";
        yield return new WaitForSeconds(timeDelay);
        slideNumber = 1;
        dialogue.text = "You won’t have heard about this on the news as it hasn’t reached the public eye yet, and you’re going to help keep it that way.  Nevada Protocol has been enacted.";
        yield return new WaitForSeconds(timeDelay);
        slideNumber = 2;
        dialogue.text = "Three days ago, the CERAEBRU cerebral Implant lab outside of Redfield, South Dakota was put under code red.  Recovered security footage preceding total facility blackout suggests an experimental failure and facility-wide outbreak.";
        timeDelay = 8f;
        yield return new WaitForSeconds(timeDelay);
        slideNumber = 3;
        dialogue.text = "Per company security standards, the security wing of the facility has been secured, and will act as a base of operations for you and your team.  You are being deployed in advance however as forward recon.";
        yield return new WaitForSeconds(timeDelay);
        slideNumber = 4;
        dialogue.text = "Your instructions are as follows: Obtain access to the facility’s lower levels, clear out any hostiles, and in accordance with Nevada Protocol, delete all information; leave no trace.";
        timeDelay = 5f;
        yield return new WaitForSeconds(timeDelay);
        slideNumber = 0;
        dialogue.enabled = false;
        caller.enabled = false;
        callerID.enabled = false;
        SceneManager.LoadScene("Reception");
    }

    IEnumerator ExitCutscene()
    {
        timeDelay = 5f;
        yield return new WaitForSeconds(timeDelay);
        caller.text = "TITAN OBSERVER";
    }
}
