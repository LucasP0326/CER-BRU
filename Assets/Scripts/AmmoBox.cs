using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public int ammoValue;
    public GameObject player;
    public AudioSource pickup;
    // Start is called before the first frame update

    void OnTriggerStay(Collider other)
    {
        if ((Input.GetKey (KeyCode.E) || Input.GetButtonDown("Interact")) && other.gameObject.CompareTag("Player"))
        {
            player.GetComponent<Player>().RaiseAmmo(ammoValue);
            pickup.Play();
            Destroy(gameObject);
        }
    }
}
