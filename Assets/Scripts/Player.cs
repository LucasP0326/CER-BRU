using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool hasGun;
    public int health;
    
    //pickups
    bool standingOnPickup;
    GameObject pickupUnderPlayer;
    public bool invisible;
    public int invisibilityLength;

    public GameObject gunPosition;
    public Material transparentMat;
    public Material opaqueMat;
    public GameObject Mesh;

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && standingOnPickup)
        {
            standingOnPickup = false;
            Destroy(pickupUnderPlayer);
            playerInvisible();
            invisible = true;
            StartCoroutine(InvisibilityCountdown());
        }
    }
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

        if (collision.gameObject.tag == "Invisibility")
        {
            standingOnPickup = true;
            pickupUnderPlayer = collision.gameObject;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Invisibility")
        {
            standingOnPickup = false;
            pickupUnderPlayer = null;
        }
    }

    private IEnumerator InvisibilityCountdown()
    {
        yield return new WaitForSeconds(invisibilityLength - 3);
        
        playerInvisible();
        yield return new WaitForSeconds(0.5f);
        playerVisible();
        yield return new WaitForSeconds(0.5f);
        playerInvisible();
        yield return new WaitForSeconds(0.5f);
        playerVisible();

        yield return new WaitForSeconds(0.25f);
        playerInvisible();
        yield return new WaitForSeconds(0.25f);
        playerVisible();
        yield return new WaitForSeconds(0.25f);
        playerInvisible();
        yield return new WaitForSeconds(0.25f);
        playerVisible();
        yield return new WaitForSeconds(0.25f);
        playerInvisible();
        yield return new WaitForSeconds(0.25f);
        playerVisible();
        
        invisible = false;
    }

    private void playerInvisible()
    {
        Mesh.GetComponent<SkinnedMeshRenderer>().materials[0].CopyPropertiesFromMaterial(transparentMat);
        Mesh.GetComponent<SkinnedMeshRenderer>().materials[1].CopyPropertiesFromMaterial(transparentMat);
        Mesh.GetComponent<SkinnedMeshRenderer>().materials[2].CopyPropertiesFromMaterial(transparentMat);
    }
    private void playerVisible()
    {
        Mesh.GetComponent<SkinnedMeshRenderer>().materials[0].CopyPropertiesFromMaterial(opaqueMat);
        Mesh.GetComponent<SkinnedMeshRenderer>().materials[1].CopyPropertiesFromMaterial(opaqueMat);
        Mesh.GetComponent<SkinnedMeshRenderer>().materials[2].CopyPropertiesFromMaterial(opaqueMat);
    }

    public void Death()
    {
        Debug.Log("You died!");
        SceneManager.LoadScene("Lose");
    }
}
