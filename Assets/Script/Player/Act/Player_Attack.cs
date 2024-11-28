using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player_Attack : SkillAct //英雄的攻击行为
{

    protected RoleCtrl role;//角色控制器，用于访问角色数据和操作
    public Player_Attack(RoleCtrl role)//构造函数，接收角色控制器实例
    {
        this.role = role;
    }


    public override void Enter() //进入攻击状态时调用
    {
        InitAttackBoxEventAndSkill(ref role, "Attack");//初始化攻击盒子事件和技能
        
    }
    public override void Init() //初始化攻击行为
    {
        allbox.gameObject.SetActive(true);//激活攻击盒子
        role.Ani.SetTrigger("Attack1");// 播放"Attack1" 动画
        role.Is_ChangeAct = false;//设置角色不能切换行为
    }
    public override void Run() //攻击行为运行逻辑
    {


        Debug.Log($"正在运行 Attack1");
    }
    public override void End() //结束攻击行为时调用
    { 
        allbox.gameObject.SetActive(false);//关闭攻击盒子
        allbox.CloseAllBoxes();//关闭所有攻击盒子

    }
    public override void Fire() //技能攻击逻辑
    {
        var box = allbox.AttackBoxList.FirstOrDefault();//激活第一个攻击盒子
        box.SetActive(true);

    } 
    public override void SkillFireKey(bool fire) //行为的按键
    { 
        if(Input.GetKeyDown(KeyCode.J))
            role.Next = "Attack";
    }
    //攻击盒子逻辑
    protected override void AttackBoxHit_Enter(Collider2D other, int AT) 
    {
        //该普通攻击只攻击"角色"
        int roleLayer = LayerMask.NameToLayer("Role");
        if (other.gameObject.layer != roleLayer) return;

        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log($"我对{other.name}造成了{AT}点伤害");
        }
        
    }

}
