using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

/// <summary>
/// Wall Running Logic
/// </summary>
public class WallRunBehaviour : MonoBehaviour
{
    #region Objects
    public enum MoveState
    {
        ONGROUND,
        ONWALL
    }
    public MoveState State;

    [BoxGroup("Object Layers")]
    public LayerMask Ground;
    [BoxGroup("Object Layers")]
    public LayerMask Wall;

    public GameObject GroundCheck;

    [BoxGroup("Object Checks")]
    public bool Grounded;
    [BoxGroup("Object Checks")]
    public bool OnLeftSideWall;
    [BoxGroup("Object Checks")]
    public bool OnRightSideWall;

    public bool WallRunning;

    [BoxGroup("Object Checks")]
    public float DistanceToWall;

    private RaycastHit wall_L;
    private RaycastHit wall_R;

    [BoxGroup("References")]
    public Transform Orientation;
    [BoxGroup("References")]
    public Rigidbody Rb;

    float verticalInput;
    float horizontalInput;
    #endregion

    #region Methods
    void FixedUpdate()
    {
        #region Checks
        // ground check
        Grounded = Physics.CheckSphere(GroundCheck.transform.position, 0.1f, Ground);

        // wall check
        OnLeftSideWall = Physics.Raycast(transform.position, -Orientation.right, out wall_L, DistanceToWall, Wall);
        OnRightSideWall = Physics.Raycast(transform.position, Orientation.right, out wall_R, DistanceToWall, Wall);
        #endregion
    }

    void Update()
    {

    }

    /// <summary>
    /// Controls Player Move State
    /// </summary>
    void SwitchStates(MoveState state)
    {
        // Wall Running State
        WallRunning = true;
        state = MoveState.ONWALL;

        // Normal Movement State
    }

    #region Movement
    /// <summary>
    /// How the Player moves when attached to a wall
    /// </summary>
    void OnWallMove()
    {


        if (OnLeftSideWall || OnRightSideWall && verticalInput > 0 && !Grounded)
        {
            State = MoveState.ONWALL;
        }
    }

    /// <summary>
    /// Jump to detach from wall with added momentum
    /// </summary>
    void JumpDetach()
    {

    }
    #endregion

    #endregion
}
