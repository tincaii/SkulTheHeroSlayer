using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role_Fall : Act //角色的下落
{
    protected RoleCtrl role;
    public Role_Fall(RoleCtrl role)
    {
        this.role = role;
    }
    public override void Enter() //行为进入时的逻辑
    {
        role.Ani.SetBool("Fall",true);
    }
    public override void Init() //初始化行为
    { 
        
    }
    public override void Run() //行为运行逻辑
    { 
        Debug.Log($"正在运行 Fall");
    }
    public override void End() //行为结束逻辑
    { 
        role.Ani.SetBool("Fall",false);
    }

}
