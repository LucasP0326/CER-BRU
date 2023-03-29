using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Terminal : MonoBehaviour
{

    public GameObject terminalScreen;

    public static bool GameIsPaused = false;
    public TextMeshProUGUI email1;
    public TextMeshProUGUI email2;
    public TextMeshProUGUI email3;
    public TextMeshProUGUI email4;
    public TextMeshProUGUI deletedMessage;
    public bool emailsDeleted = false;
    public int informationValue = 0;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        terminalScreen.SetActive(false);
        email1.enabled = false;
        email2.enabled = false;
        email3.enabled = false;
        email4.enabled = false;
        deletedMessage.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        }
        else if (emailsDeleted == true)
        {
            email1.enabled = false;
            email2.enabled = false;
            email3.enabled = false;
            email4.enabled = false;
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
        }
        else if (emailsDeleted == true)
        {
            email1.enabled = false;
            email2.enabled = false;
            email3.enabled = false;
            email4.enabled = false;
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
        }
        else if (emailsDeleted == true)
        {
            email1.enabled = false;
            email2.enabled = false;
            email3.enabled = false;
            email4.enabled = false;
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
        }
        else if (emailsDeleted == true)
        {
            email1.enabled = false;
            email2.enabled = false;
            email3.enabled = false;
            email4.enabled = false;
            deletedMessage.enabled = true;
        }
    }

    public void DeleteEmails()
    {
        if (emailsDeleted == false)
        {
            player.GetComponent<Player>().DeleteInformation(informationValue);
        }
        emailsDeleted = true;
        Resume();
    }
}
