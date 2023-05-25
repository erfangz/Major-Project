using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Health : MonoBehaviour
{
    [SerializeField]
    private int healthpoints;

    private void Awake()
    {
        healthpoints = 20;
    }

    public bool TakeHit()
    {
        healthpoints -= 10;

        bool isDead = healthpoints <= 0;

        if (isDead)
            Die();

        return isDead;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
