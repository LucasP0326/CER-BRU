using UnityEngine;
using TMPro;

public class ProjectileGun : MonoBehaviour
{
    //Player
    public GameObject player;
    
    //Shoot Audio
    public AudioSource ShootSFX;

    //Reload Audio
    public AudioSource ReloadSFX;

    //bullet 
    public GameObject bullet;

    //bullet force
    public float shootForce, upwardForce;

    //Gun stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    int bulletsLeft, bulletsShot;

    //Empty Gun SFX
    public AudioSource empty;

    //bools
    bool shooting, readyToShoot, reloading;
    public bool aiming;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;

    //Graphics
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;
    public GameObject lightBeam;

    //bug fixing :D
    public bool allowInvoke = true;

    //equipped
    public bool equipped;

    public GameObject crosshair;

    private void Awake()
    {
        //make sure magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();

        //Set ammo display, if it exists :D
        if (ammunitionDisplay != null)
        {
            if (player.GetComponent<Player>().ammo > 0)
                ammunitionDisplay.SetText("AMMO:  " + bulletsLeft + " / " + player.GetComponent<Player>().ammo);
            if (player.GetComponent<Player>().ammo == 0)
                ammunitionDisplay.SetText("OUT OF AMMO");
        }
    }
    private void MyInput()
    {
        //Check if allowed to hold down button and take corresponding input
        if (allowButtonHold) 
            shooting = Input.GetButton("Shoot");
        else 
            shooting = Input.GetButtonDown("Shoot");
        //Reloading 
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
        //Reload automatically when trying to shoot without ammo
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();

        //Shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0 && Time.timeScale > 0 && aiming && player.GetComponent<Player>().ammo > 0)
        {
            //Set bullets shot to 0
            bulletsShot = 0;

            Shoot();
            
            if (player.GetComponent<Player>().ammo <= 0)
                empty.Play();
        }
    }

    private void Shoot()
    {
        player.GetComponent<Player>().ReduceAmmo();
        crosshair.GetComponent<Crosshair>().FireGun();
        ShootSFX.Play ();

        readyToShoot = false;

        //Find the exact hit position using a raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Just a ray through the middle of your current view

        //check if ray hits something
        Vector3 targetPoint = ray.GetPoint(75);

        //Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //Just add spread to last direction

        //Instantiate bullet/projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
        //Rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        //Instantiate muzzle flash, if you have one
        if (muzzleFlash != null)
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot++;

        //Invoke resetShot function (if not already invoked), with your timeBetweenShooting
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;


        }

        //if more than one bulletsPerTap make sure to repeat shoot function
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {
        //Allow shooting and invoking again
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        //Move to reload position
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, 75);
        ReloadSFX.Play ();
        Invoke("ReloadFinished", reloadTime); //Invoke ReloadFinished function with your reloadTime as delay
    }
    private void ReloadFinished()
    {
        //Reset gun position
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        //Fill magazine
        bulletsLeft = magazineSize;
        reloading = false;
    }
}