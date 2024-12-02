using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role_Jump : Act //角色的跳跃
{
    protected RoleCtrl role;
    private int JumpCount = 0; // 跳跃计数器，记录当前跳跃次数
    public Role_Jump(RoleCtrl role)
    {
        this.role = role;
    }
    public override void Enter() //行为进入时的逻辑
    {
        role .sx.isGrounded=true;
        role.sx.Jumpingpower=1f;// 跳跃力量的初始值
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
        // 在跳跃时，按 K 键触发跳跃
        if (Input.GetKeyDown(KeyCode.K) && (role .sx.isGrounded || JumpCount < 2))
        {
            if (JumpCount < 2) // 双重跳跃逻辑
            {
                role.rb.velocity = new Vector2(role.rb.velocity.x, 0); // 重置竖直速度
                role.rb.AddForce(Vector2.up * role.sx.Jumpingpower, ForceMode2D.Impulse); // 施加跳跃力
                JumpCount++; // 增加跳跃计数
            }
            role.Next = "Jump";
        }

    } 

    // 当角色与地面发生碰撞时
    void OnCollisionStay2D(Collision2D collision)
    {
        // 检测角色是否与地面发生接触
        if (collision.gameObject.CompareTag("Ground"))
        {
            role .sx.isGrounded = true; // 角色与地面接触，设置为在地面上
            if(JumpCount>0)
            {
                JumpCount = 0; // 重置跳跃计数
            }
            Debug.Log("角色与地面发生碰撞");
        }
    }
    // 当角色离开地面时
    void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("角色离开地面");
        // 离开地面时，设置为不在地面上
        if (collision.gameObject.CompareTag("Ground"))
        {
            role .sx.isGrounded = false;
        }
    }
}
