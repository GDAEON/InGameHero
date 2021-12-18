using System.Collections;
using System.Collections.Generic;
using Player.Scripts;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool _isDead;
    private Healthbar _healthbar;
    private Animator _animator;
    public Material material;
    [Header("Fight settings")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private List<Transform> attackPoint;
    public float attackRange;
    void Start()
    {
        _healthbar = GetComponentInChildren<Healthbar>();
        _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (_healthbar.health == 0 && !_isDead)
        {
            GetComponent<Collider>().enabled = false;
            _animator.SetTrigger("Death");
            _isDead = true;
            StartCoroutine(nameof(DeathTimer));
        }
    }

    public void Attack(int damage)
    {
        foreach (var point in attackPoint)
        {
            var hitPlayer = Physics.OverlapSphere(point.position, attackRange, playerLayer);
            foreach (var player in hitPlayer)
            {
                Debug.Log("Attack invoke, player name = " + player.name);
                player.GetComponentInChildren<PlayerBar>().SendMessage("TakeDamage", damage);
            }
        }
    }
    
    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
    
    private void OnDrawGizmosSelected()
    {
        foreach (var point in attackPoint)
        {
            Gizmos.DrawWireSphere(point.position, attackRange);
        }
    }
}
