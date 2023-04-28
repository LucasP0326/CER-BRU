using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSignalLoss : MonoBehaviour
{
    public GameObject transmission;
    public bool audioPlayed;

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
        if (other.gameObject.CompareTag("Player") && audioPlayed == false)
        {
            transmission.GetComponent<LaboratoriesEventManager>().SignalDialogue();
            audioPlayed = true;
        }
    }
}
