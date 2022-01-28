using System;
using System.Collections;
using Enemies.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;
using Random = System.Random;

public class PlayerControllerFirst : MonoBehaviour
{
    [Header("Player settings")] [SerializeField]
    private float moveSpeed;

    [SerializeField] private float cameraSensitivity;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float jumpPower;
    
    private CharacterController _controller;
    private Camera _camera;
    private Vector2 _mRotation;
    private Vector2 _mLook;
    private Vector2 _mMove;
    private Vector3 _velocity = Vector3.zero;

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

    private void Awake()
    {
        _camera = GetComponentInChildren<Camera>();
        _controller = GetComponent<CharacterController>();
        
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
}
