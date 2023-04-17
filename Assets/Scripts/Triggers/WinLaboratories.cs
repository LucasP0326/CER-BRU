using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLaboratories : MonoBehaviour
{

    public static bool laboratoriesWon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            laboratoriesWon = true;
            SceneManager.LoadScene("Office");
        }
    }
}
