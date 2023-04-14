using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInvisibilityDialogue : MonoBehaviour
{

    public GameObject transmission;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transmission.GetComponent<AdministrationEventManager>().InvisibilityDialogue();
        }
    }
}
