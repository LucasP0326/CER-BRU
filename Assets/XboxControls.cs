using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xbox : MonoBehaviour
{
    public GameObject Gun;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Instantiate(Gun, transform.position, transform.rotation);
    }
}
