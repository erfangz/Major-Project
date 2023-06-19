using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerCamMove : MonoBehaviour
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
    [BoxGroup("Player References")]
    public Rigidbody PlayerRb;

    [BoxGroup("Camera References")]
    [Tooltip("Camera Sensitivity")]
    public float RotationSpeed;
    [BoxGroup("Camera References")]
    [Label("Combat Cam")]
    public Transform CombatLookAt;

    public CameraMode CamMode;
    #endregion

    #region Camera States
    public enum CameraMode
    {
        TRAVERSE,
        COMBAT
    }
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// How the Camera behaves outside of fighting
    /// </summary>
    void TraversalBehaviour()
    {
        // rotate orientation
        Vector3 viewDirection = Player.position - new Vector3(transform.position.x, Player.position.y, transform.position.z);

        Orientation.forward = viewDirection.normalized;

        //rotate player model
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 inputDirection = Orientation.forward * verticalInput + Orientation.right * horizontalInput;

        if (inputDirection != Vector3.zero)
            PlayerGameObject.forward = Vector3.Slerp(PlayerGameObject.forward, inputDirection.normalized, Time.deltaTime * RotationSpeed);
    }

    /// <summary>
    /// How the Camera behaves when the player is in a fighting state (shooting, aiming)
    /// </summary>
    void CombatBehaviour()
    {
        Vector3 combatViewDirection = CombatLookAt.position - new Vector3(transform.position.x, CombatLookAt.position.y, transform.position.z);

        Orientation.forward = combatViewDirection.normalized;

        PlayerGameObject.forward = combatViewDirection.normalized;
    }
    #endregion
}
