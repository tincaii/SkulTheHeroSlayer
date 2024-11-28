using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleSX : MonoBehaviour//角色属性类
{
    public int HP; //生命值
    [HideInInspector] public int CurHP; //当前生命值
    public int MP; //魔法值
    [HideInInspector] public int CurMP; //当前魔法值
    public int AT; //攻击力
    public float MoveSpeed; //移动速度
    virtual public void Awake()//Awake是Unity 的生命周期函数，在对象被创建时自动调用
    {
        CurHP = HP;//初始化当前生命值为最大生命值
        CurMP = MP;//初始化当前魔法值为最大魔法值
    }
}
