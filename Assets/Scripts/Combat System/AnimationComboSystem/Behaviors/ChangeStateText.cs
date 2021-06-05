using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStateText : StateMachineBehaviour
{
    [Header("State Info")]
    public string _stateName = "";

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // if (animator.GetComponent<PlayerController>()._stateText != null)
        //     animator.GetComponent<PlayerController>()._stateText.text = _stateName;
    }
}
