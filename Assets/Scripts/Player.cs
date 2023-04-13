using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //ADS
    public GameObject followCamera;
    private float defaultZoom;
    public float ADSZoom;
    
    //pickups
    bool standingOnPickup;
    public GameObject pickupUnderPlayer;
    
    //invisibility
    private bool invisibilityUsable;
    public bool invisible;
    public int invisibilityLength;
    public int invisibilityCooldown;
    public GameObject invisiblityIcon;

    //xray
    private bool xrayUsable;
    public bool xray;
    public int xrayLength;
    public int xrayCooldown;
    public GameObject xrayIcon;
    public Material transparentMat;
    public Material opaqueMat;
    public GameObject Mesh;

    private GameObject[] enemies;

    //gun
    public GameObject restingGunPosition;
    public GameObject aimingGunPosition;
    public bool hasGun;
    private GameObject gun;

    //score

    public int informationDeleted = 0;
    public int informationSaved = 0;
    public int killCount = 0;
    public int health;

    private void Start()
    {
        followCamera = GameObject.FindWithTag("Camera");
        defaultZoom = followCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView;

        if (hasGun)
        {
            gun = GameObject.FindWithTag("Gun");
        }

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
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

        //collect pickup
        if ((Input.GetKey(KeyCode.E) || Input.GetButtonDown("Interact")) && standingOnPickup)
        {
            //invisibility
            if (pickupUnderPlayer.tag == "Invisibility"){
                invisibilityUsable = true;
                invisiblityIcon.SetActive(true);
                invisiblityIcon.GetComponent<InvisibilityIcon>().Ready();
            }

            //xray
            if (pickupUnderPlayer.tag == "Xray"){
                xrayUsable = true;
                xrayIcon.SetActive(true);
                xrayIcon.GetComponent<XrayIcon>().Ready();
            }

            standingOnPickup = false;
            Destroy(pickupUnderPlayer);
        }

        //use invisibliity
        if ((Input.GetKey(KeyCode.F) || Input.GetButtonDown("Use")) && invisibilityUsable)
        {
            invisibilityUsable = false;
            playerInvisible();
            invisible = true;
            StartCoroutine(InvisibilityCountdown());
            invisiblityIcon.GetComponent<InvisibilityIcon>().CoolDown();
        }

        //use xray (needs controller keybind)
        if (Input.GetKey(KeyCode.C) && xrayUsable)
        {
            xrayUsable = false;
            xrayOn();
            xray = true;
            StartCoroutine(XrayCountdown());
            xrayIcon.GetComponent<XrayIcon>().CoolDown();
        }
        if (Input.GetButtonDown("Use") && xrayUsable)
         {
            xrayUsable = false;
            xrayOn();
            xray = true;
            StartCoroutine(XrayCountdown());
            xrayIcon.GetComponent<XrayIcon>().CoolDown();
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

        if (collision.gameObject.tag == "Invisibility" || collision.gameObject.tag == "Xray")
        {
            standingOnPickup = true;
            pickupUnderPlayer = collision.gameObject;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Invisibility" || collision.gameObject.tag == "Xray")
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

        private IEnumerator XrayCountdown()
    {
        yield return new WaitForSeconds(xrayLength - 3);
        
        xrayOn();
        yield return new WaitForSeconds(0.5f);
        xrayOff();
        yield return new WaitForSeconds(0.5f);
        xrayOn();
        yield return new WaitForSeconds(0.5f);
        xrayOff();

        yield return new WaitForSeconds(0.25f);
        xrayOn();
        yield return new WaitForSeconds(0.25f);
        xrayOff();
        yield return new WaitForSeconds(0.25f);
        xrayOn();
        yield return new WaitForSeconds(0.25f);
        xrayOff();
        yield return new WaitForSeconds(0.25f);
        xrayOn();
        yield return new WaitForSeconds(0.25f);
        xrayOff();
        
        xray = false;

        yield return new WaitForSeconds(xrayCooldown);
        xrayIcon.GetComponent<XrayIcon>().Ready();
        xrayUsable = true;
    }


    private void playerInvisible()
    {
        Mesh.GetComponent<SkinnedMeshRenderer>().materials[0].CopyPropertiesFromMaterial(transparentMat);
        Mesh.GetComponent<SkinnedMeshRenderer>().materials[1].CopyPropertiesFromMaterial(transparentMat);
        //Mesh.GetComponent<SkinnedMeshRenderer>().materials[2].CopyPropertiesFromMaterial(transparentMat);
    }
    private void playerVisible()
    {
        Mesh.GetComponent<SkinnedMeshRenderer>().materials[0].CopyPropertiesFromMaterial(opaqueMat);
        Mesh.GetComponent<SkinnedMeshRenderer>().materials[1].CopyPropertiesFromMaterial(opaqueMat);
        //Mesh.GetComponent<SkinnedMeshRenderer>().materials[2].CopyPropertiesFromMaterial(opaqueMat);
    }

    private void xrayOn(){
        foreach (GameObject enemy in enemies){
            enemy.layer = 6;
            var children = enemy.transform.GetComponentsInChildren<Transform>(includeInactive: true);
            foreach (var child in children)
            {
                child.gameObject.layer = 6;
            }
        }
    }
    private void xrayOff(){
        foreach (GameObject enemy in enemies){
            enemy.layer = 0;
            var children = enemy.transform.GetComponentsInChildren<Transform>(includeInactive: true);
            foreach (var child in children)
            {
                child.gameObject.layer = 0;
            }
        }
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

    public void SaveInformation(int informationValue)
    {
        informationSaved += informationValue;
    }

    public void UpdateKillCount()
    {
        killCount += 1;
    }
}
