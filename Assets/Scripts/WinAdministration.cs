using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinAdministration : MonoBehaviour
{
    public bool adminWon = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            adminWon = true;
            SceneManager.LoadScene("Office");
        }
    }
}
