using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Enemies.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;
using Random = System.Random;

namespace Player.Scripts
{
    public class Controller : MonoBehaviour
    {
        [Header("Player settings")] [SerializeField]
        private float moveSpeed;

        [SerializeField] private float cameraSensitivity;
        [SerializeField] private float gravity = 9.81f;
        [SerializeField] private float jumpPower;

        [Header("Fight settings")] [SerializeField]
        private float damage;

        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private Transform attackPoint;
        [SerializeField] private float attackRange;
        [SerializeField] private PlayerBar healthBar;
        [SerializeField] private PlayerBar staminaBar;
        [SerializeField] private TextMeshProUGUI timer;
        [SerializeField] private int timeToChangeBody;

        [Header("Bodies")] 
        [SerializeField] private GameObject[] playerPrefabs;
        [SerializeField] private GameObject[] enemyPrefabs;

        private List<string> _bodyTypes;

        private CharacterController _controller;
        private Camera _camera;
        private bool _mCharging;
        private bool _canAttack = true;
        private Vector2 _mRotation;
        private Vector2 _mLook;
        private Vector2 _mMove;
        private Vector3 _velocity = Vector3.zero;
        private Animator _animator;
        private static readonly int Agony = Animator.StringToHash("Agony");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int AttackTrigger = Animator.StringToHash("Attack");
        private static readonly int JumpTrigger = Animator.StringToHash("Jump");

        public void OnMove(InputAction.CallbackContext context)
        {
            _mMove = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started && _controller.isGrounded)
            {
                Jump();
            }
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            _mLook = context.ReadValue<Vector2>();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started && staminaBar.health >= 25 && _canAttack)
            {
                Attack();
                staminaBar.TakeDamage(25);
            }
        }

        public void OnAgony(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
                Transmit();
        }

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            
            _camera = GetComponentInChildren<Camera>();
            _controller = GetComponent<CharacterController>();

            // Lock cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _bodyTypes = enemyPrefabs
                .Select(prefab => prefab.tag.ToString()
                .Replace("Enemy", "")//All the magic filtration happens here
                .ToLower()).ToList();
        }
        private void Start()
        {
            timeToChangeBody = 20;
            StartCoroutine(ReduceTime());
        }

        public void Update()
        {

            if (healthBar.health <= 0)
                gameObject.GetComponent<EndGameScript>().SetupDeathScreen();
            Look(_mLook);
            if (_controller.isGrounded)
            {
                Move(_mMove);
            }
            ApplyGravity();
        }

        private IEnumerator ReduceTime()
        {
            while (timeToChangeBody > 0)
            {
                yield return new WaitForSeconds(1);
                timeToChangeBody -= 1;
                timer.text = timeToChangeBody.ToString();
            }

            healthBar.SetHealth(0);
        }

        private void ApplyGravity()
        {
            if (Math.Abs(_velocity.y - jumpPower) > 0.01f && _controller.isGrounded)
            {
                _velocity = Vector3.zero;
                return;
            }

            var move = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) *
                       new Vector3(_mMove.x, 0, _mMove.y);

            _velocity.x = move.x * moveSpeed;
            _velocity.z = move.z * moveSpeed;

            _velocity.y -= gravity * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);
        }

        private void Move(Vector2 direction)
        {
            if (direction.sqrMagnitude < 0.01)
                return;
            var scaledMoveSpeed = moveSpeed * Time.deltaTime;

            var move = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) *
                       new Vector3(direction.x, 0, direction.y);

            _controller.Move(move * (scaledMoveSpeed * Time.deltaTime));
        }

        private void Jump()
        {
            _animator.SetTrigger(JumpTrigger);
            
            _velocity.y = jumpPower;
            staminaBar.TakeDamage(15);
        }

        private void Look(Vector2 rotate)
        {
            if (rotate.sqrMagnitude < 0.01)
                return;
            var scaledRotateSpeed = cameraSensitivity * Time.deltaTime;
            _mRotation.y += rotate.x * scaledRotateSpeed;
            _mRotation.x = Mathf.Clamp(_mRotation.x - rotate.y * scaledRotateSpeed, -89, 89);
            _camera.transform.localEulerAngles = _mRotation;
        }

        private void Attack()
        {
            StartCoroutine(HandleAttackAnimation());
        }

        public void DealDamage()
        {
            // ReSharper disable once Unity.PreferNonAllocApi
            var hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);
            if(hitEnemies.Length > 0)
                GetComponent<AudioController>().DealDamageSound();
            foreach (var enemy in hitEnemies)
            {
                EnemyBar[] bars = enemy.GetComponentsInChildren<EnemyBar>();
                bars[0].SendMessage("TakeDamage", damage);
                bars[1].SendMessage("TakeDamage", damage * 3);
                enemy.GetComponent<Animator>().SetTrigger(Hit);
            }
        }

        private IEnumerator HandleAttackAnimation()
        {
            _canAttack = false;
            var random = new Random();
            _animator.SetInteger(AttackTrigger, random.Next(0, 3));
            yield return new WaitForEndOfFrame();
            var animationDuration = _animator.GetNextAnimatorClipInfo(0)[0].clip.length;
            _animator.SetInteger(AttackTrigger, -1);
            yield return new WaitForSeconds(animationDuration);
            _canAttack = true;
        }

        private void Transmit()
        {
            var animator = GetComponentInChildren<PostProcessVolume>().gameObject.GetComponent<Animator>();
            // ReSharper disable once Unity.PreferNonAllocApi
            var hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);
            if (hitEnemies.Length > 0)
            {
                var enemy = hitEnemies[0];
                EnemyBar[] bars = enemy.GetComponentsInChildren<EnemyBar>();
                if (bars[1].health <= 30 || bars[0].health <= 30)
                {
                    animator.SetTrigger(Agony);
                    StartCoroutine(SpawnBody(GetBodyType(enemy.tag), enemy.gameObject));
                }
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator SpawnBody(int body, GameObject enemy)
        {
            yield return new WaitForSeconds(1f);

            var enemyTransform = enemy.transform;
            var enemyPosition = enemyTransform.position;
            var playerTransform = transform;
            var playerPosition = playerTransform.position;
            
            GameObject newEnemy = Instantiate(enemyPrefabs[GetBodyType(gameObject.name)], playerPosition, playerTransform.rotation);

            var tmpHealth = GetComponentInChildren<PlayerBar>().prevHealth;
            
            yield return new WaitForEndOfFrame();
            
            newEnemy.GetComponentInChildren<EnemyBar>().SetHealth(tmpHealth);

            Instantiate(playerPrefabs[body], new Vector3(enemyPosition.x, enemyPosition.y + 1, enemyPosition.z), enemyTransform.rotation)
                .GetComponentInChildren<PlayerBar>()
                .prevHealth = enemy.GetComponentInChildren<EnemyBar>().health;
            Destroy(enemy);
            GameObject.FindWithTag("EnemyCounter").GetComponent<EnemyCounter>().enemies.Remove(enemy.GetComponent<EnemyController>());
            Destroy(gameObject);
        }

        private int GetBodyType(string s)
        {
            s = s.ToLower();
            foreach (var (item, index) in _bodyTypes.Select((item, index) => (item, index)))
            {
                if (s.Contains(item)) return index;
            }
            return 0;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}