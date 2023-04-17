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
    public bool invisibilityUsable;
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
    public GameObject miximoMesh;
    public GameObject aimingModelPosition;
    public bool hasGun;
    private GameObject gun;

    //score

    public static int informationDeleted = 0;
    public static int informationSaved = 0;
    public int killCount = 0;
    public int health;
    public int ammo;

    //prone
    public bool prone;
    public GameObject visibleMesh;
    public GameObject playerCameraRoot;
    public bool infrontOfVent;
    private GameObject ventInterior;
    private GameObject ventExterior;

    public GameObject proneRestingGunPosition;
    public GameObject proneAimingGunPosition;

    public HealthBar healthbar;

    Animator animator;

    public GameObject globalVariables;
    

    private void Start()
    {
        ammo = 20;
        defaultZoom = followCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView;

        if (hasGun)
        {
            gun = GameObject.FindWithTag("Gun");
        }

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (globalVariables.GetComponent<GlobalVariables>().collectedInvisibility == true)
        {
            invisibilityUsable = true;
            invisiblityIcon.SetActive(true);
        }
        if (globalVariables.GetComponent<GlobalVariables>().collectedXray == true)
        {
            xrayUsable = true;
            xrayIcon.SetActive(true);
        }

        //HealthBar
        healthbar.SetHealth(health);

        //ADS
        if (hasGun && Input.GetMouseButtonDown(1))
        {
            followCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = ADSZoom;
            gameObject.GetComponent<ThirdPersonController>().aiming = true;
            gun.GetComponent<ProjectileGun>().aiming = true;

            if (!prone) gun.transform.parent = aimingGunPosition.transform;
            else gun.transform.parent = proneAimingGunPosition.transform;

            gun.transform.localPosition = new Vector3(0f, 0f, 0f);
            gun.transform.localRotation = Quaternion.Euler(0, 0, 0);

            miximoMesh.transform.parent = aimingModelPosition.transform;

            animator.SetBool("ADS",true);
        }
        else if (hasGun && Input.GetMouseButtonUp(1))
        {
            followCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = defaultZoom;
            gameObject.GetComponent<ThirdPersonController>().aiming = false;
            gun.GetComponent<ProjectileGun>().aiming = false;

            if (!prone) gun.transform.parent = restingGunPosition.transform;
            else gun.transform.parent = proneRestingGunPosition.transform;

            gun.transform.localPosition = new Vector3(0f, 0f, 0f);
            gun.transform.localRotation = Quaternion.Euler(0, 0, 0);

            miximoMesh.transform.parent = gameObject.transform;

            animator.SetBool("ADS",false);
        }

        if (hasGun && Input.GetButtonDown("Aim")) 
        {
            followCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = ADSZoom;
            gameObject.GetComponent<ThirdPersonController>().aiming = true;
            gun.GetComponent<ProjectileGun>().aiming = true;

            if (!prone) gun.transform.parent = aimingGunPosition.transform;
            else gun.transform.parent = proneAimingGunPosition.transform;
           
            gun.transform.parent = aimingGunPosition.transform;
            gun.transform.localPosition = new Vector3(0f, 0f, 0f);
            gun.transform.localRotation = Quaternion.Euler(0, 0, 0);

            animator.SetBool("ADS",true);
        }
        else if (hasGun && Input.GetButtonUp("Aim"))
        {
            followCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = defaultZoom;
            gameObject.GetComponent<ThirdPersonController>().aiming = false;
            gun.GetComponent<ProjectileGun>().aiming = false;

            gun.transform.parent = restingGunPosition.transform;
            gun.transform.localPosition = new Vector3(0f, 0f, 0f);
            gun.transform.localRotation = Quaternion.Euler(0, 0, 0);

            animator.SetBool("ADS",false);
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

        if ((Input.GetKeyDown(KeyCode.X) || Input.GetButtonDown("Crouch")) && infrontOfVent){
            GoProne();
        }

        if ((Input.GetKey(KeyCode.C) || Input.GetButtonDown("Use"))  && xrayUsable)
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

        if (collision.gameObject.tag == "VentEntrance"){
           infrontOfVent = true;
           ventInterior = collision.GetComponent<Vent>().InsidePoint;
           ventExterior = collision.GetComponent<Vent>().OutsidePoint;
            if (prone){
                GoProne();
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Invisibility" || collision.gameObject.tag == "Xray")
        {
            standingOnPickup = false;
            pickupUnderPlayer = null;
        }

        if (collision.gameObject.tag == "VentEntrance"){
            infrontOfVent = false;
            ventInterior = null;
        }
    }

    public IEnumerator InvisibilityCountdown()
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
    }
    private void playerVisible()
    {
        Mesh.GetComponent<SkinnedMeshRenderer>().materials[0].CopyPropertiesFromMaterial(opaqueMat);
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

    public void GoProne(){
        
        //infrontOfVent = false;
        var cameraVars = followCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        float ADSZoomstore = ADSZoom;

        if (!prone){ //going prone
            infrontOfVent = false;
            prone = true;
            cameraVars.CameraDistance = 0;
            cameraVars.ShoulderOffset = new Vector3(0f, 0f, 0f);
            playerCameraRoot.transform.localPosition = new Vector3(0f, 1f, 0f);
            gameObject.GetComponent<ThirdPersonController>().aiming = true;
            gameObject.GetComponent<CharacterController>().height = 0f;
            visibleMesh.SetActive(false);
            GetComponent<ThirdPersonController>().JumpHeight = 0f;
            GetComponent<ThirdPersonController>().SprintSpeed = 2f;
            gameObject.GetComponent<CharacterController>().enabled = false;
            gameObject.transform.position = ventInterior.transform.position;
            gameObject.GetComponent<CharacterController>().enabled = true;

            gun.transform.parent = proneRestingGunPosition.transform;
            gun.transform.localPosition = new Vector3(0f, 0f, 0f);
            gun.transform.localRotation = Quaternion.Euler(0, 0, 0);

            ADSZoom = 30f;
        }
        else{ //standing up
            prone = false;
            gameObject.GetComponent<CharacterController>().enabled = false;
            gameObject.transform.position = ventExterior.transform.position;
            gameObject.GetComponent<CharacterController>().enabled = true;
            cameraVars.CameraDistance = 1.5f;
            cameraVars.ShoulderOffset = new Vector3(2f, 0.2f, 0f);
            playerCameraRoot.transform.localPosition = new Vector3(0f, 1.375f, 0f);
            gameObject.GetComponent<ThirdPersonController>().aiming = false;
            gameObject.GetComponent<CharacterController>().height = 1.8f;
            ADSZoom = ADSZoomstore;
            visibleMesh.SetActive(true);
            GetComponent<ThirdPersonController>().JumpHeight = 1.2f;
            GetComponent<ThirdPersonController>().SprintSpeed = 5.335f;

            gun.transform.parent = restingGunPosition.transform;
            gun.transform.localPosition = new Vector3(0f, 0f, 0f);
            gun.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void ReduceAmmo()
    {
        ammo -= 1;
    }

    public void RaiseAmmo(int ammoRaiseAmount)
    {
        ammo += ammoRaiseAmount;
    }

    public void RaiseHealth(int healthRaiseAmount)
    {
        if (health < 10)
            health += healthRaiseAmount;
    }
}
