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
    public GameObject player;
    public float timeDelay;
    public float timeDelay2;
    public float timeDelay3;
    public bool showedEnemyTutorial = false;
    public bool showedTerminalTutorial = false;
    public AudioSource music;
    public bool dialogueActive = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogueActive = false;
        transmission.SetActive(false);
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Player>().killCount == 10 && showedEnemyTutorial == false)
        {
            StartCoroutine(EnemyWait());
            showedEnemyTutorial = true;
        }
        if (dialogueActive == true)
        {
            music.GetComponent<AudioSource>().volume = 0.25f;
        }
        if (dialogueActive == false)
        {
            music.GetComponent<AudioSource>().volume = 1f;
        }
    }

    IEnumerator Wait()
    {
        dialogueActive = true;
        timeDelay = 5f;
        yield return new WaitForSeconds(timeDelay);
        transmission.SetActive(true);
        caller.text = "CERAEBRU SECURITY DISPATCH";
        dialogue.text = "You have reached the administrative offices, operative.";
        yield return new WaitForSeconds(timeDelay);
        dialogue.text = "Your objective here remains the same.  Clear the area, and wipe as much information pertinent to CERAEBRU operations as possible.";
        yield return new WaitForSeconds(timeDelay);
        transmission.SetActive(false);
        dialogueActive = false;
    }

    public void InvisibilityDialogue()
    {
        if (showedTerminalTutorial == false && dialogueActive == false)
        {
            StartCoroutine(Invisibility());
            showedTerminalTutorial = true;
        }
    }

    IEnumerator Invisibility()
    {
        dialogueActive = true;
        transmission.SetActive(true);
        caller.text = "CERAEBRU SECURITY DISPATCH";
        dialogue.text = "I’m securing a connection to this terminal, Operative.  It seems that this terminal contains the necessary information to download and install a software upgrade to your cybernetics.";
        yield return new WaitForSeconds(timeDelay);
        dialogue.text = "This upgrade would give you the option to cloak yourself for a limited time, hiding yourself from enemy threats.";
        yield return new WaitForSeconds(timeDelay);
        dialogue.text = "Be warned, operative.  These may come in handy, but keep in mind that these are the same cybernetic enhancements that created this outbreak in the first place.  I’d advise caution in choosing whether or not to install it.";
        yield return new WaitForSeconds(timeDelay);
        transmission.SetActive(false);
        dialogueActive = false;
    }

    IEnumerator EnemyWait()
    {
        dialogueActive = true;
        transmission.SetActive(true);
        caller.text = "CERAEBRU SECURITY DISPATCH";
        dialogue.text = "Good work, operative.  The threats here have been neutralized.  Continue with your mission to erase all information, then enter the elevator to return to the security wing.";
        timeDelay = 5f;
        yield return new WaitForSeconds(timeDelay);
        transmission.SetActive(false);
        dialogueActive = false;
    }
}
