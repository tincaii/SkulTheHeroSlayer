using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role_Reborn : Act//角色复活动画
{
    protected RoleCtrl role;
    public Role_Reborn(RoleCtrl role)
    {
        this.role = role;
    }
    public override void Enter() //行为进入时的逻辑
    {
        role.Is_ChangeAct = false;//设置角色不能切换行为
    }
    public override void Init() //初始化行为
    { 
        
    }
    public override void Run() //行为运行逻辑
    { 
        Debug.Log($"正在运行 Reborn");
    }
    public override void End() //行为结束逻辑
    { 
        role.Is_ChangeAct = true;//设置角色能切换行为
    }
}
