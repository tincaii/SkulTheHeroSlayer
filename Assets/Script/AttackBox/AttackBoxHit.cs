using System;
using UnityEngine;

public class AttackBoxHit : MonoBehaviour
{
    [Tooltip("攻击盒子伤害")] public int BoxAT;//攻击盒子的伤害值

    public event Action<Collider2D, int> attackBoxHit_Enter;//攻击盒子检测逻辑

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Invoke(nameof(CloseActive), 0.1f);//每帧调用一次CloseActive方法，延迟0.1秒关闭攻击盒子

    }

    //关闭攻击Box
    protected void CloseActive()//关闭攻击盒子
    {
        gameObject.SetActive(false);
    }

    protected void OnTriggerEnter2D(Collider2D other)//当碰撞体进入攻击盒子触发
    {
        attackBoxHit_Enter?.Invoke(other, BoxAT);
        Debug.Log($"{name}检测到{other.name} 进入");
    }

    private void OnTriggerExit2D(Collider2D other)//当碰撞体退出攻击盒子触发
    {
        Debug.Log($"检测到{other.name} 退出");
    }

    void OnEnable()
    {

    }
}
