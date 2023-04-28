using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public RawImage centerPoint;
    public RawImage crosshair;
    public float timeDelay;

    // Start is called before the first frame update
    void Start()
    {
        centerPoint.GetComponent<RawImage>().color = new Color32(0,235,225,255);
        crosshair.GetComponent<RawImage>().color = new Color32(0,235,225,255);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireGun()
    {
        crosshair.transform.localScale += new Vector3(1f,1f,1f);
        StartCoroutine(DriftBack());
    }

    public void HitEnemy()
    {
        centerPoint.GetComponent<RawImage>().color = new Color32(255,0,0,255);
        crosshair.GetComponent<RawImage>().color = new Color32(255,0,0,255);
        StartCoroutine(Wait());
    }

    IEnumerator DriftBack()
    {
        for(float i = 1.75f; i >= 0.75f; i-= 0.05f)
        {
            crosshair.transform.localScale -= new Vector3(.05f,.05f,.05f);
            yield return null;
        }
        crosshair.transform.localScale += new Vector3(.05f,.05f,.05f);
    }

    IEnumerator Wait()
    {
        timeDelay = 0.25f;
        yield return new WaitForSeconds(timeDelay);
        centerPoint.GetComponent<RawImage>().color = new Color32(0,235,225,255);
        crosshair.GetComponent<RawImage>().color = new Color32(0,235,225,255);
    }
}
