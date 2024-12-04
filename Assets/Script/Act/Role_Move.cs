using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Role_Move : Act //角色移动行为
{
    protected RoleCtrl role;
    public Role_Move(RoleCtrl role)
    {
        this.role = role;
    }

    public override void Enter()
    {

    }
    public override void Init()
    {
        role.Ani.SetBool("Move", true);
        role.Ani.SetBool("idle", false);

    }
    public override void Run()
    {
        Debug.Log($"正在运行 Move");
        var movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));//GetAxisRaw获取水平和垂直方向的输入
        var pos = role.transform.position;// 角色位置更新
        pos.x += movement.normalized.x * role.sx.MoveSpeed * Time.deltaTime;// 使用 movement.x 直接移动
        role.transform.position = pos;
        if ((movement.x < 0 && role.transform.localScale.x > 0) || (movement.x > 0 && role.transform.localScale.x < 0))// 控制角色朝向
        {
            // 只反转 x 轴方向，保持 y 轴不变
            var Scale = role.transform.localScale;
            Scale.x *= -1; 
            role.transform.localScale = Scale;
        }
        if (movement.sqrMagnitude == 0)
        {
            role.Next = "idle";
        }
    }
    public override void End()
    {

    }
    public override void SkillFireKey(bool fire) //行为的按键
    {
        if(role.sx.isGrounded==true)//当玩家和地面碰撞的时候
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) 
            {
                role.Next = "Move";
            }
        }
        else if(role.sx.isGrounded==false)//当玩家离开地面的时候
        {
            role.Next="Fall";
        }

    } 
}
