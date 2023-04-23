using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XRayTerminal : MonoBehaviour
{

    public GameObject terminalScreen;

    public static bool GameIsPaused = false;
    public TextMeshProUGUI email1;
    public TextMeshProUGUI email2;
    public TextMeshProUGUI email3;
    public TextMeshProUGUI email4;
    public TextMeshProUGUI deletedMessage;
    public bool emailsDeleted = false;
    public bool emailsSaved = false;
    public int informationValue = 0;
    public GameObject player;
    public bool screenOpen = false;
    public GameObject downloadButton;
    public GameObject xrayIcon;
    public static bool collectedXRay = false;

    // Start is called before the first frame update
    void Start()
    {
        terminalScreen.SetActive(false);
        email1.enabled = false;
        email2.enabled = false;
        email3.enabled = false;
        email4.enabled = false;
        downloadButton.SetActive(false);
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
            email1.enabled = true;
            email2.enabled = false;
            email3.enabled = false;
            email4.enabled = false;
            downloadButton.SetActive(false);
        }
        else if (emailsDeleted == true)
        {
            email1.enabled = false;
            email2.enabled = false;
            email3.enabled = false;
            email4.enabled = false;
            downloadButton.SetActive(false);
            deletedMessage.enabled = true;
        }
    }

    public void OpenEmail2()
    {
        if (emailsDeleted == false)
        {
            email1.enabled = false;
            email2.enabled = true;
            email3.enabled = false;
            email4.enabled = false;
            downloadButton.SetActive(false);
        }
        else if (emailsDeleted == true)
        {
            email1.enabled = false;
            email2.enabled = false;
            email3.enabled = false;
            email4.enabled = false;
            downloadButton.SetActive(false);
            deletedMessage.enabled = true;
        }
    }

    public void OpenEmail3()
    {
        if (emailsDeleted == false)
        {
            email1.enabled = false;
            email2.enabled = false;
            email3.enabled = true;
            email4.enabled = false;
            downloadButton.SetActive(true);

        }
        else if (emailsDeleted == true)
        {
            email1.enabled = false;
            email2.enabled = false;
            email3.enabled = false;
            email4.enabled = false;
            downloadButton.SetActive(false);
            deletedMessage.enabled = true;
        }
    }
    
    public void OpenEmail4()
    {
        if (emailsDeleted == false)
        {
            email1.enabled = false;
            email2.enabled = false;
            email3.enabled = false;
            email4.enabled = true;
            downloadButton.SetActive(false);
        }
        else if (emailsDeleted == true)
        {
            email1.enabled = false;
            email2.enabled = false;
            email3.enabled = false;
            email4.enabled = false;
            downloadButton.SetActive(false);
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

    public void DownloadXRay()
    {
        player.GetComponent<Player>().xrayUsable = true;
        player.GetComponent<Player>().xrayIcon.SetActive(true);
        xrayIcon.GetComponent<XrayIcon>().Ready();
        collectedXRay = true;
    }

    public void Close()
    {
        Resume();
    }
}
