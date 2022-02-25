using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTargetsController : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform startCapsulePos;
    [SerializeField] private Transform endCapsulePos;
    [SerializeField] private float capsuleRadius;
    [SerializeField] private LayerMask targetsMask;
    [SerializeField] private BossLegsTargets target;
    private Ray _ray;
    private RaycastHit _hit;
    void FixedUpdate()
    {
        _ray = new Ray(transform.position + new Vector3(0, 5, 0), Vector3.down);
        if (Physics.Raycast(_ray, out _hit, 20f,  groundMask, QueryTriggerInteraction.Ignore))
        {
            Debug.DrawLine(_ray.origin, _hit.point, Color.green);
            transform.position = _hit.point;
        }

        if (!Physics.CheckCapsule(startCapsulePos.position, endCapsulePos.position, capsuleRadius, targetsMask, QueryTriggerInteraction.Collide))
        {
            target.SetTargetPose();
            target.step = true;
        }
            
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(startCapsulePos.position, capsuleRadius);
        Gizmos.DrawWireSphere(endCapsulePos.position, capsuleRadius);
    }
}
