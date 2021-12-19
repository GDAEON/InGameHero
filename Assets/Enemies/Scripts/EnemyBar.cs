using UnityEngine;
using UnityEngine.UI;

namespace Enemies.Scripts
{
    [RequireComponent(typeof(Slider))]
    public class EnemyBar : Healthbar
    {
        private Camera _camera;

        private void FixedUpdate()
        {
            _camera = Camera.main;
            if(_camera)
                transform.LookAt(_camera.transform, Vector3.down);
        }
    }
}