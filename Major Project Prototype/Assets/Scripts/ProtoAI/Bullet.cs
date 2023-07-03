using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Parameters
    public float Damage
    {
        get
        {
            return damage;
        }
    }

    [SerializeField]
    // Defines the damage value dealt by the enemy
    private int damage = 20;
    private Rigidbody rb;
    #endregion

    #region Methods
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (rb.velocity.sqrMagnitude > 0)
        {
            transform.forward = rb.velocity;
        }
    }

    private void OnCollisionEnter(Collision _collision)
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// upon collision with tagged player, deal damage and destroy bullet
    /// on other collision only destroy the bullet
    /// </summary>
    /// <param name="_other"></param>
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            //Health health = _other.GetComponent<Health>();
            //health.ReceiveDamage(damage);

            Destroy(gameObject);
        }

    }

    /// <summary>
    /// defines the lifespan of the bullet
    /// </summary>
    /// <param name="_impulse"></param>
    /// <param name="_lifeSpan"></param>
    public void Init(Vector3 _impulse, float _lifeSpan)
    {
        rb.AddForce(_impulse, ForceMode.Impulse);

        Destroy(gameObject, _lifeSpan);
    }
    #endregion
}
