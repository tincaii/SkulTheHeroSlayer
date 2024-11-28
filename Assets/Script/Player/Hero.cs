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

    public override void InitAct() 
    {
        Act act;

        act = new Role_Reborn(this);
        act.Enter();
        ActMap.Add("Reborn", act);

        act = new Role_idle(this);
        act.Enter();
        ActMap.Add("idle", act);

        act.Init();
        CurAct = act; //该动作初始化动作

        act = new Role_Move(this);
        act.Enter();
        ActMap.Add("Move", act);

        act = new Player_Attack(this);
        act.Enter();
        ActMap.Add("Attack", act);


    }

}
