using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class WeaponShoot : MonoBehaviour
{
    #region Parameters
    public float FireRate;
    float fireDelay;
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
            Shoot();
    }

    void Shoot()
    {

    }
    #endregion
}
