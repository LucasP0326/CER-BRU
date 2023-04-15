using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SecurityOfficeTerminal : MonoBehaviour
{

    public GameObject terminalScreen;
    public GameObject globalVariables;

    public static bool GameIsPaused = false;
    public TextMeshProUGUI email1;
    public TextMeshProUGUI email2;
    public TextMeshProUGUI email3;
    public TextMeshProUGUI email4;
    public TextMeshProUGUI deletedMessage;
    public bool emailsDeleted = false;
    public bool emailsSaved = false;
    public int levelSelected = 0;
    public int informationValue = 0;
    public GameObject player;
    public bool screenOpen = false;
    public GameObject levelButtons;

    public GameObject receptionCleared;
    public GameObject receptionInaccessible;
    public GameObject adminCleared;
    public GameObject adminInaccessible;
    public GameObject labsCleared;
    public GameObject labsInaccessible;

    // Start is called before the first frame update
    void Start()
    {
        terminalScreen.SetActive(false);
        email1.enabled = false;
        email2.enabled = false;
        email3.enabled = false;
        email4.enabled = false;
        levelButtons.SetActive(false);
        deletedMessage.enabled = false;

        receptionCleared.SetActive(false);
        receptionInaccessible.SetActive(false);
        adminCleared.SetActive(false);
        adminInaccessible.SetActive(false);
        labsCleared.SetActive(false);
        labsInaccessible.SetActive(false);
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
        if (globalVariables.GetComponent<GlobalVariables>().receptionWon == false && globalVariables.GetComponent<GlobalVariables>().adminWon == false && globalVariables.GetComponent<GlobalVariables>().labsWon == false)
        {
            receptionCleared.SetActive(false);
            receptionInaccessible.SetActive(false);
            adminCleared.SetActive(false);
            adminInaccessible.SetActive(true);
            labsCleared.SetActive(false);
            labsInaccessible.SetActive(true);
        }
        if (globalVariables.GetComponent<GlobalVariables>().receptionWon == true && globalVariables.GetComponent<GlobalVariables>().adminWon == false && globalVariables.GetComponent<GlobalVariables>().labsWon == false)
        {
            receptionCleared.SetActive(true);
            receptionInaccessible.SetActive(false);
            adminCleared.SetActive(false);
            adminInaccessible.SetActive(false);
            labsCleared.SetActive(false);
            labsInaccessible.SetActive(true);
        }
        if (globalVariables.GetComponent<GlobalVariables>().receptionWon == true && globalVariables.GetComponent<GlobalVariables>().adminWon == true && globalVariables.GetComponent<GlobalVariables>().labsWon == false)
        {
            receptionCleared.SetActive(true);
            receptionInaccessible.SetActive(false);
            adminCleared.SetActive(true);
            adminInaccessible.SetActive(false);
            labsCleared.SetActive(false);
            labsInaccessible.SetActive(false);
        }
        if (globalVariables.GetComponent<GlobalVariables>().receptionWon == true && globalVariables.GetComponent<GlobalVariables>().adminWon == true && globalVariables.GetComponent<GlobalVariables>().labsWon == false)
        {
            receptionCleared.SetActive(true);
            receptionInaccessible.SetActive(false);
            adminCleared.SetActive(true);
            adminInaccessible.SetActive(false);
            labsCleared.SetActive(true);
            labsInaccessible.SetActive(false);
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
            levelButtons.SetActive(true);
        }
        else if (emailsDeleted == true)
        {
            email1.enabled = false;
            email2.enabled = false;
            email3.enabled = false;
            email4.enabled = false;
            deletedMessage.enabled = true;
            levelButtons.SetActive(false);
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
            levelButtons.SetActive(false);
        }
        else if (emailsDeleted == true)
        {
            email1.enabled = false;
            email2.enabled = false;
            email3.enabled = false;
            email4.enabled = false;
            deletedMessage.enabled = true;
            levelButtons.SetActive(false);
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
            levelButtons.SetActive(false);
        }
        else if (emailsDeleted == true)
        {
            email1.enabled = false;
            email2.enabled = false;
            email3.enabled = false;
            email4.enabled = false;
            deletedMessage.enabled = true;
            levelButtons.SetActive(false);
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
            levelButtons.SetActive(false);
        }
        else if (emailsDeleted == true)
        {
            email1.enabled = false;
            email2.enabled = false;
            email3.enabled = false;
            email4.enabled = false;
            deletedMessage.enabled = true;
            levelButtons.SetActive(false);
        }
    }

    public void SetReceptionLevel()
    {
        if (globalVariables.GetComponent<GlobalVariables>().receptionWon == false)
            levelSelected = 1;
    }
    public void SetAdministrationLevel()
    {
        if (globalVariables.GetComponent<GlobalVariables>().receptionWon == true && globalVariables.GetComponent<GlobalVariables>().adminWon == false)
            levelSelected = 2;
    }
    public void SetLaboratoryLevel()
    {
        if (globalVariables.GetComponent<GlobalVariables>().receptionWon == true && globalVariables.GetComponent<GlobalVariables>().adminWon == true && globalVariables.GetComponent<GlobalVariables>().labsWon == false)
            levelSelected = 3;
    }

    public void Close()
    {
        Resume();
    }
}
