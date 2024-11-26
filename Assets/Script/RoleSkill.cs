using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleSkill : MonoBehaviour
{
    [Tooltip("定义技能各个数据")] public List<SkillKeyandimg> SkillKeyDataList;
    public Dictionary<string, SkillKeyandimg> SkillKeyDataDic = new();

    //将数据存储到字典
    public void InitSkillData()
    {
        foreach (var skill in SkillKeyDataList)
        {
            SkillKeyDataDic.Add(skill.name, new SkillKeyandimg
            {
                name = skill.name,
                icon = skill.icon,
                SkillTime = skill.SkillTime,
                AttackBox = skill.AttackBox,
                DepleteMP = skill.DepleteMP,
                FirePoint = skill.FirePoint,
            });
        }
    }
}
