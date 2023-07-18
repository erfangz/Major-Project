using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

// Script based on: https://www.youtube.com/watch?v=bqNW08Tac0Y

/// <summary>
/// Template for creation of different kind of Gun Types
/// </summary>
public class WeaponTemplate : MonoBehaviour
{
    #region Variables
    [BoxGroup("Stats")]
    public float TimeBetweenShots, Spread, ReloadTime, FireDelay, Force;
    [BoxGroup("Stats")]
    public int MagSize, ShotsPerSecond, RemainingShots, FiredShots;

    public bool Automatic;

    public bool Reloading, CanShoot;
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
        CanShoot = true;
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
        if (Input.GetKeyDown(KeyCode.R) && RemainingShots < MagSize && !Reloading)
            Reload();

        // Shoot
        if (CanShoot && Shooting && !Reloading && RemainingShots > 0)
        {
            // amount of shots fired per single tap
            FiredShots = 0;
            Shoot();
        }
    }

    public void Shoot()
    {
        CanShoot = false;

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
        CanShoot = true;
    }

    /// <summary>
    /// Perform a Reload
    /// </summary>
    public void Reload()
    {
        Reloading = true;

        // play reload animation

        Invoke("Reloaded", ReloadTime);
    }

    public void Reloaded()
    {
        RemainingShots = MagSize;
        Reloading = false;
    }
    #endregion
}
