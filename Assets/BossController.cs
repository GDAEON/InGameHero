using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private bool _movementCheck = true;
    private Vector3 _initialPosition;
    [SerializeField] private List<BossLegsTargets> targets;

    void FixedUpdate()
    {
        if (_movementCheck)
        {
            _initialPosition = transform.position;
            StartCoroutine(MovementCheck());
            _movementCheck = false;
        }
    }
    
    IEnumerator MovementCheck()
    {
        yield return new WaitForFixedUpdate();
        if (transform.position == _initialPosition)
        {
            yield return StartCoroutine(FixedCheck());
        }
        _movementCheck = true;
    }

    IEnumerator FixedCheck()
    {
        yield return new WaitForSeconds(1);
        if (transform.position == _initialPosition)
        {
            foreach (var target in targets)
            {
                target.ResetLegs();
            }
        }
    }
}
