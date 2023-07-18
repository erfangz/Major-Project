using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class MeleeAttack : Action
{
    #region Variables
    MonoBehaviour mono;
    public float AtkSpeed;

    bool attacking;

    Animator animator;
    #endregion

    #region Methods

    public override void OnAwake()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    public override TaskStatus OnUpdate()
    {
        if (!attacking)
        {
            Attack();
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }

    void Attack()
    {
        attacking = true;

        // play animation
        //animator.SetBool("MeleeAttack", true);
        Debug.Log("Melee");

        mono.Invoke("ResetAttack", 5f);
    }

    public void ResetAttack()
    {
        attacking = false;
    }
    #endregion
}