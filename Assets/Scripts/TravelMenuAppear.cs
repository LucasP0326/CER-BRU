using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelMenuAppear : MonoBehaviour
{
    public GameObject menu;

    public static bool GameIsPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
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
    private void OnTriggerEnter(Collider other)
    {
        Pause();
        menu.SetActive(true);
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.Confined;

        menu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;

        menu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
}
