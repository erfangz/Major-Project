using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class SlidingBehaviour : MonoBehaviour
{
    #region Variables
    [BoxGroup("Sliding Parameters")]
    public float SlideSpeed;
    [BoxGroup("Sliding Parameters")]
    public float SlideTime;
    float slideTimer;
    float normalSize;
    float slidingSize;
    float verticalInput;

    public bool Sliding;
    #endregion

    #region References
    [BoxGroup("References")]
    public Transform Orientation;
    [BoxGroup("References")]
    public Transform Player;

    [BoxGroup("References")]
    public Rigidbody Rb;

    [BoxGroup("References")]
    public WallRunBehaviour WallRunBehaviour;

    [BoxGroup("References")]
    public KeyCode SlideKey;
    #endregion

    #region Methods
    void Start()
    {
        normalSize = Player.localScale.y;
    }

    void Update()
    {
        // set inputs
        verticalInput = Input.GetAxisRaw("Vertical");

        #region Input Check
        // start sliding
        if (Input.GetKeyDown(SlideKey) && (verticalInput != 0))
            StartSlide();
        if (Input.GetKeyUp(SlideKey) && Sliding)
            StopSlide();
        #endregion
    }

    void FixedUpdate()
    {
        if (Sliding)
            Slide();
    }

    /// <summary>
    /// Initiates the slide
    /// </summary>
    void StartSlide()
    {
        // if wall running, do nothing
        if (WallRunBehaviour.WallRunning)
            return;

        // start sliding
        Sliding = true;

        // scale down player model to appear lowered
        Player.localScale = new Vector3(Player.localScale.x, slidingSize, Player.localScale.z);

        Rb.AddForce(Vector3.down * 10f, ForceMode.Impulse);

        // set slide timer
        slideTimer = SlideTime;
    }

    /// <summary>
    /// Cancels the slide
    /// </summary>
    void StopSlide()
    {
        Sliding = false;

        // scale player up to inital size
        Player.localScale = new Vector3(Player.localScale.x, normalSize, Player.localScale.z);
    }

    /// <summary>
    /// Slide Logic
    /// </summary>
    void Slide()
    {
        Vector3 direction = Orientation.forward * verticalInput;

        if(Rb.velocity.y > -0.1f)
        {
            Rb.AddForce(direction.normalized * SlideSpeed, ForceMode.Force);

            slideTimer -= Time.fixedDeltaTime;
        }

        if (slideTimer <= 0)
            StopSlide();
    }
    #endregion
}
