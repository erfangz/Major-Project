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

    [BoxGroup("References")]
    public Transform Orientation;
    [BoxGroup("References")]
    public Rigidbody Rb;
    #endregion

    #region Methods
    void Update()
    {
        #region Checks
        // ground check
        Grounded = Physics.CheckSphere(GroundCheck.transform.position, 0.1f, Ground);

        // wall check
        //OnLeftSideWall = Physics.Raycast(transform.position, -Orientation.right, out );
        #endregion
    }

    /// <summary>
    /// Controls Player Move State
    /// </summary>
    void SwitchStates()
    {

    }

    #region Movement
    /// <summary>
    /// How the Player moves when attached to a wall
    /// </summary>
    void OnWallMove()
    {

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
