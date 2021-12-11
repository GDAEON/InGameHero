using Player.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Enemies.Scripts
{
    [RequireComponent(typeof(Slider))]
    public class EnemyBar : Healthbar
    {
        private Camera _camera;
        
        private void Awake()
        {
            _camera = Camera.main;
        }

        private void FixedUpdate()
        {
            transform.LookAt(_camera.transform, Vector3.down);
        }
    }
}