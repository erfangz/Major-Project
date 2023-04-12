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
        // movement axis
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // rotate orientation
        Vector3 viewDir = Player.position - new Vector3(transform.position.x, Player.position.y, transform.position.z);
        Orientation.forward = viewDir.normalized;

        // rotate player model

        #endregion

        #region Camera
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
