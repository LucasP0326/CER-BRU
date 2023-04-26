using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject key;
    public GameObject keyPad;
    public Material staticMat;
    public Material successMat;
    public Material failMat;
    public bool locked = false;
    public bool doorOpen = false;
    public bool doorMoving = false;
    public GameObject closedPosition;
    public GameObject openPosition;
    public float speed = 0.25f;
    public AudioSource door;
    public float timeDelay;
    public bool usedLockedDoor = false;
    public bool openedLockedDoor = false;
    
    // Start is called before the first frame update
    void Start()
    {
        keyPad.GetComponent<MeshRenderer>().material = staticMat;
    }

    // Update is called once per frame
    void Update()
    {
        if (doorOpen == true && transform.position != openPosition.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, openPosition.transform.position, speed * Time.deltaTime);
            doorMoving = true;
        }
        if (doorOpen == false && transform.position != closedPosition.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, closedPosition.transform.position, speed * Time.deltaTime);
            doorMoving = true;
        }
        if (doorOpen == true && transform.position == openPosition.transform.position)
            doorMoving = false;
        if (doorOpen == false && transform.position == closedPosition.transform.position)
            doorMoving = false;

        if (doorOpen == true)
            keyPad.GetComponent<MeshRenderer>().material = successMat;
        if (doorOpen == false && locked == false)
            keyPad.GetComponent<MeshRenderer>().material = staticMat;
    }

    void OnTriggerStay(Collider other)
    {
        Key keyCard = key.gameObject.GetComponent<Key>();

        if (Input.GetKey (KeyCode.E) && other.gameObject.CompareTag("Player") || Input.GetButtonDown("Interact") && other.gameObject.CompareTag("Player"))
        {
            if (doorOpen == false && doorMoving == false)
            {
                if (locked == false)
                {
                    doorOpen = true;
                    door.Play();
                }
                if (locked == true && keyCard.hasKey == false)
                {
                    keyPad.GetComponent<MeshRenderer>().material = failMat;
                }
                if (locked == true && keyCard.hasKey == true)
                {
                    locked = false;
                    keyPad.GetComponent<MeshRenderer>().material = successMat;
                }
            }
            else if (doorOpen == true && doorMoving == false)
            {
                doorOpen = false;
                door.Play();
            }
        }
    }
}
