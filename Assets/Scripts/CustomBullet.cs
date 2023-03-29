using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomBullet : MonoBehaviour
{
    public Rigidbody rb;

    void Start(){
        StartCoroutine(TimeOut());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy"){
            //Debug.Log("Hit Enemy!");
            collision.collider.GetComponent<Enemy>().TakeDamage(1);
            Destroy(gameObject);
        }

        if (collision.collider.tag == "Floor")
        {
            //Debug.Log("Hit Floor!");
            Destroy(gameObject);
        }

        if (collision.collider.tag == "Ceiling")
        {
            //Debug.Log("Hit Ceiling!");
            Destroy(gameObject);
        }

        if (collision.collider.tag == "Wall")
        {
            //Debug.Log("Hit Wall!");
            Destroy(gameObject);
        }

        if (collision.collider.tag == "Glass")
        {
            collision.collider.GetComponent<Glass>().ChangeMesh();
            Destroy(gameObject);
        }
    }

    private IEnumerator TimeOut(){
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
