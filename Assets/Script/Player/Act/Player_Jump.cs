using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Jump : Role_Jump //英雄的跳跃行为
{
    protected Hero hero;
    public Player_Jump(RoleCtrl role) : base(role)
    {
        hero = role as Hero;
    }

    public override void SkillFireKey(bool fire) //行为的按键
    {
        // 在跳跃时，按 K 键触发跳跃
        if (Input.GetKeyDown(KeyCode.K) && (role .sx.isGrounded || hero.JumpCount < 1)) 
        {
            role.sx.isDropJump=false;//开始跳跃
            if (hero.JumpCount < 1) // 双重跳跃逻辑
            {
                role.rb.velocity = new Vector2(role.rb.velocity.x, 0); // 重置竖直速度
                role.rb.AddForce(Vector2.up * role.sx.Jumpingspeed, ForceMode2D.Impulse); // 施加跳跃力
                hero.JumpCount++; // 增加跳跃计数
            }
            role.Next = "Jump";
        } 
        
    }

}
