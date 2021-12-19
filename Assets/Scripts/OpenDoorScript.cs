using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorScript : MonoBehaviour
{
    private bool trigger = false;

    private void Update()
    {
        if (trigger)
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position,
                gameObject.transform.position + new Vector3(0, 0, -5), 2 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            trigger = true;
    }
}
