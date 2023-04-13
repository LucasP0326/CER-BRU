using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdministrationEventManager : MonoBehaviour
{

    public TextMeshProUGUI dialogue;
    public TextMeshProUGUI caller;
    public GameObject transmission;
    public float timeDelay;
    public float timeDelay2;
    public float timeDelay3;

    // Start is called before the first frame update
    void Start()
    {
        transmission.SetActive(false);
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Wait()
    {
        timeDelay = 5f;
        yield return new WaitForSeconds(timeDelay);
        transmission.SetActive(true);
        caller.text = "CERAEBRU SECURITY DISPATCH";
    }
}
