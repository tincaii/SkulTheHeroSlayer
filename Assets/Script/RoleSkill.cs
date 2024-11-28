using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleSkill : MonoBehaviour//定义一个管理角色技能的类
{
    [Tooltip("定义技能各个数据")] public List<SkillKeyandimg> SkillKeyDataList;//使用Tooltip提供说明，在unity面板中显示提示
    public Dictionary<string, SkillKeyandimg> SkillKeyDataDic = new();//技能数据列表，用于存储技能基础信息

    //将数据存储到字典，键为技能名称
    public void InitSkillData()//初始化技能数据，将技能数据列表中的内容存储到字典中
    {
        foreach (var skill in SkillKeyDataList)//遍历技能数据列表
        {
            SkillKeyDataDic.Add(skill.name, new SkillKeyandimg//将技能名称作为键，技能信息作为值，添加到字典中
            {
                name = skill.name,//技能名称
                icon = skill.icon,//技能图标
                SkillTime = skill.SkillTime,//技能冷却时间
                AttackBox = skill.AttackBox,//技能攻击范围
                DepleteMP = skill.DepleteMP,//技能消耗的魔法值
                FirePoint = skill.FirePoint,//技能释放的起点
            });
        }
    }
}
