using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using TooltipAttribute = UnityEngine.TooltipAttribute;

/// <summary>
/// AI Behaviour during combat
/// </summary>
public class RangedAttack : Action
{
    #region Variables
    //[Tooltip("Fired bullets per second")]
    //public float FireRate;
    //[SerializeField]
    //[Tooltip("Duration of burst fire")]
    //float burstDuration;
    //[SerializeField]
    //[Tooltip("Delay between bursts")]
    //float fireDelay;
    //public float ReloadTime;

    public float AttackRange;
    public SharedFloat AwarenessRange;

    public LayerMask Enemy;

    public int CurrentAmmo;
    public int MaxAmmo;

    [SerializeField]
    bool canShoot;
    [SerializeField]
    bool inAttackRange;
    #endregion

    #region References
    public SharedTransform Target;

    public WeaponTemplate Weapon;
    #endregion

    #region Methods
    public override void OnAwake()
    {
        //MaxAmmo = Weapon.MagSize;
        //CurrentAmmo = MaxAmmo;
        Weapon.FiringRay = new Ray(transform.position, Vector3.forward);
    }

    #region old code
    //public override TaskStatus OnUpdate()
    //{
    //    Reload();


    //    if (inAttackRange)
    //    {
    //        canShoot = true;
    //        Shoot();

    //        return TaskStatus.Success;
    //    }
    //    else
    //    {
    //        Debug.Log("Attack failed");
    //        return TaskStatus.Failure;
    //    }
    //}

    /// <summary>
    /// Shoots the equipped weapon
    /// </summary>
    //void Shoot()
    //   {
    //       if (canShoot)
    //           StartCoroutine(BurstFire());

    //       if (!canShoot)
    //       {
    //           StopCoroutine(BurstFire());
    //           StartCoroutine(DelayFire());
    //       }
    //   }

    /// <summary>
    /// Reloads the weapon on empty ammo
    /// </summary>
    //void Reload()
    //   {
    //       if (CurrentAmmo <= 0)
    //       {
    //           // play reload animation
    //           StartCoroutine(Reloading());
    //       }
    //   }

    #region Enumerators
    /// <summary>
    /// Fires weapon for set amount of time
    /// </summary>
    /// <returns></returns>
    //private IEnumerator BurstFire()
    //{

    //    yield return new WaitForSeconds(burstDuration);
    //    canShoot = false;
    //}

    /// <summary>
    /// Delay between next attack instance
    /// </summary>
    /// <returns></returns>
    //private IEnumerator DelayFire()
    //{
    //    yield return new WaitForSeconds(fireDelay);
    //    canShoot = true;
    //}

    /// <summary>
    /// Reloads weapon
    /// </summary>
    /// <returns></returns>
    //private IEnumerator Reloading()
    //{
    //    canShoot = false;
    //    yield return new WaitForSeconds(ReloadTime);
    //    CurrentAmmo = MaxAmmo;
    //    canShoot = true;
    //}
    #endregion
    #endregion

    public override TaskStatus OnUpdate()
    {
        canShoot = Weapon.CanShoot;

        // look at the target
        transform.LookAt(Target.Value);

        inAttackRange = Physics.CheckSphere(transform.position, AttackRange, Enemy);
        
        if (inAttackRange)
        {
            Attack();
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }

    void Attack()
    {
        //Weapon.Shooting = true;
        //Weapon.Shoot();

        Debug.Log("Fire");
    }
    #endregion
}