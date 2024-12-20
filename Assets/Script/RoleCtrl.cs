using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillKeyandimg //技能按钮功能以及图标
{
    public string name; //技能名称
    public Sprite icon; //技能图标
    public float SkillTime;//技能冷却时间
    public int DepleteMP;//消耗的能量
    public GameObject AttackBox;//技能攻击盒子
    public Transform FirePoint;//技能发送位置
}
public class Act //动作
{
    public virtual void Enter() { }//行为进入时的逻辑
    public virtual void Init() { }//初始化行为
    public virtual void Run() { }//行为运行逻辑
    public virtual void End() { }//行为结束逻辑
    public virtual void SkillFireKey(bool fire) { } //行为的按键
}
public class SkillAct : Act //技能行为
{
    public float CoolingTime; //冷却时间
    public float Cur_CoolingTime;//当前冷却时间
    public int DepleteMP;//消耗的能量
    public AllAttackBox allbox;//属于我这个行为的所有攻击盒子
    public virtual void Fire() { } //技能攻击逻辑
    public virtual void PlayEffect() { } //播放特效
    public virtual bool Is_CanActRun() { return true; } //该技能是否可以执行
    public virtual bool SkillDepleteMP(ref RoleSX sx) //技能消耗能量
    {
        if (sx.CurMP - DepleteMP < 0)//判断能量是都足够
            return false;
        else
        {
            sx.CurMP -= DepleteMP;//消耗能量
            return true;
        }
    }
    //注册攻击盒子事件和技能图标方法
    public virtual void InitAttackBoxEventAndSkill(ref RoleCtrl role, string act)
    {
        var skb = role.GetComponent<RoleSkill>();//获取角色上的RoleSkill组件，用于访问技能数据
        if (skb != null)
        {
            if (skb.SkillKeyDataDic.ContainsKey(act))//检查技能字典中是否包含指定技能名
            {
                var skillData = skb.SkillKeyDataDic[act];//获取指定技能的数据
                if (!role.SkillDataDic.ContainsKey(act))// 检查键是否已存在
                {
                    // 只在键不存在时添加
                    role.SkillDataDic.Add(act, new SkillKeyandimg //将技能数据注册到角色的SkillDataDic中，确保角色能访问该技能
                    {
                    name = skillData.name,//技能名
                    icon = skillData.icon,//技能图标
                    SkillTime = skillData.SkillTime,//技能冷却时间
                    DepleteMP = skillData.DepleteMP,//技能消耗的魔法值
                    FirePoint = skillData.FirePoint,//技能释放位置
                    AttackBox = skillData.AttackBox,//全部攻击盒子
                    });
                }
                
                //注册攻击盒子事件，并储存到allbox成员变量中
                allbox = skillData.AttackBox.GetComponent<AllAttackBox>();
                if (allbox != null)
                {
                    foreach (var box in allbox.AttackBoxList)//遍历攻击盒子列表，为每个盒子注册相关事件
                    {
                        //尝试获取攻击盒子的伤害事件组件
                        var boxhit = box.GetComponent<AttackBoxHit>();// ?? box.GetComponent<AttackBoxAnim>()?.attackBoxHit
                        if (boxhit != null)//如果获取成功，则为攻击事件注册回调方法
                        {
                            boxhit.attackBoxHit_Enter += AttackBoxHit_Enter;//攻击盒子单次伤害事件
                            //boxhit.PersistentTargetEvent += PersistentTargetEvent;//攻击盒子持续检测的目标
                            //boxhit.PersistentTargetHit += PersistentTargetEvent;//攻击盒子持续伤害事件
                            //boxhit.PersistentTargetEnd += PersistentTargetEnd;//攻击盒子持续伤害结束
                        }

                    }

                }

            }
        }
    }
    //攻击盒子逻辑
    protected virtual void AttackBoxHit_Enter(Collider2D other, int AT) { }
    //攻击盒子持续检测到的物体
    protected virtual void PersistentTargetEvent(Collider2D other, AttackBoxHit boxhit) { }
    //攻击盒子持续检测的伤害
    protected virtual void PersistentTargetEvent(Collider2D other, int AT) { }
    //攻击盒子持续盒子结束
    protected virtual void PersistentTargetEnd(AttackBoxHit boxHit) { }
}


