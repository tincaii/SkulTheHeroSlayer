using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllAttackBox : MonoBehaviour
{
    [Header("攻击盒子")] public List<GameObject> AttackBoxList;
    [Header("特效")] public List<GameObject> EffectList;

    //动作被打断，又可能来不急关闭全部攻击盒子，手动掉用
    public void CloseAllBoxes()
    {
        foreach (var box in AttackBoxList)
        {
            box.SetActive(false);
        }
    }

}
