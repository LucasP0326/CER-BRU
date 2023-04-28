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
    public bool musicLowered = false;

    public AudioSource reachedAdmin;
    public AudioSource missionSame;
    public AudioSource securingConnection;
    public AudioSource upgrade;
    public AudioSource beWarned;
    public AudioSource goodWork;

    public AudioSource voiceLog;

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
            if (musicLowered == false)
                music.GetComponent<AudioSource>().volume = (music.GetComponent<AudioSource>().volume / 5);
            musicLowered = true;
        }
        if (dialogueActive == false)
        {
            if (musicLowered == true)
                music.GetComponent<AudioSource>().volume = (music.GetComponent<AudioSource>().volume * 5);
            musicLowered = false;
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
        reachedAdmin.Play();
        timeDelay = 3f;
        yield return new WaitForSeconds(timeDelay);
        dialogue.text = "Your objective here remains the same.  Clear the area, and wipe as much information pertinent to CERAEBRU operations as possible.";
        missionSame.Play();
        timeDelay = 8f;
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
        securingConnection.Play();
        timeDelay = 9f;
        yield return new WaitForSeconds(timeDelay);
        dialogue.text = "This upgrade would give you the option to cloak yourself for a limited time, hiding yourself from enemy threats.";
        upgrade.Play();
        timeDelay = 5f;
        yield return new WaitForSeconds(timeDelay);
        dialogue.text = "Be warned, operative.  These may come in handy, but keep in mind that these are the same cybernetic enhancements that created this outbreak in the first place.  I’d advise caution in choosing whether or not to install it.";
        beWarned.Play();
        timeDelay = 11f;
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
        goodWork.Play();
        timeDelay = 5f;
        yield return new WaitForSeconds(timeDelay);
        transmission.SetActive(false);
        dialogueActive = false;
    }

    public void PlayLog()
    {
        dialogueActive = true;
        voiceLog.Play();
        StartCoroutine(Wait2());
    }

    IEnumerator Wait2()
    {
        timeDelay = 20f;
        yield return new WaitForSeconds(timeDelay);
        dialogueActive = false;
    }
}
