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

    [SerializeField]
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

    public KeyCode JumpKey = KeyCode.Space;
    #endregion

    #region Methods
    void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        Jump();
        
    }

    /// <summary>
    /// Moves the player
    /// </summary>
    void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Vector3 moveDirection = Orientation.TransformDirection(direction);

        //PlayerRb.MovePosition(Orientation.position + moveDirection * MoveSpeed * Time.deltaTime);
        PlayerRb.velocity = new Vector3(moveDirection.x * MoveSpeed, PlayerRb.velocity.y, moveDirection.z * MoveSpeed);
        //PlayerRb.AddForce(moveDirection.normalized * MoveSpeed, ForceMode.Force);
    }

    /// <summary>
    /// Makes player jump
    /// </summary>
    void Jump()
    {
        // ground check
        Grounded = Physics.CheckSphere(GroundCheck.transform.position, 0.1f, Ground);

        //if (velocity.y < 0)
        //    velocity.y = -2f;

        // capable of jumping
        if (JumpCount > 0 && Input.GetKeyDown(JumpKey))
        {
            Debug.Log("Jump");
            JumpCount -= 1;
            PlayerRb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }

        // jump refresh upon touching ground
        if (Grounded && JumpCount != 1)
            JumpCount = 1;
    }
    #endregion
}
