using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody PlayerRb;

    public GameObject GroundCheck;

    public LayerMask Ground;

    private Vector3 velocity;

    [BoxGroup("Player Variables")]
    [Tooltip("Player movement speed")]
    public float MoveSpeed;
    [BoxGroup("Player Variables")]
    [Tooltip("The strength of the players jump")]
    public float JumpForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // walking
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Vector3 moveDirection = transform.TransformDirection(direction);

        PlayerRb.MovePosition(transform.position + moveDirection * MoveSpeed * Time.deltaTime);

        // jumping
        // ground check
        bool grounded = Physics.CheckSphere(GroundCheck.transform.position, 0.1f, Ground);

        if (grounded && velocity.y < 0)
            velocity.y = -2f;

        // if grounded, capable of jumping
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            PlayerRb.AddForce(Vector3.up * JumpForce);
        }
    }
}
