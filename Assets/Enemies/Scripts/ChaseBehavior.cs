using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseBehavior : StateMachineBehaviour
{
    private Transform _playerTransform;
    private NavMeshAgent _agent;
    public float attackRange;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;
        _agent = animator.GetComponentInParent<NavMeshAgent>();
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator)
        animator.SetInteger("Chance", Random.Range(0, 10));
        _agent.destination = _playerTransform.position;
        animator.SetFloat("Speed", _agent.velocity.magnitude);
        animator.SetFloat("RangeToPlayer", Vector3.Distance(animator.transform.position, _playerTransform.position));
        if (Vector3.Distance(animator.transform.position, _playerTransform.position) <= attackRange)
        {
            animator.SetBool("Attack", true);
        }
        
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
