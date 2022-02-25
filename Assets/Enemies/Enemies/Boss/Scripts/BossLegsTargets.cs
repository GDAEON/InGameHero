using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLegsTargets : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject body;

    public bool step;
    private bool _halfStep = false;
    private Vector3 _targetPos;
    
    public float stepSpeed = 0.1f;
    public Vector3 offset;
    


    public void Start()
    {
        ResetLegs();
    }

    
    public void FixedUpdate()
    {
        transform.rotation = body.transform.rotation;
        if (!step)
            SetTargetPose();
        else
        {
            transform.position = Vector3.Lerp(transform.position, _targetPos, stepSpeed);
            if (Vector3.Distance(gameObject.transform.position, _targetPos) < 0.1f)
            {
                step = false;
            }
        }
    }

    public void SetTargetPose()
    {
        _targetPos = target.transform.position;
    }
    public void ResetLegs()
    {
        step = false;
        StartCoroutine(ResetLegPosition());
    }

    IEnumerator ResetLegPosition()
    {
        yield return new WaitForFixedUpdate();
        _targetPos = target.transform.position + offset;
        step = true;
    }
}
