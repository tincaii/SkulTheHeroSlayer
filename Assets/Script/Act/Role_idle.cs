using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role_idle : Act //角色的idle
{
    protected RoleCtrl role;
    public Role_idle(RoleCtrl role) 
    {
        this.role = role;
    }

    public override void Enter() 
    {
        
    }
    public override void Init() 
    {
        if(role.sx.isGrounded==true)
        {
            role.Ani.SetBool("idle", true);
        }

        role.Ani.SetBool("Move", false);

    }
    public override void Run() 
    {
        Debug.Log($"正在运行 idle");
    }
    public override void End() 
    { 
        role.Ani.SetBool("idle", false);
    }

}
