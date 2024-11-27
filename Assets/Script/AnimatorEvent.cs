using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvent : MonoBehaviour
{
    public event Action FireEvent;//攻击
    public event Action OnActToidle;


    private void EvetFire()
    {
        FireEvent?.Invoke();

    }
    private void EventActToidle()
    {
        OnActToidle?.Invoke();
    }
}
