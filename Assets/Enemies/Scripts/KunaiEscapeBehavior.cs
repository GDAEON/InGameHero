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
        if(GameObject.FindWithTag("Player"))
            _playerTransform = GameObject.FindWithTag("Player").transform;
        _agent = animator.GetComponentInParent<NavMeshAgent>();
        _agent.stoppingDistance = 1;
        GoToRandomPoint(animator);
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_playerTransform)
        {
            _distance = Vector3.Distance(animator.transform.position, _playerTransform.position);
            animator.SetFloat("DistanceToPlayer", _distance);
            if (Vector3.Distance(target, animator.transform.position) <= 1.1f)
                _isEscaping = false;
            if (_distance < escapeDistance && !_isEscaping)
            {
                GoToRandomPoint(animator);
            }
        }

        animator.SetFloat(Speed, _agent.velocity.magnitude);
    }

    private void GoToRandomPoint(Animator animator)
    {
        _getCorrectPoint = false;
        NavMeshPath path = new NavMeshPath();
        while (!_getCorrectPoint)
        {
            NavMesh.SamplePosition(animator.transform.position + Random.insideUnitSphere * escapeDistance,
                out var hit, escapeDistance, NavMesh.AllAreas);
            target = hit.position;
            _agent.CalculatePath(target, path);
            if (path.status == NavMeshPathStatus.PathComplete)
                _getCorrectPoint = true;
        }
        _agent.SetDestination(target);
        _isEscaping = true;
    }
}
