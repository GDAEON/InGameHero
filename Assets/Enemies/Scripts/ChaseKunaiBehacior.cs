using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseKunaiBehacior : StateMachineBehaviour
{
    private Transform _playerTransform;
    private NavMeshAgent _agent;
    private float _distance;
    public float attackRange;
    public float avoidRange;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;
        _agent = animator.GetComponentInParent<NavMeshAgent>();
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        _distance = Vector3.Distance(animator.transform.position, _playerTransform.position);
        if (_distance < avoidRange)
        {
            _agent.destination = animator.transform.position - _playerTransform.position;
        }
        else
        {
            _agent.destination = _playerTransform.position;
            if (_distance <= attackRange)
            {
                animator.SetBool("Attack", true);
            }
        }
        animator.SetFloat("Speed", _agent.velocity.magnitude);
        animator.SetFloat("RangeToPlayer", _distance);
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}
