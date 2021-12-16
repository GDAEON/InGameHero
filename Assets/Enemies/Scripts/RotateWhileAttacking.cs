using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RotateWhileAttacking : MonoBehaviour
{
    private Animator _animator;
    private Transform _playerTransform;
    public float rotationSpeed;
    private void Start()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_animator.GetBool("Attack"))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                Quaternion.LookRotation(_playerTransform.position - transform.position),
                rotationSpeed * Time.deltaTime);
        }
    }
}
