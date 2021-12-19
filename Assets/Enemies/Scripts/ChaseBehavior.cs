using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Scripts
{
    public class ChaseBehavior : StateMachineBehaviour
    {
        private Transform _playerTransform;
        private NavMeshAgent _agent;
        public float attackRange;
        private static readonly int Chance = Animator.StringToHash("Chance");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int RangeToPlayer = Animator.StringToHash("RangeToPlayer");
        private static readonly int Attack = Animator.StringToHash("Attack");

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _playerTransform = GameObject.FindWithTag("Player").transform;
            _agent = animator.GetComponentInParent<NavMeshAgent>();
        }
    
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetInteger(Chance, Random.Range(0, 10));
            if (_playerTransform)
            {
                var playerPosition = _playerTransform.position;
                _agent.destination = playerPosition;
                animator.SetFloat(Speed, _agent.velocity.magnitude);
                animator.SetFloat(RangeToPlayer, Vector3.Distance(animator.transform.position, playerPosition));
                if (Vector3.Distance(animator.transform.position, _playerTransform.position) <= attackRange)
                {
                    animator.SetBool(Attack, true);
                }
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
