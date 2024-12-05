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

    }
    public override void Init() //初始化行为
    { 
        role.Ani.SetBool("Fall",true);
    }
    public override void Run() //行为运行逻辑
    { 
        Debug.Log($"正在运行 Fall");
        var movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));//GetAxisRaw
        var pos = role.transform.position;
        pos.x += movement.normalized.x * role.sx.MoveSpeed * Time.deltaTime;
        role.transform.position = pos;
        if ((movement.x < 0 && role.transform.localScale.x > 0) || (movement.x > 0 && role.transform.localScale.x < 0))
        {
            var Scale = role.transform.localScale;
            Scale.x *= -1;
            role.transform.localScale = Scale;
        }
        if(role.sx.isGrounded==true)
        {
            role.Next="idle";
        }
        
    }
    public override void End() //行为结束逻辑
    { 
        role.Ani.SetBool("Fall",false);
    }

    public override void SkillFireKey(bool fire) //行为的按键
    {
        
        if((Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.D))&&role.sx.isDropJump==false)
        {
            role.Next="Fall";
        } 
    }
}
