using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboIdleBehavior : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ComboManager._inst.canRecieveInput = true;
        ComboManager._inst.isAttacking = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (ComboManager._inst.inputRecieved)
        {
            ComboManager._inst.isAttacking = true;
            animator.SetTrigger(ComboManager.AttackNames.Attack01.ToString());
            ComboManager._inst.ToggleCanRecieveInput();
            ComboManager._inst.inputRecieved = false;
        }
    }
}
