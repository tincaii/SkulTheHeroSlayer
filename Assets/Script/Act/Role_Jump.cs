using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role_Jump : Act //角色的跳跃
{
    protected RoleCtrl role;

    public Role_Jump(RoleCtrl role)
    {
        this.role = role;
    }
    public override void Enter() //行为进入时的逻辑
    {
        role .sx.isGrounded=true;
        role.sx.Jumpingspeed=1.5f;// 跳跃力量的初始值
    }
    public override void Init() //初始化行为
    { 
        role.Ani.SetBool("Jump",true);
    }
    
    public override void Run() //行为运行逻辑
    { 
        Debug.Log($"正在运行 Jump");
        
    }
    public override void End() //行为结束逻辑
    { 
        role.Ani.SetBool("Jump",false);
    }

    public override void SkillFireKey(bool fire) //行为的按键
    {
        // // 在跳跃时，按 K 键触发跳跃
        // if (Input.GetKeyDown(KeyCode.K) && (role .sx.isGrounded || JumpCount < 2))
        // {
        //     if (JumpCount < 2) // 双重跳跃逻辑
        //     {
        //         role.rb.velocity = new Vector2(role.rb.velocity.x, 0); // 重置竖直速度
        //         role.rb.AddForce(Vector2.up * role.sx.Jumpingpower, ForceMode2D.Impulse); // 施加跳跃力
        //         JumpCount++; // 增加跳跃计数
        //     }
        //     role.Next = "Jump";
        // }
        
    } 

}
