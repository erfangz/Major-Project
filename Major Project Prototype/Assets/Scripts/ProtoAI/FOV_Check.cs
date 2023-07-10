using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

/// <summary>
/// Conditional node to check if a target is within this gameobjects field of view
/// </summary>
public class FOV_Check : Conditional
{
    #region Variables
    // later used by move behaviour to go to a target
    public SharedTransform Target;
    // the tag of the targets to be added into array
    public string TargetTag;
    // array of possible targets to acquire
    private Transform[] targetCollection;

    public float FOV;
    public float SightRange;
    #endregion

    #region Methods
    // behaviour designer version of unity update function
    public override TaskStatus OnUpdate()
    {
        // target spotted
        for (int i = 0; i < targetCollection.Length; i++)
        {
            // target within FOV
            if (InSight(targetCollection[i], FOV))
            {
                Target.Value = targetCollection[i];

                return TaskStatus.Success;
            }
        }

        // no target in sight
        return TaskStatus.Failure;
    }

    // behaviour designer version of unity awake function
    public override void OnAwake()
    {
        var targets = GameObject.FindGameObjectsWithTag(TargetTag);

        targetCollection = new Transform[targets.Length];

        for (int i = 0; i < targets.Length; i++)
            targetCollection[i] = targets[i].transform;
    }

    /// <summary>
    /// Adds a field of view to the gameobject
    /// </summary>
    /// <param name="Target">the target to look for</param>
    /// <param name="fov"> angle of the gameobjects sight</param>
    /// <returns></returns>
    public bool InSight(Transform Target, float fov)
    {
        Vector3 direction = Target.position - transform.position;

        return Vector3.Angle(direction, transform.forward) < fov && Vector3.Distance(transform.position, Target.position) <= SightRange;
    }
    #endregion
}
