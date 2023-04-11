using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerControl : MonoBehaviour
{
    public bool isFlickering = false;
    public float timeDelay;
    public AudioSource buzz;

    // Update is called once per frame
    void Update()
    {
        if (isFlickering == false)
        {
            StartCoroutine(FlickeringLight());
        }
    }

    IEnumerator FlickeringLight()
    {
        isFlickering = true;
        buzz.enabled = true;
        this.gameObject.GetComponent<Light>().enabled = false;
        buzz.enabled = false;
        timeDelay = Random.Range(0.05f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        buzz.enabled = true;
        timeDelay = Random.Range(0.05f, 0.4f);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
        buzz.enabled = false;
    }
}
