using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSecurity : MonoBehaviour
{

    public GameObject transmission;
    public GameObject globalVariables;
    public bool warningPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && globalVariables.GetComponent<GlobalVariables>().adminWon == false)
        {
            transmission.GetComponent<OfficeEventManager>().SecurityDialogue();
        }
        if (other.gameObject.CompareTag("Player") && globalVariables.GetComponent<GlobalVariables>().labsWon == true && warningPlayed == false)
        {
            transmission.GetComponent<OfficeEventManager>().startEncounter();
            warningPlayed = true;
        }
    }
}
