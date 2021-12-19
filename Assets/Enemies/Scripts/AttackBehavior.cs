using UnityEngine;

namespace Enemies.Scripts
{
    public class AttackBehavior : StateMachineBehaviour
    {
        private Transform _playerTransform;
        private static readonly int Attack = Animator.StringToHash("Attack");

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _playerTransform = GameObject.FindWithTag("Player").transform;
            var animatorTransform = animator.transform;
            animator.rootRotation = Quaternion.RotateTowards(animatorTransform.rotation,
                Quaternion.LookRotation(_playerTransform.position - animatorTransform.position),
                180f);
        }
    
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(Attack, false);
        }
    }
}
