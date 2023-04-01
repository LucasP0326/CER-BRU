using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool hasGun;
    private GameObject gun;
    public int health;

    //ADS
    public GameObject followCamera;
    private float defaultZoom;
    public float ADSZoom;
    
    //pickups
    bool standingOnPickup;
    GameObject pickupUnderPlayer;
    private bool invisibilityUsable;
    public bool invisible;
    public int invisibilityLength;
    public int invisibilityCooldown;
    public GameObject invisiblityIcon;

    public GameObject restingGunPosition;
    public GameObject aimingGunPosition;
    public Material transparentMat;
    public Material opaqueMat;
    public GameObject Mesh;
    public int informationDeleted = 0;

    private void Start()
    {
        followCamera = GameObject.FindWithTag("Camera");
        defaultZoom = followCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView;

        if (hasGun)
        {
            gun = GameObject.FindWithTag("Gun");
        }
    }

    private void Update()
    {
        //ADS
        if (hasGun && Input.GetMouseButtonDown(1))
        {
            followCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = ADSZoom;
            gameObject.GetComponent<ThirdPersonController>().aiming = true;
            gun.GetComponent<ProjectileGun>().aiming = true;

            gun.transform.parent = aimingGunPosition.transform;
            gun.transform.localPosition = new Vector3(0f, 0f, 0f);
            gun.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (hasGun && Input.GetMouseButtonUp(1))
        {
            followCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = defaultZoom;
            gameObject.GetComponent<ThirdPersonController>().aiming = false;
            gun.GetComponent<ProjectileGun>().aiming = false;

            gun.transform.parent = restingGunPosition.transform;
            gun.transform.localPosition = new Vector3(0f, 0f, 0f);
            gun.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        //Controller require attention here, aim stuck and won't toggle like PC does
        if (hasGun && Input.GetButtonDown("Aim")) 
        {
            followCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = ADSZoom;
            gameObject.GetComponent<ThirdPersonController>().aiming = true;
            gun.GetComponent<ProjectileGun>().aiming = true;

            gun.transform.parent = aimingGunPosition.transform;
            gun.transform.localPosition = new Vector3(0f, 0f, 0f);
            gun.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (hasGun && Input.GetButtonUp("Aim"))
        {
            followCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = defaultZoom;
            gameObject.GetComponent<ThirdPersonController>().aiming = false;
            gun.GetComponent<ProjectileGun>().aiming = false;

            gun.transform.parent = restingGunPosition.transform;
            gun.transform.localPosition = new Vector3(0f, 0f, 0f);
            gun.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.E) && standingOnPickup)
        {
            standingOnPickup = false;
            Destroy(pickupUnderPlayer);
            invisibilityUsable = true;
            invisiblityIcon.SetActive(true);
            invisiblityIcon.GetComponent<InvisibilityIcon>().Ready();
        }
        if (Input.GetButtonDown("Interact") && standingOnPickup) //CONTROLLER
         {
            standingOnPickup = false;
            Destroy(pickupUnderPlayer);
            invisibilityUsable = true;
            invisiblityIcon.SetActive(true);
            invisiblityIcon.GetComponent<InvisibilityIcon>().Ready();
        }

        if (Input.GetKey(KeyCode.F) && invisibilityUsable)
        {
            invisibilityUsable = false;
            playerInvisible();
            invisible = true;
            StartCoroutine(InvisibilityCountdown());
            invisiblityIcon.GetComponent<InvisibilityIcon>().CoolDown();
        }
        if (Input.GetButtonDown("Use") && invisibilityUsable) //CONTROLLER
        {
            invisibilityUsable = false;
            playerInvisible();
            invisible = true;
            StartCoroutine(InvisibilityCountdown());
            invisiblityIcon.GetComponent<InvisibilityIcon>().CoolDown();
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Gun" && !hasGun)
        {
            gun = collision.gameObject;

            gun.transform.parent = restingGunPosition.transform;
            gun.transform.localPosition = new Vector3(0f, 0f, 0f);
            gun.transform.localRotation = Quaternion.Euler(0, 0, 0);

            gun.GetComponent<ProjectileGun>().equipped = true;
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

        yield return new WaitForSeconds(invisibilityCooldown);
        invisiblityIcon.GetComponent<InvisibilityIcon>().Ready();
        invisibilityUsable = true;
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

    public void DeleteInformation(int informationValue)
    {
        informationDeleted += informationValue;
    }
}
