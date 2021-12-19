using UnityEngine;

namespace Enemies.Scripts
{
    public class StartCoolDown : StateMachineBehaviour
    {
        public string skillName;
        private CoolDown _coolDown;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(skillName, false);
            _coolDown = animator.GetComponent<CoolDown>();
            _coolDown.coolDown = true;
        }
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
    }
}
