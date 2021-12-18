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
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
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
        var hitPlayer = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);
        foreach (var player in hitPlayer)
        {
            Debug.Log("Attack invoke, player name = " + player.name);
            player.GetComponentInChildren<PlayerBar>().SendMessage("TakeDamage", damage);
        }
    }
    
    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