public class RoleCtrl : MonoBehaviour //角色控制父类
{
    public Dictionary<string, Act> ActMap = new(); //行为字典
    protected Act CurAct = null; //当前角色行为
    [HideInInspector] public string Next = null;//角色下一个行为
    public Animator Ani;//角色动画
    public RoleSX sx; //角色属性
    public Rigidbody2D rb;
    [HideInInspector] public bool Is_ChangeAct = true; //是否可以切换行为
    public Dictionary<string, SkillKeyandimg> SkillDataDic = new(); //角色技能列表
    [Header("角色头像")] public Sprite Avatar;
    virtual public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();// 获取 Rigidbody2D 组件并赋值给 rb
        var skillkeydata = GetComponent<RoleSkill>() ?? null;//获得技能数据
        skillkeydata?.InitSkillData();
        //加载行为
        InitAct();
    }

    // Start is called before the first frame update
    virtual public void Start()
    {
        //注册动画事件
        RegistAnimEvent();
    }

    // Update is called once per frame
    virtual public void Update()
    {
        CurAct.Run();
        //更新行为
        Behavior_updata();
        //按键切换行为
        KeySwitchAct();
        //更新冷却时间
        UpdateCoolingTime();
        //检查是否死亡
        CheckHP();

    }
    //更新行为
    protected void Behavior_updata()
    {
        //如果不能切换行为，则不更新
        if (Is_ChangeAct == false)
        {
            Next = null;
            return;
        }

        if (Next != null)
        {
            CurAct.End();
            if (!ActMap.ContainsKey(Next))
            {
                Next = null;
                return;
            }
            CurAct = ActMap[Next];
            CurAct.Init();
            Next = null;
        }
    }
    //加载行为
    public virtual void InitAct() { }
    //释放技能按键
    public virtual void KeySwitchAct()
    {
        // //如果不能切换行为，则不更新
        // if (Is_ChangeAct == false)
        // {
        //     return;
        // }
        foreach (var act in ActMap)
        {
            var v = act.Value;
            v.SkillFireKey(false);//这里传入false必须通过按键才能触发
        }
    }
    //更新冷却时间
    protected virtual void UpdateCoolingTime()
    {
        // foreach (var act in ActMap)
        // {
        //     if (act.Value is not SkillAct v)
        //         continue;
        //     if (v.Cur_CoolingTime > 0)
        //         v.Cur_CoolingTime -= Time.deltaTime;
        // }
    }
    //检查是否死亡
    public virtual void CheckHP()
    {
        // //受击效果
        // img.DOColor(Color.red, 0.1f).OnComplete(() =>
        //     {
        //         img.DOColor(Color.white, 0.1f);
        //     });
        if (sx.CurHP <= 0)
        {
            Debug.LogError($"{name} 死亡");
            // Next = "Die";
            // GetComponent<Rigidbody>().isKinematic = true;
            // GetComponent<CapsuleCollider>().enabled = false;
        }

    }
    //注册动画事件
    public virtual void RegistAnimEvent()
    {
        var AnimEvent = Ani.GetComponent<AnimatorEvent>();
        AnimEvent.FireEvent += ActFire;
        AnimEvent.OnActToidle += ActToidle;
    }
    //攻击
    protected virtual void ActFire()
    {
        if (CurAct is SkillAct skillAct)
        {
            skillAct.Fire();
        }



    }
    //动画转换idle
    void ActToidle()
    {
        Next = "idle";
        Is_ChangeAct = true;
    }
}
