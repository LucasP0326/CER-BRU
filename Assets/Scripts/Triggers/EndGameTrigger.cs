using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameTrigger : MonoBehaviour
{
    public GameObject globalVariables;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && globalVariables.GetComponent<GlobalVariables>().labsWon == true)
        {
            SceneManager.LoadScene("Cutscene Room");
        }
    }
}
