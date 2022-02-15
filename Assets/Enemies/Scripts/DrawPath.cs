using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DrawPath : MonoBehaviour
{
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        var agentPath = agent.path;
        Vector3 prevCorner = transform.position;
        foreach (var corner in agentPath.corners)
        {
            Gizmos.DrawLine(prevCorner, corner);
            Gizmos.DrawSphere(corner, 0.1f);
            prevCorner = corner;
        }
    }
}
