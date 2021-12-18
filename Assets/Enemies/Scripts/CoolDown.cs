using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDown : MonoBehaviour
{
    public string skillName;
    public float coolDownTime;
    private Animator _animator;
    [HideInInspector]
    public bool coolDown;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (coolDown)
            StartCoroutine(nameof(TimerTake));
    }
    
    private IEnumerator TimerTake()
    {
        coolDown = false;
        yield return new WaitForSeconds(coolDownTime);
        _animator.SetBool(skillName, true);
    }
}
