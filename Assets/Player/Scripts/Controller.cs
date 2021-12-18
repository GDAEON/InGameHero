using System;
using System.Collections;
using Enemies.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;

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

        [Header("Bodies")] [SerializeField] private GameObject[] bodiesPrefabs;


        private CharacterController _controller;
        private Camera _camera;
        private bool _mCharging;
        private Vector2 _mRotation;
        private Vector2 _mLook;
        private Vector2 _mMove;
        private Vector3 _velocity = Vector3.zero;
        private static readonly int Agony = Animator.StringToHash("Agony");

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
            if (context.phase == InputActionPhase.Started && staminaBar.health >= 25)
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

        private void Start()
        {
            _camera = GetComponentInChildren<Camera>();
            _controller = GetComponent<CharacterController>();

            // Lock cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


        public void Update()
        {
            Look(_mLook);
            if (_controller.isGrounded)
            {
                Move(_mMove);
            }

            ApplyGravity();
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
            // ReSharper disable once Unity.PreferNonAllocApi
            var hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);
            foreach (var enemy in hitEnemies)
            {
                enemy.GetComponentInChildren<EnemyBar>().SendMessage("TakeDamage", damage);
                enemy.GetComponent<Animator>().SetTrigger("Hit");
            }
        }

        private void Transmit()
        {
            var animator = GetComponentInChildren<PostProcessVolume>().gameObject.GetComponent<Animator>();
            // ReSharper disable once Unity.PreferNonAllocApi
            var hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);
            foreach (var enemy in hitEnemies)
            {
                if (enemy.CompareTag("DefaultEnemy"))
                {
                    animator.SetTrigger(Agony);
                    StartCoroutine(SpawnBody(0, enemy.gameObject));
                }
                else if (enemy.CompareTag("KunaiEnemy"))
                {
                    animator.SetTrigger(Agony);
                    StartCoroutine(SpawnBody(1, enemy.gameObject));
                }
                else if (enemy.CompareTag("TankEnemy"))
                {
                    animator.SetTrigger(Agony);
                    StartCoroutine(SpawnBody(2, enemy.gameObject));
                }
            }
        }

        private IEnumerator SpawnBody(int body, GameObject enemy)
        {
            yield return new WaitForSeconds(1f);
            var enemyTransform = enemy.transform;
            var enemyPosition = enemyTransform.position;
            Instantiate(bodiesPrefabs[body], new Vector3(enemyPosition.x, enemyPosition.y + 1, enemyPosition.z),
                enemyTransform.rotation);
            Destroy(gameObject);

            // TODO add death
            Destroy(enemy);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}