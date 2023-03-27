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
    CinemachineFreeLook freeLook;

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

        #endregion

        #region Camera
        freeLook.LookAt = LookAtPos_2;
        #endregion
    }
    #endregion
}
