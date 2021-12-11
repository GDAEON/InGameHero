using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Scripts
{
    public class Controller : MonoBehaviour
    {
        [Header("Player settings")]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float cameraSensitivity;

        private bool _mCharging;
        private Vector2 _mRotation;
        private Vector2 _mLook;
        private Vector2 _mMove;

        public void OnMove(InputAction.CallbackContext context)
        {
            _mMove = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            _mLook = context.ReadValue<Vector2>();
        }

        private void Start()
        {
            // Lock cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


        public void Update()
        {
            Look(_mLook);
            Move(_mMove);
        }

        private void Move(Vector2 direction)
        {
            if (direction.sqrMagnitude < 0.01)
                return;
            var scaledMoveSpeed = moveSpeed * Time.deltaTime;
            
            var move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(direction.x, 0, direction.y);
            transform.position += move * scaledMoveSpeed;
        }

        private void Look(Vector2 rotate)
        {
            if (rotate.sqrMagnitude < 0.01)
                return;
            var scaledRotateSpeed = cameraSensitivity * Time.deltaTime;
            _mRotation.y += rotate.x * scaledRotateSpeed;
            _mRotation.x = Mathf.Clamp(_mRotation.x - rotate.y * scaledRotateSpeed, -89, 89);
            transform.localEulerAngles = _mRotation;
        }
    }
}
