using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool hasGun;
    public GameObject gunPosition;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Gun" && !hasGun)
        {
            collision.transform.parent = gunPosition.transform;
            collision.transform.localPosition = new Vector3(0f, 0f, 0f);
            //collision.transform.localPosition = new Vector3(0.7f, -0.33f, 1.03f);
            collision.transform.localRotation = Quaternion.Euler(0, 0, 0);

            collision.gameObject.GetComponent<ProjectileGun>().equipped = true;
            hasGun = true;
        }
    }
}
