using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public Component doorcolliderhere;
    public Component doorcolliderhere2;
    public Component doorcolliderhere3;
    public GameObject keygone;

    // Update is called once per frame
    void OnTriggerStay ()
    {
        if(Input.GetKey(KeyCode.E))
        {
            doorcolliderhere.GetComponent<BoxCollider> ().enabled = true;
            doorcolliderhere2.GetComponent<BoxCollider> ().enabled = true;
            doorcolliderhere3.GetComponent<BoxCollider> ().enabled = true;
        }

        if(Input.GetKey(KeyCode.E))
        keygone.SetActive(false);
    }
}