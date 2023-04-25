using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryGame : MonoBehaviour
{
    public GameObject globalVariables;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        if (globalVariables.GetComponent<GlobalVariables>().receptionWon == true)
            SceneManager.LoadScene("Office");
        if (globalVariables.GetComponent<GlobalVariables>().receptionWon == false)
            SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
    
        Application.Quit();
    }
}
