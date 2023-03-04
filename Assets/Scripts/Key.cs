using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public Component doorcolliderhere;
    public GameObject keygone;

    // Update is called once per frame
    void OnTriggerStay ()
    {
        if(Input.GetKey(KeyCode.E))
        doorcolliderhere.GetComponent<BoxCollider> ().enabled = true;

        if(Input.GetKey(KeyCode.E))
        keygone.SetActive(false);
    }
}