using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackBehavior : StateMachineBehaviour
{
    private Transform _playerTransform;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;
        animator.rootRotation = Quaternion.RotateTowards(animator.transform.rotation,
            Quaternion.LookRotation(_playerTransform.position - animator.transform.position),
            180f);
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Attack", false);
    }
}
