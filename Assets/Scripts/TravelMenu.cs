using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TravelMenu : MonoBehaviour
{

    public void LoadOffice()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Office");
    }

    public void LoadLaboratory()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Laboratory");
    }

    public void LoadAdministration()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Administration");
    }

    public void LoadReception()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Reception");
    }
}
