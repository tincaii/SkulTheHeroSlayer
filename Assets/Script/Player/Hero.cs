using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : RoleCtrl //玩家控制角色
{
    // public Dictionary<string, Act> ActMap = new(); //行为字典
    // protected Act CurAct = null; //当前角色行为
    // [HideInInspector] public string Next = null;//角色下一个行为
    // public Animator Ani;//角色动画
    // public RoleSX sx; //角色属性
    // [HideInInspector] public bool Is_ChangeAct = true; //是否可以切换行为
    // public Dictionary<string, SkillKeyandimg> SkillDataDic = new(); //角色技能列表
    // [Header("角色头像")] public Sprite Avatar;

    // virtual public void Awake()
    // {
    //     //获得技能数据
    //     var skillkeydata = GetComponent<RoleSkill>() ?? null;
    //     skillkeydata?.InitSkillData();
    //     //加载行为
    //     InitAct();
    // }
    // virtual public void Start()
    // {
    //     //注册动画事件
    //     RegistAnimEvent();
    // }

    // virtual public void Update()
    // {
    //     CurAct.Run();
    //     //更新行为
    //     Behavior_updata();
    //     //按键切换行为
    //     KeySwitchAct();
    //     //更新冷却时间
    //     UpdateCoolingTime();
    //     //检查是否死亡
    //     CheckHP();
    // }

    public int JumpCount = 0; // 跳跃计数器，记录当前跳跃次数

    public override void InitAct() 
    {
        Act act;

        act = new Role_Reborn(this);
        act.Enter();
        ActMap.Add("Reborn", act);
        
        act.Init();
        CurAct = act; //该动作初始化动作

        act = new Role_idle(this);
        act.Enter();
        ActMap.Add("idle", act);

        act = new Role_Move(this);
        act.Enter();
        ActMap.Add("Move", act);

        act = new Player_Attack(this);
        act.Enter();
        ActMap.Add("Attack", act);

        act = new Player_Jump(this);
        act.Enter();
        ActMap.Add("Jump", act);

        act = new Role_Fall(this);
        act.Enter();
        ActMap.Add("Fall", act);
    }




    // 当角色与地面发生碰撞时
    void OnCollisionStay2D(Collision2D collision)
    {
        // 判断与地面发生碰撞
    if (collision.gameObject.CompareTag("Ground"))
    {

        // 遍历碰撞接触点，检查是否有接触点在底部
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0) // 只有接触点的法线朝上的才是底部接触
            {
                Ani.SetBool("DropJump",true);
                sx.isGrounded = true; // 角色在地面
                if (JumpCount > 0)
                {
                    JumpCount = 0; // 重置跳跃计数
                }
                Debug.Log("角色与地面发生碰撞");
                break; // 找到底部接触点后就退出循环
            }
        }
    }
    }
    // 当角色离开地面时
    void OnCollisionExit2D(Collision2D collision)
    {
        // 离开地面时，设置为不在地面上
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("角色离开地面");
            Ani.SetBool("DropJump",false);
            sx.isGrounded = false;// 角色在空中
        }
    }
    private void FixedUpdate() 
    {
        // 检查是否开始下落（竖直速度为负）
        if (rb.velocity.y < 0 && !Ani.GetBool("Fall"))
        {
            Ani.SetBool("DropJump",true);
            sx.isDropJump=true;//开始下落
            Next = "Fall"; // 切换到下落状态
        }
    }

}
