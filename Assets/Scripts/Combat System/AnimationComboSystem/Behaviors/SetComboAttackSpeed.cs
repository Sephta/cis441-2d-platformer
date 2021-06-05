using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetComboAttackSpeed : StateMachineBehaviour
{
    [Header("Configurable Data")]
    [SerializeField, Range(0f, 10f)] private float attackAnimationSpeed = 0f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("playerAttackSpeed", attackAnimationSpeed);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("playerAttackSpeed", 1f);
    }
}
