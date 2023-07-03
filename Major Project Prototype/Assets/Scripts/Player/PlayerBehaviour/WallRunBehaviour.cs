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
        NormalMovement,
        WallMovement
    }
    public MoveState State;

    [BoxGroup("Object Layers")]
    public LayerMask Ground;
    [BoxGroup("Object Layers")]
    public LayerMask Wall;

    [BoxGroup("Object Checks")]
    public bool Grounded;
    [BoxGroup("Object Checks")]
    public bool OnLeftSideWall;
    [BoxGroup("Object Checks")]
    public bool OnRightSideWall;

    public bool WallRunning;
    public bool HorizontalWallRun;
    public bool VerticalWallRun;

    [BoxGroup("Object Checks")]
    public float DistanceToWall;

    private RaycastHit wall_L;
    private RaycastHit wall_R;

    [BoxGroup("References")]
    public Transform Orientation;
    [BoxGroup("References")]
    public Rigidbody Rb;
    [BoxGroup("References")]
    public PlayerMove PlayerMove;
    Vector3 wallNormal;

    float verticalInput;
    float horizontalInput;
    #endregion

    #region Methods
    void FixedUpdate()
    {

        if (WallRunning)
        {
            OnWallMove();

            // Cancel Wallrun via Input of LeftCtrl
            if (Input.GetKeyDown(KeyCode.LeftControl))
                StopWallRun();
        }
    }

    void Update()
    {
        #region Input Checks
        // Get Inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        #endregion

        #region Checks
        // ground check
        Grounded = PlayerMove.Grounded;

        // wall check
        OnLeftSideWall = Physics.Raycast(transform.position, -Orientation.right, out wall_L, DistanceToWall, Wall);
        OnRightSideWall = Physics.Raycast(transform.position, Orientation.right, out wall_R, DistanceToWall, Wall);
        #endregion

        SwitchStates();
    }

    /// <summary>
    /// Initiates Wallrun logic
    /// </summary>
    void BeginnWallRun()
    {
        //Rb.useGravity = false;

        WallRunning = true;

        Rb.velocity = new Vector3(Rb.velocity.x, 0f, Rb.velocity.z);
    }

    /// <summary>
    /// Terminates Wallrun Logic
    /// </summary>
    void StopWallRun()
    {
        WallRunning = false;

        State = MoveState.NormalMovement;

        //Rb.useGravity = true;
    }

    /// <summary>
    /// Controls Player Move State
    /// </summary>
    void SwitchStates()
    {
        // Wall Running State
        if (OnLeftSideWall || OnRightSideWall && verticalInput > 0 || horizontalInput > 0 && !Grounded)
        {
            if (!WallRunning)
                BeginnWallRun();
        }

        // Normal Movement State
        if (!WallRunning)
            State = MoveState.NormalMovement;
    }

    #region Movement
    /// <summary>
    /// How the Player moves when attached to a wall
    /// </summary>
    void OnWallMove()
    {
        // determine wall normal
        if (OnRightSideWall)
            wallNormal = wall_R.normal;

        if (OnLeftSideWall)
            wallNormal = wall_L.normal;

        // determine relative forward direction to wall
        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if ((Orientation.forward - wallForward).magnitude > (Orientation.forward - -wallForward).magnitude)
            wallForward = -wallForward;

        // Forward movement on wall
        Rb.AddForce(wallForward * PlayerMove.MoveSpeed, ForceMode.Force);

        // Push player towards wall
        if (!(OnLeftSideWall && horizontalInput > 0) && !(OnRightSideWall && horizontalInput < 0))
            Rb.AddForce(-wallNormal * 100, ForceMode.Force);
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
