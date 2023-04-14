using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinReception : MonoBehaviour
{
    public static bool receptionWon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            receptionWon = true;
            SceneManager.LoadScene("Office");
        }
    }
}
