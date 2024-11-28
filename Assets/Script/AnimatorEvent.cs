using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvent : MonoBehaviour
{
    public event Action FireEvent;//攻击事件
    public event Action OnActToidle;//行为切换为idle


    private void EvetFire()//动画攻击事件触发
    {
        FireEvent?.Invoke();//如果有订阅者，触发FireEvent事件

    }
    private void EventActToidle()//动画切换为idle触发
    {
        OnActToidle?.Invoke();//如果有订阅者，触发EventActToidle事件
    }
}
