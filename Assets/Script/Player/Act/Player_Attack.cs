using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player_Attack : SkillAct //英雄的攻击行为
{

    protected RoleCtrl role;
    public Player_Attack(RoleCtrl role)
    {
        this.role = role;
    }


    public override void Enter() 
    {
        InitAttackBoxEventAndSkill(ref role, "Attack");
        
    }
    public override void Init() 
    {
        allbox.gameObject.SetActive(true);
        role.Ani.SetTrigger("Attack1");
        role.Is_ChangeAct = false;
    }
    public override void Run() 
    {


        Debug.Log($"正在运行 Attack1");
    }
    public override void End() 
    { 
        allbox.gameObject.SetActive(false);
        allbox.CloseAllBoxes();

    }
    public override void Fire() //技能攻击逻辑
    {
        var box = allbox.AttackBoxList.FirstOrDefault();
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
