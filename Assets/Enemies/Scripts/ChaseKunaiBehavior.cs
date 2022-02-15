using UnityEngine;
using UnityEngine.AI;

public class ChaseKunaiBehavior : StateMachineBehaviour
{
    private Transform _playerTransform;
    private NavMeshAgent _agent;
    private float _distance;
    public float attackRange;
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Speed = Animator.StringToHash("Speed");

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(GameObject.FindWithTag("Player"))
            _playerTransform = GameObject.FindWithTag("Player").transform;
        _agent = animator.GetComponentInParent<NavMeshAgent>();
        _agent.stoppingDistance = 5;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_playerTransform)
        {
            var playerPosition = _playerTransform.position;
            _distance = Vector3.Distance(animator.transform.position, playerPosition);
            animator.SetFloat("DistanceToPlayer", _distance);
            if (_distance > attackRange)
                _agent.SetDestination(playerPosition);
            if (_distance <= attackRange && _distance > 3)
            {
                animator.SetBool(Attack, true);
            }
        }
        animator.SetFloat(Speed, _agent.velocity.magnitude);
    }
}
