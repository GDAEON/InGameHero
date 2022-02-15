using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Scripts
{
    public class ChaseKunaiBehavior : StateMachineBehaviour
    {
        private Transform _playerTransform;
        private NavMeshAgent _agent;
        private float _distance;
        public float attackRange;
        public float avoidRange;
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int RangeToPlayer = Animator.StringToHash("RangeToPlayer");

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _playerTransform = GameObject.FindWithTag("Player").transform;
            _agent = animator.GetComponentInParent<NavMeshAgent>();
        }
    
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_playerTransform)
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
                        animator.SetBool(Attack, true);
                    }
                }
                animator.SetFloat(Speed, _agent.velocity.magnitude);
                animator.SetFloat(RangeToPlayer, _distance);
            }
            else
            {
                _playerTransform = GameObject.FindWithTag("Player").transform;
            }
        }
    
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
       
        }
    }
}
