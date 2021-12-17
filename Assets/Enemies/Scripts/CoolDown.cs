using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDown : MonoBehaviour
{
    public string skillName;
    public float coolDownTime;
    private bool _coolDown;
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        _coolDown = !_animator.GetBool(skillName);
        if (_coolDown)
            StartCoroutine(nameof(TimerTake));
    }
    
    private IEnumerator TimerTake()
    {
        yield return new WaitForSeconds(coolDownTime);
        _coolDown = false;
        _animator.SetBool(skillName, true);
    }
}
