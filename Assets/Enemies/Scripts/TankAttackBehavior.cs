using UnityEngine;

namespace Enemies.Scripts
{
    public class TankAttackBehavior : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.GetComponent<EnemyController>().attackRange = 0.2f;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.GetComponent<EnemyController>().attackRange = 0.4f;
        }
    }
}
