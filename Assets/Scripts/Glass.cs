using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{

    public Material staticMat;
    public Material brokenMat;
    public AudioSource glassBreak;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material = staticMat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMesh()
    {
        gameObject.GetComponent<MeshRenderer>().material = brokenMat;
        glassBreak.Play();
    }
}
