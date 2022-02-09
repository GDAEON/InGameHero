using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPosition : MonoBehaviour
{
    [SerializeField] private List<GameObject> targets;
    [SerializeField] private float height;
    private float _averagePosition;
    void FixedUpdate()
    {
        foreach (var target in targets)
        {
            _averagePosition += target.transform.position.y;
        }

        _averagePosition = _averagePosition / targets.Count;
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, _averagePosition + height, pos.z);
    }
}
