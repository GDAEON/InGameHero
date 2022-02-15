using System;
using System.Collections;
using System.Collections.Generic;
using Enemies.Scripts;
using Player.Scripts;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    public List<EnemyController> enemies;

    private void Update()
    {
        if (enemies.Count == 0)
        {
            GameObject.FindWithTag("Player").GetComponent<EndGameScript>().SetupWinScreen();
        }
    }
}
