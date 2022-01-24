using Enemies.Scripts;
using UnityEngine;

namespace Enemies.Enemies.Boss.Scripts
{
    public class Shield : StateMachineBehaviour
    {
        [SerializeField] private GameObject[] enemies;
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.transform.Find("EnergyShield").gameObject.SetActive(true);
            
            for (var i = 0; i < 3; i++)
            {
                var enemyNumber = Random.Range(0, 2);
                Instantiate(enemies[enemyNumber]);
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (FindObjectsOfType<EnemyController>().Length == 0)
            {
                Debug.Log("Нужно опускать щиты");
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }
    }
}
