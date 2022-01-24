using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class KunaiEscapeBehavior : StateMachineBehaviour
{
    private Transform _playerTransform;
    private NavMeshAgent _agent;
    private float _distance;
    private bool _getCorrectPoint;
    private bool _isEscaping;
    private Vector3 target;
    public float escapeDistance;
    private static readonly int Speed = Animator.StringToHash("Speed");

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;
        _agent = animator.GetComponentInParent<NavMeshAgent>();
        _agent.stoppingDistance = 1;
        GoToRandomPoint(animator);
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _distance = Vector3.Distance(animator.transform.position, _playerTransform.position);
        animator.SetFloat("DistanceToPlayer", _distance);
        if (Vector3.Distance(target, animator.transform.position) <= 1.1f)
            _isEscaping = false;
        if (_distance < escapeDistance && !_isEscaping)
        {
            GoToRandomPoint(animator);
        }
        animator.SetFloat(Speed, _agent.velocity.magnitude);
    }

    private void GoToRandomPoint(Animator animator)
    {
        _getCorrectPoint = false;
        NavMeshPath path = new NavMeshPath();
        while (!_getCorrectPoint)
        {
            /*target = animator.transform.position + 2 * (animator.transform.position - _playerTransform.position);
            _agent.CalculatePath(target, path);
            if (path.status == NavMeshPathStatus.PathComplete)
                _getCorrectPoint = true;
            else*/
            {
                NavMesh.SamplePosition(animator.transform.position + Random.insideUnitSphere * escapeDistance,
                    out var hit, escapeDistance, NavMesh.AllAreas);
                target = hit.position;
                _agent.CalculatePath(target, path);
                if (path.status == NavMeshPathStatus.PathComplete)
                    _getCorrectPoint = true;
            }
        }
        _agent.SetDestination(target);
        _isEscaping = true;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
