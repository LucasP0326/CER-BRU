using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinReception : MonoBehaviour
{
    public bool receptionWon = false;

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("Office");
        receptionWon = true;
    }
}
