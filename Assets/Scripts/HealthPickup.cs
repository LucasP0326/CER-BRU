using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPickup : MonoBehaviour
{
    public int healthValue;
    public GameObject player;
    public Image healthBar;
    public AudioSource healthPickup;
    public float timeDelay;
    public bool healthCollected;

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKey (KeyCode.E) || Input.GetButtonDown("Interact") && other.gameObject.CompareTag("Player"))
        {
            player.GetComponent<Player>().RaiseHealth(healthValue);
            healthPickup.Play();
            StartCoroutine(Wait());
            healthCollected = true;
            
        }
    }

    IEnumerator Wait()
    {
        if(healthCollected == true)
        {
            healthBar.GetComponent<Image>().color = new Color32(0,255,0,200);
            timeDelay = 0.25f;
            yield return new WaitForSeconds(timeDelay);
            healthBar.GetComponent<Image>().color = new Color32(255,0,0,200);
            Destroy(gameObject);
        }
    }
}
