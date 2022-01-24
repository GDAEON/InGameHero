using Enemies.Scripts;
using UnityEngine;

namespace Enemies.Enemies.Boss.Scripts
{
    public class Shield : StateMachineBehaviour
    {
        [SerializeField] private GameObject[] enemies;
        private static readonly int LowerTheShields = Animator.StringToHash("LowerTheShields");
        [SerializeField] private Vector3 startPoint;
        private GameObject _player;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.ResetTrigger(LowerTheShields);
            
            Transform transform;
            (transform = animator.transform).Find("EnergyShield").gameObject.SetActive(true);

            var position = transform.position;
            var x = position.x;
            var y = position.y;
            var z = position.z;
            
            for (var i = 0; i < 3; i++)
            {
                var enemyNumber = Random.Range(0, 3);
                var randomX = Random.Range(-5, 5);
                var randomZ = Random.Range(-5, 5);
                Instantiate(enemies[enemyNumber], new Vector3(x + randomX, y, z + randomZ), Quaternion.identity);
            }
            
            // Move player out of shield
            var player = GameObject.FindWithTag("Player");
            player.transform.position = startPoint;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var aliveEnemies = FindObjectsOfType<EnemyController>();
            if (aliveEnemies.Length == 1 && aliveEnemies[0].isDead)
            {
                animator.SetTrigger(LowerTheShields);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }
    }
}
