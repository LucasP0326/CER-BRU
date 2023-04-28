using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LaboratoriesEventManager : MonoBehaviour
{
    public TextMeshProUGUI dialogue;
    public TextMeshProUGUI caller;
    public GameObject transmission;
    public GameObject player;
    public float timeDelay;
    public AudioSource music;
    public bool dialogueActive = false;
    public bool musicLowered = false;
    public GameObject labFootage;
    public GameObject[] encounterEnemies;
    public GameObject door1;
    public GameObject door2;
    public GameObject door3;
    public GameObject door4;

    public AudioSource reachedLab;
    public AudioSource numerousHostiles;
    public AudioSource doNotEngage;
    public AudioSource losingSignal;

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

    public void startEncounter(){
        foreach (var enemy in encounterEnemies){
            enemy.GetComponent<Enemy>().encounterActive = true;
        door1.GetComponent<Door>().doorOpen = true;
        door2.GetComponent<Door>().doorOpen = true;
        door3.GetComponent<Door>().doorOpen = true;
        door4.GetComponent<Door>().doorOpen = true;
        }
    }

    public void SignalDialogue()
    {
        if (dialogueActive == false)
            StartCoroutine(losingSignal2());
    }

    IEnumerator Wait()
    {
        dialogueActive = true;
        timeDelay = 5f;
        yield return new WaitForSeconds(timeDelay);
        transmission.SetActive(true);
        labFootage.SetActive(false);
        caller.text = "CERAEBRU SECURITY DISPATCH";
        dialogue.text = "Operative, you have reached the laboratories and…wait.  Do not go past that door.";
        reachedLab.Play();
        timeDelay = 6f;
        yield return new WaitForSeconds(timeDelay);
        labFootage.SetActive(true);
        dialogue.text = "I am reading numerous hostile signals on the other side of that door.  Far too many for you to handle.";
        numerousHostiles.Play();
        timeDelay = 7f;
        yield return new WaitForSeconds(timeDelay);
        labFootage.SetActive(false);
        dialogue.text = "Operative, do not engage the enemy.  I suggest looking for another way around.  Remember your objective–wipe all information.";
        doNotEngage.Play();
        timeDelay = 8f;
        yield return new WaitForSeconds(timeDelay);
        transmission.SetActive(false);
        dialogueActive = false;
    }

    IEnumerator losingSignal2()
    {
        dialogueActive = true;
        transmission.SetActive(true);
        caller.text = "CERAEBRU SECURITY DISPATCH";
        dialogue.text = "Operative, I’m beginning…to lose signal.  Remember…your objective…";
        losingSignal.Play();
        timeDelay = 6f;
        yield return new WaitForSeconds(timeDelay);
        transmission.SetActive(false);
        dialogueActive = false;
    }
}
