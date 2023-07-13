using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

// Script based on: https://www.youtube.com/watch?v=bqNW08Tac0Y

public class WeaponTemplate : MonoBehaviour
{
    #region Variables
    [BoxGroup("Stats")]
    public float TimeBetweenShots, Spread, Range, ReloadTime, FireDelay, Force;
    [BoxGroup("Stats")]
    public int MagSize, ShotsPerSecond;

    int remainingShots, firedShots;

    public bool Automatic;

    [SerializeField]
    bool reloading, canShoot;
    public bool Shooting;

    [BoxGroup("References")]
    public RaycastHit Hit;
    [BoxGroup("References")]
    public Ray FiringRay;
    //[BoxGroup("References")]
    //public LayerMask Enemy;
    [BoxGroup("References")]
    public Transform BulletSpawn;
    [BoxGroup("References")]
    public GameObject Bullet;
    [BoxGroup("References")]
    public Camera ThirdPersonCam;

    // bullet trajectory
    Vector3 direction;
    // bullet trajectory after adding spread
    Vector3 direction1;
    #endregion

    #region Methods
    private void Update()
    {
        GetInput();
    }

    private void Awake()
    {
        remainingShots = MagSize;
        canShoot = true;
    }

    /// <summary>
    /// Manages Inputs
    /// </summary>
    void GetInput()
    {
        // Automatic firing
        if (Automatic)
            Shooting = Input.GetKey(KeyCode.Mouse0);

        // Semi-Auto firing
        else Shooting = Input.GetKeyDown(KeyCode.Mouse0);

        // Reload
        if (Input.GetKeyDown(KeyCode.R) && remainingShots < MagSize && !reloading)
            Reload();

        // Shoot
        if (canShoot && Shooting && !reloading && remainingShots > 0)
        {
            // amount of shots fired per single tap
            //firedShots = ShotsPerSecond;
            firedShots = 0;
            Shoot();
        }
    }

    public void Shoot()
    {
        canShoot = false;

        #region old code
        // Raycast
        //FiringRay = new Ray(ThirdPersonCam.transform.position, direction);
        //if(Physics.Raycast(FiringRay, out Hit, Range, Enemy))
        //{
        //    // get the damage calculation from other gameobject and execute function
        //    if (Hit.collider.CompareTag("Enemy"))
        //        Hit.collider.GetComponent<AI_Health>().TakeHit(); // temporary, replace with proper function later
        //}
        #endregion

        FiringRay = ThirdPersonCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));


        Vector3 targetpoint;
        if (Physics.Raycast(FiringRay, out Hit))
            targetpoint = Hit.point;
        // shooting in the air, get a random point far away from players position
        else targetpoint = FiringRay.GetPoint(100);

        #region Bullet Spread
        float x = Random.Range(-Spread, Spread);
        float y = Random.Range(-Spread, Spread);

        // shot direction without spread
        direction = targetpoint - BulletSpawn.position;

        // shot direction after spread
        direction1 = direction + new Vector3(x, y, 0);
        #endregion

        #region Instantiations
        // Bullet
        GameObject projectile = Instantiate(Bullet, BulletSpawn.position, Quaternion.identity);

        projectile.transform.forward = direction1.normalized;

        // bullet force
        projectile.GetComponent<Rigidbody>().AddForce(direction1.normalized * Force, ForceMode.Impulse);

        // muzzle flash
        #endregion

        remainingShots--;
        firedShots++;

        Invoke("ResetShot", FireDelay);

        // shoots as many bullets per button tap as set in firedshots variable
        if (firedShots < ShotsPerSecond && remainingShots > 0)
            Invoke("Shoot", FireDelay);
    }

    public void ResetShot()
    {
        canShoot = true;
    }

    /// <summary>
    /// Perform a Reload
    /// </summary>
    public void Reload()
    {
        reloading = true;

        // play reload animation

        Invoke("Reloaded", ReloadTime);
    }

    public void Reloaded()
    {
        remainingShots = MagSize;
        reloading = false;
    }
    #endregion
}
