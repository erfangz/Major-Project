using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Melee_Check : Conditional
{
    #region Variables
    public bool InMeleeRange;

    [SerializeField]
    float meleeRange;

    public LayerMask Layer;
    #endregion

    #region Methods
    public override TaskStatus OnUpdate()
    {
        // distance check
        InMeleeRange = Physics.CheckSphere(transform.position, meleeRange, Layer);

        if (InMeleeRange)
            return TaskStatus.Success;
        else
            return TaskStatus.Failure;
    }
    #endregion
}