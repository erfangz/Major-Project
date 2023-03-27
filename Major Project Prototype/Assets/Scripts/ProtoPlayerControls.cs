using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

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
    #endregion

    #region Functions
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region Movement

        #endregion

        #region Camera

        #endregion
    }
    #endregion
}
