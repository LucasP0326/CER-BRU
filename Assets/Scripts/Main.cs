using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject globalVariables;

    public void StartGame() 
    {
        globalVariables.GetComponent<GlobalVariables>().informationDeleted = 0;
        globalVariables.GetComponent<GlobalVariables>().informationDeleted = 0;
        WinReception.receptionWon = false;
        WinAdministration.adminWon = false;
        WinLaboratories.laboratoriesWon = false;
        InvisibilityTerminal.collectedInvisibility = false;
        globalVariables.GetComponent<GlobalVariables>().collectedInvisibility = false;
        Player.informationDeleted = 0;
        Player.informationSaved = 0;
        SceneManager.LoadScene("Cutscene Room");
    }

    public void QuitGame() 
    {
        Debug.Log("Button pressed");
        Application.Quit();
    }

    public void LoadOffice()
    {
        SceneManager.LoadScene("Office");
    }

    public void LoadLaboratory()
    {
        SceneManager.LoadScene("Laboratory");
    }

    public void LoadAdministration()
    {
        SceneManager.LoadScene("Administration");
    }

    public void LoadReception()
    {
        SceneManager.LoadScene("Reception");
    }
}
