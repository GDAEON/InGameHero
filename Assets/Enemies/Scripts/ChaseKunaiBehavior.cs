using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemies.Scripts
{
    public class ChaseKunaiBehavior : StateMachineBehaviour
    {
        private Transform _playerTransform;
        private NavMeshAgent _agent;
        private float _distance;
        public float attackRange;
        public float avoidRange;
        public float escapeRange;
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
                    
                }

                if (_distance < escapeRange)
                {
                    NavMeshPath path = new NavMeshPath();
                    Vector3 target = new Vector3();
                    while (path.status != NavMeshPathStatus.PathComplete)
                    {
                        double angle = Random.Range(0, 359);
                        double targetX = 5 * Math.Cos(angle);
                        double targetY = 5 * Math.Sin(angle);
                        var position = animator.transform.position;
                        target = new Vector3(Convert.ToSingle(position.x + targetX),
                            Convert.ToSingle(position.y + targetY), position.z);
                        _agent.CalculatePath(target, path);
                        
                    }
                    _agent.destination = target;
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
