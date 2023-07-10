using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

// Script based on: https://www.youtube.com/watch?v=bqNW08Tac0Y

public class WeaponTemplate : MonoBehaviour
{
    #region Variables
    [BoxGroup("Stats")]
    public float TimeBetweenShots, Spread, Range, ReloadTime, FireDelay;
    [BoxGroup("Stats")]
    public int MagSize, ShotsPerSecond, Damage;

    int remainingShots, firedShots;

    public bool Automatic;

    [SerializeField]
    bool shooting, reloading, canShoot;

    [BoxGroup("References")]
    public RaycastHit Hit;
    [BoxGroup("References")]
    public Ray FiringRay;
    [BoxGroup("References")]
    public LayerMask Enemy;
    [BoxGroup("References")]
    public Transform BulletSpawn;
    [BoxGroup("References")]
    public Camera ThirdPersonCam;

    Vector3 direction;
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
            shooting = Input.GetKey(KeyCode.Mouse0);

        // Semi-Auto firing
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        // Reload
        if (Input.GetKeyDown(KeyCode.R) && remainingShots < MagSize && !reloading)
            Reload();

        // Shoot
        if (canShoot && shooting && !reloading && remainingShots > 0)
        {
            // amount of shots fired per single tap
            firedShots = ShotsPerSecond;
            Shoot();
        }
    }

    public void Shoot()
    {
        #region Weapon Fire
        canShoot = false;

        // Raycast
        FiringRay = new Ray(ThirdPersonCam.transform.position, direction);
        if(Physics.Raycast(FiringRay, out Hit, Range, Enemy))
        {
            // get the damage calculation from other gameobject and execute function
            if (Hit.collider.CompareTag("Enemy"))
                Hit.collider.GetComponent<AI_Health>().TakeHit(); // temporary, replace with proper function later
        }

        remainingShots--;
        firedShots--;

        Invoke("ResetShot", FireDelay);

        // shoots as many bullets per button tap as set in firedshots variable
        if (firedShots > 0 && remainingShots > 0)
            Invoke("Shoot", FireDelay);
        #endregion

        #region Bullet Spread
        float x = Random.Range(-Spread, Spread);
        float y = Random.Range(-Spread, Spread);

        // shot direction after spread
        direction = ThirdPersonCam.transform.forward + new Vector3(x, y, 0);
        #endregion
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
