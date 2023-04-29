using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContainmentCellTerminal : MonoBehaviour
{

    public GameObject terminalScreen;

    public static bool GameIsPaused = false;
    public GameObject email1;
    public GameObject email2;
    public GameObject email3;
    public GameObject email4;
    public TextMeshProUGUI deletedMessage;
    public bool emailsDeleted = false;
    public bool emailsSaved = false;
    public int informationValue = 0;
    public GameObject player;
    public bool screenOpen = false;
    public AudioSource voiceLog;
    public AudioSource voiceLog2;
    public GameObject playButton;
    public GameObject playButton2;
    public GameObject screen;

    public Material staticMat;
    public Material deleteMat;

    // Start is called before the first frame update
    void Start()
    {
        screen.GetComponent<MeshRenderer>().material = staticMat;
        terminalScreen.SetActive(false);
        email1.SetActive(false);
        email2.SetActive(false);
        email3.SetActive(false);
        email4.SetActive(false);
        deletedMessage.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0f)
        {
            screenOpen = false;
        }

        if (screenOpen == false)
        {
            terminalScreen.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
        }
        if (Input.GetButtonDown("Cancel"))
        {
            if (GameIsPaused)
            {
                Resume();
            }
        }
    }

    //Script to open terminal
    void OnTriggerStay (Collider other)
    {
        if(Input.GetKey(KeyCode.E) && other.gameObject.tag == "Player")
        {
            Cursor.lockState = CursorLockMode.Confined;
            screenOpen = true;
            terminalScreen.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
        //CONTROLLER Need editing here, since it's a menu
        if (Input.GetButtonDown("Interact") && other.gameObject.tag == "Player") 
        {
            Cursor.lockState = CursorLockMode.Confined;
            screenOpen = true;
            terminalScreen.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
    }

    //Script for closing terminal and resuming game
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;

        terminalScreen.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void OpenEmail1()
    {
        if (emailsDeleted == false)
        {
            email1.SetActive(true);
            email2.SetActive(false);
            email3.SetActive(false);
            email4.SetActive(false);
        }
        else if (emailsDeleted == true)
        {
            email1.SetActive(false);
            email2.SetActive(false);
            email3.SetActive(false);
            email4.SetActive(false);
            deletedMessage.enabled = true;
        }
    }

    public void OpenEmail2()
    {
        if (emailsDeleted == false)
        {
            email1.SetActive(false);
            email2.SetActive(true);
            email3.SetActive(false);
            email4.SetActive(false);
        }
        else if (emailsDeleted == true)
        {
            email1.SetActive(false);
            email2.SetActive(false);
            email3.SetActive(false);
            email4.SetActive(false);
            deletedMessage.enabled = true;

        }
    }

    public void OpenEmail3()
    {
        if (emailsDeleted == false)
        {
            email1.SetActive(false);
            email2.SetActive(false);
            email3.SetActive(true);
            email4.SetActive(false);
        }
        else if (emailsDeleted == true)
        {
            email1.SetActive(false);
            email2.SetActive(false);
            email3.SetActive(false);
            email4.SetActive(false);
            deletedMessage.enabled = true;
        }
    }
    
    public void OpenEmail4()
    {
        if (emailsDeleted == false)
        {
            email1.SetActive(false);
            email2.SetActive(false);
            email3.SetActive(false);
            email4.SetActive(true);
        }
        else if (emailsDeleted == true)
        {
            email1.SetActive(false);
            email2.SetActive(false);
            email3.SetActive(false);
            email4.SetActive(false);
            deletedMessage.enabled = true;
        }
    }

    public void DeleteEmails()
    {
        if (emailsDeleted == false && emailsSaved == false)
        {
            player.GetComponent<Player>().DeleteInformation(informationValue);
        }
        emailsDeleted = true;
        screen.GetComponent<MeshRenderer>().material = deleteMat;
        Resume();
    }

    public void SaveEmails()
    {
        if (emailsDeleted == false && emailsSaved == false)
        {
            player.GetComponent<Player>().SaveInformation(informationValue);
        }
        emailsSaved = true;

        Resume();
    }

    public void PlayAudio()
    {
        voiceLog.Play();
    }

    public void PlayAudio2()
    {
        voiceLog2.Play();
    }

    public void Close()
    {
        Resume();
    }
}
