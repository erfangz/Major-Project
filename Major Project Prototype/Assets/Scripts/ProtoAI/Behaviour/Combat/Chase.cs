using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

/// <summary>
/// Action node to chase a target acquired by FOV_Check Node
/// </summary>
public class Chase : Action
{
    #region References
    public SharedTransform Target;
    public float MoveSpeed;
    #endregion

    #region Methods
    public override TaskStatus OnUpdate()
    {
        #region Chase Logic
        // check distance to target
        if (Vector3.SqrMagnitude(transform.position - Target.Value.position) > 10f)
        {
            // move to target location
            transform.position = Vector3.MoveTowards(transform.position, Target.Value.position, MoveSpeed * Time.deltaTime);

            // look at the target
            transform.LookAt(Target.Value);

            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
        #endregion
    }
    #endregion
}