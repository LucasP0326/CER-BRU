using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthValue;
    public GameObject player;

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKey (KeyCode.E) || Input.GetButtonDown("Interact") && other.gameObject.CompareTag("Player"))
        {
            player.GetComponent<Player>().RaiseHealth(healthValue);
            Destroy(gameObject);
        }
    }
}
