using UnityEngine;

namespace Enemies.Scripts
{
    public class TankJumpBehavior : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.GetComponent<EnemyController>().attackRange = 1.4f;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.GetComponent<EnemyController>().attackRange = 0.5f;
        }
    }
}
