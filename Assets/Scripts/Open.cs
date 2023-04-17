using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Open : MonoBehaviour
{
    public GameObject key;
    public GameObject keyPad;
    public Material staticMat;
    public Material successMat;
    public Material failMat;
    public Animation hinge;
    public AudioSource elevator;
    public bool locked = false;
    public TextMeshProUGUI textObject;
    public float timeDelay;
    public bool usedLockedDoor = false;
    public bool openedLockedDoor = false;

    void Start()
    {
        keyPad.GetComponent<MeshRenderer>().material = staticMat;
    }

    void OnTriggerStay(Collider other)
    {
        Key keyCard = key.gameObject.GetComponent<Key>();
        
        //If press E
        if (Input.GetKey (KeyCode.E) && other.gameObject.CompareTag("Player"))
        {
            //if door unlocked
            if (locked == false)
            {
                keyPad.GetComponent<MeshRenderer>().material = successMat;
                hinge.Play ();
                elevator.Play ();
            }

            //if door locked and no key
            if (locked == true && keyCard.hasKey == false)
            {
                keyPad.GetComponent<MeshRenderer>().material = failMat;
                StartCoroutine(Wait());
            }

            //if door locked and key
            if (locked == true && keyCard.hasKey == true)
            {
                locked = false;
                openedLockedDoor = true;
                keyPad.GetComponent<MeshRenderer>().material = successMat;
                hinge.Play ();
                elevator.Play ();
            }
        }
        if (Input.GetButtonDown("Interact")) //CONTROLLER
        {
             //if door unlocked
            if (locked == false)
            {
                keyPad.GetComponent<MeshRenderer>().material = successMat;
                hinge.Play ();
                elevator.Play ();
            }

            //if door locked and no key
            if (locked == true && keyCard.hasKey == false)
            {
                keyPad.GetComponent<MeshRenderer>().material = failMat;
                StartCoroutine(Wait());
            }

            //if door locked and key
            if (locked == true && keyCard.hasKey == true)
            {
                locked = false;
                keyPad.GetComponent<MeshRenderer>().material = successMat;
                hinge.Play ();
                elevator.Play ();
            }
        }
    }

    IEnumerator Wait()
    {
        textObject.enabled = true;
        usedLockedDoor = true;
        textObject.text = "THIS DOOR IS LOCKED";
        timeDelay = 3f;
        yield return new WaitForSeconds(timeDelay);
        textObject.enabled = false;
    }

}