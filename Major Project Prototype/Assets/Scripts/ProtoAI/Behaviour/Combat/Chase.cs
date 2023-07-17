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
    public float StoppingPoint;

    public FOV_Check FOV;
    #endregion

    #region Methods
    public override TaskStatus OnUpdate()
    {
        #region Chase Logic
        // check distance to target
        if (FOV.InSight(Target.Value, FOV.FOV))
        {
            // move to target location
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(Target.Value.position.x + StoppingPoint, Target.Value.position.y, Target.Value.position.z + StoppingPoint)
                , MoveSpeed * Time.deltaTime);

            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
        #endregion
    }
    #endregion
}