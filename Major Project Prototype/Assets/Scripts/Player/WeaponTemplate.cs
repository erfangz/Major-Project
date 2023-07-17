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
    public int MagSize, ShotsPerSecond, RemainingShots, FiredShots;

    public bool Automatic;

    [SerializeField]
    bool reloading, canShoot;
    public bool Shooting;

    [BoxGroup("References")]
    public RaycastHit Hit;
    [BoxGroup("References")]
    public Ray FiringRay;
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
        RemainingShots = MagSize;
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
        if (Input.GetKeyDown(KeyCode.R) && RemainingShots < MagSize && !reloading)
            Reload();

        // Shoot
        if (canShoot && Shooting && !reloading && RemainingShots > 0)
        {
            // amount of shots fired per single tap
            FiredShots = 0;
            Shoot();
        }
    }

    public void Shoot()
    {
        canShoot = false;

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

        RemainingShots--;
        FiredShots++;

        Invoke("ResetShot", TimeBetweenShots);

        // shoots as many bullets per button tap as set in firedshots variable
        if (FiredShots < ShotsPerSecond && RemainingShots > 0)
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
        RemainingShots = MagSize;
        reloading = false;
    }
    #endregion
}
