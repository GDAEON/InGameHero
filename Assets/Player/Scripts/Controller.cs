using Enemies.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Scripts
{
    public class Controller : MonoBehaviour
    {
        [Header("Player settings")]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float cameraSensitivity;
        [SerializeField]private float gravity = 9.81f;
        [SerializeField]private float jumpPower;
        
        [Header("Fight settings")]
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private Transform attackPoint;
        [SerializeField] private float attackRange;
        [SerializeField] private Healthbar healthBar;
        [SerializeField] private Healthbar staminaBar;
        
        private CharacterController _controller;
        private Camera _camera;
        private bool _mCharging;
        private Vector2 _mRotation;
        private Vector2 _mLook;
        private Vector2 _mMove;
        private Vector3 _velocity = Vector3.zero;

        public void OnMove(InputAction.CallbackContext context)
        {
            if (_controller.isGrounded)
            {
                _mMove = context.ReadValue<Vector2>();
            }
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

        private void Start()
        {
            _camera = Camera.main;
            _controller = GetComponent<CharacterController>();

            // Lock cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


        public void Update()
        {
            Look(_mLook);
            Move(_mMove);
            
            //TODO fix jump
            
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
            
            _controller.Move(move * scaledMoveSpeed);
        }

        private void Jump()
        {
            _velocity.y = jumpPower;
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
                enemy.GetComponentInChildren<EnemyBar>().SendMessage("TakeDamage", 20f);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
