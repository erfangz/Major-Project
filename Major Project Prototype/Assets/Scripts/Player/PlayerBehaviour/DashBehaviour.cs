using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

/// <summary>
/// Dashes in movement direction, can be used while grounded or airborne
/// </summary>
public class DashBehaviour : MonoBehaviour
{
    #region Objects
    public Rigidbody PlayerRb;

    public PlayerMove PlayerMove;

    [BoxGroup("Dash Parameters")]
    public float DashModifier;
    [BoxGroup("Dash Parameters")]
    public float DashTime;

    public bool Grounded;
    public bool CanDash = true;
    #endregion

    #region Methods
    void FixedUpdate()
    {
        // Groundcheck
        Grounded = PlayerMove.Grounded;

        DashControl();
    }

    /// <summary>
    /// Dash Mechanic
    /// </summary>
    void Dash()
    {
        // dash direction
        var x = Input.GetAxisRaw("Horizontal");
        var z = Input.GetAxisRaw("Vertical");

        Vector3 dashDirection = PlayerMove.Orientation.TransformDirection(x, PlayerRb.velocity.y, z);

        if (dashDirection == Vector3.zero)
            return;

        PlayerRb.velocity = (dashDirection.normalized * DashModifier);

        StartCoroutine(StopDash());
    }

    /// <summary>
    /// The controls of the Dash
    /// </summary>
    void DashControl()
    {
        // Dash Input
        if (Input.GetKeyDown(KeyCode.LeftControl) && CanDash)
        {
            Dash();
            CanDash = false;
        }

        // Dash refresh when grounded: minimal cooldown between dashes to hinder continous dashing
        if (Grounded && !CanDash)
            StartCoroutine(CoolDown(DashTime));

        // Dash refresh when airborne: can dash again upon landing
        if (!Grounded && !CanDash)
            if (Grounded)
                CanDash = true;
    }

    /// <summary>
    /// Duration of the Dash
    /// </summary>
    /// <returns></returns>
    private IEnumerator StopDash()
    {
        yield return new WaitForSeconds(DashTime);
        PlayerRb.velocity = Vector3.zero;
    }

    private IEnumerator CoolDown(float cooldown)
    {
        CanDash = false;
        yield return new WaitForSeconds(cooldown);
        CanDash = true;
    }
    #endregion
}
