using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Cinemachine;

public class ProtoPlayerControls : MonoBehaviour
{
    #region Objects
    [BoxGroup("Player References")]
    [Tooltip("The empty Player GameObject handling logic")]
    public Transform Player;
    [BoxGroup("Player References")]
    [Tooltip("The Player Model")]
    public Transform PlayerGameObject;
    [BoxGroup("Player References")]
    [Tooltip("Relative Direction of Player to Camera and World Space during movement")]
    public Transform Orientation;

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

    [BoxGroup("Player Variables")]
    [SerializeField]
    private int jumpCount;

    [BoxGroup("Camera References")]
    [Tooltip("Camera Sensitivity")]
    public float RotationSpeed;
    [BoxGroup("Camera References")]
    [Tooltip("Cinemachine LookAt Target for out-of-combat camera movement")]
    public Transform LookAtPos_1;
    [BoxGroup("Camera References")]
    [Tooltip("Cinemachine LookAt Target for in-combat camera movement")]
    public Transform LookAtPos_2;

    /// <summary>
    /// Switches between different modes of Camera movement, protoversion via button input to switch modes on demand
    /// </summary>
    public enum CameraState
    {
        TRAVERSE,
        COMBAT
    }

    public CameraState CamMode;
    #endregion

    #region Functions
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        #region Movement
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
        if(grounded && Input.GetKeyDown(KeyCode.Space))
        {
            PlayerRb.AddForce(Vector3.up * JumpForce);
        }
        #endregion

        #region Camera
        // rotate orientation
        Vector3 viewDir = Player.position - new Vector3(transform.position.x, Player.position.y, transform.position.z);
        Orientation.forward = viewDir.normalized;

        // rotate player model
        // movement behaviour in traverse mode
        if (CamMode == CameraState.TRAVERSE)
        {
            // movement axis
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = Orientation.forward * verticalInput + Orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
                PlayerGameObject.forward = Vector3.Slerp(PlayerGameObject.forward, inputDir.normalized, Time.deltaTime * RotationSpeed);
        }

        // movement behaviour in combat mode
        //if(CamMode == CameraState.COMBAT)
        //{

        //}

        // switch between camera modes
        if (Input.GetKeyDown(KeyCode.Alpha1))
            CamMode = CameraState.TRAVERSE;

        if (Input.GetKeyDown(KeyCode.Alpha2))
            CamMode = CameraState.COMBAT;

        // camera behaviour in traverse mode


        // camera behaviour in combat mode
        #endregion
    }
    #endregion
}
