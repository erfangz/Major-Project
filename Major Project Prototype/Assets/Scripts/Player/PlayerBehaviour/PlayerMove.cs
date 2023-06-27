using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

/// <summary>
/// Basic Movement behaviour of the Player
/// </summary>
public class PlayerMove : MonoBehaviour
{
    #region Objects
    public Rigidbody PlayerRb;

    public GameObject GroundCheck;

    public Transform Orientation;

    public LayerMask Ground;

    private Vector3 velocity;

    [BoxGroup("Player Variables")]
    [Tooltip("Player movement speed")]
    public float MoveSpeed;
    [BoxGroup("Player Variables")]
    [Tooltip("The strength of the players jump")]
    public float JumpForce;
    [HideInInspector]
    public int JumpCount = 1;

    public bool Grounded;
    #endregion

    #region Methods
    void Update()
    {
        Move();
        Jump();
    }

    /// <summary>
    /// Moves the player
    /// </summary>
    void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Vector3 moveDirection = Orientation.TransformDirection(direction);

        PlayerRb.MovePosition(Orientation.position + moveDirection * MoveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Makes player jump
    /// </summary>
    void Jump()
    {
        // ground check
        Grounded = Physics.CheckSphere(GroundCheck.transform.position, 0.1f, Ground);

        if (velocity.y < 0)
            velocity.y = -2f;

        // capable of jumping
        if (JumpCount > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            JumpCount -= 1;
            PlayerRb.AddForce(Vector3.up * JumpForce);
        }

        // jump refresh upon touching ground
        if (Grounded && JumpCount != 1)
            JumpCount = 1;
    }
    #endregion
}
