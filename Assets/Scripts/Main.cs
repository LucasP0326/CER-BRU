using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame() 
    {
        SceneManager.LoadScene(1);
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
