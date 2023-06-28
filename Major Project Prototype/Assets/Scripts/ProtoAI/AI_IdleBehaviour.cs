using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class AI_IdleBehaviour : Action
{
    public SharedTransform Target;

    public override void OnStart()
    {

    }

    public override TaskStatus OnUpdate()
    {
        if (Target == null)
        {
            Idle();
        }
        return TaskStatus.Running;
    }

    void Idle()
    {
        Debug.Log("Idle");
        // idle animation
        transform.Rotate(Vector3.right, 20f);
    }
}