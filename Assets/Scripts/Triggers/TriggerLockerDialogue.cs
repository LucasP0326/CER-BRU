using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLockerDialogue : MonoBehaviour
{

    public GameObject transmission;
    public GameObject globalVariables;
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
            transmission.GetComponent<OfficeEventManager>().LockerDialogue();
        }
        if (other.gameObject.CompareTag("Player") && globalVariables.GetComponent<GlobalVariables>().collectedInvisibility == true && globalVariables.GetComponent<GlobalVariables>().collectedXray == true)
        {
            transmission.GetComponent<OfficeEventManager>().RogueEncounter();
        }  
    }
}
