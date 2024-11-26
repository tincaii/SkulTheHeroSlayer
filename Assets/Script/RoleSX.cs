using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleSX : MonoBehaviour
{
    public int HP; //生命值
    [HideInInspector] public int CurHP; //当前生命值
    public int MP; //魔法值
    [HideInInspector] public int CurMP; //当前魔法值
    public int AT; //攻击力
    public float MoveSpeed; //移动速度
    virtual public void Awake()
    {
        CurHP = HP;
        CurMP = MP;
    }
}
