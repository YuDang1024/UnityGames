using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotGroup : MonoBehaviour
{
    public float runspeed;
    BotCharacter[] bots;
    public BotCharacter botPrefab;

    public enum Type { 
        None = -1,
        Normal,
        Slow,
    }

    // 怪物飞散
    public void GroupBlowout()
    {
        Vector3 blowout;           // 怪物飞散的方向，含速度
        float blowoutSpeed;         //速度
        float blowoutSpeedVary;     //速度变化调控

        blowoutSpeedVary = 10.0f;
        //=========================================


        //遍历每一个怪物，决定他们的飞散结果
        foreach (BotCharacter bot in bots)
        {
            blowoutSpeed = Random.Range(0.5f, 1.5f) * blowoutSpeedVary;
            blowout = Vector3.up * Random.Range(0, 3f) + Vector3.forward * Random.Range(-3f, 3f) + Vector3.right * Random.Range(-3f, 3f);
            blowout = blowout.normalized * Random.Range(0.5f, 1.5f) * blowoutSpeed;
            // 角速度
            //叉乘后得到的第三向量，与飞散方向和Y轴垂直，便是旋转怪物的方向
            Vector3 angular_velocity = Vector3.Cross(Vector3.up, blowout);
            angular_velocity.Normalize();
            angular_velocity *= Random.Range(0.5f, 1.5f) * blowoutSpeed;

            //应用
            // Debug.DrawRay(transform.position, blowout, Color.green, 1000.0f);
            bot.Blowout(blowout, angular_velocity);

        }

        Destroy(this.gameObject);
    }



// 生成怪物
public void SpawnBot(int botNum)
    {
        // 创建一个怪物数组
        bots = new BotCharacter[botNum];
        Vector3 position;
        for (int i = 0; i < botNum; i++)
        {
            // 获取怪物组盒的position
            position = transform.position;
            // 初始化一个怪物的变量
            BotCharacter bot = Instantiate<BotCharacter>(botPrefab);
            // 把初始化的怪物放在怪物数组里面
            bots[i] = bot;

            // 根据怪物数决定分散范围,并限定在GroupBox内
            Vector3 splat_range;
            //根据怪物数量，计算怪物在X,Z轴上的分布范围
            // 每一个怪物的碰撞盒的x轴的长度*准备生成怪物的数量 = 大碰撞盒的x长度
            splat_range.x = bot.bounds.size.x * (float)(botNum - 1);
            splat_range.z = bot.bounds.size.z * (float)(botNum - 1);

            //splat_range最大，不能超过怪物组包围盒范围的一半
            var collider = GetComponent<Collider>();
            splat_range.x = Mathf.Min(splat_range.x, collider.bounds.extents.x);
            splat_range.z = Mathf.Min(splat_range.z, collider.bounds.extents.z);

            position.x += Random.Range(-splat_range.x, splat_range.x);
            position.z += Random.Range(0f, splat_range.z); //z轴，让怪物集中分布在包围盒的前方
            position.y = 0;

            bots[i].transform.position = position;
            bots[i].transform.parent = transform;

            //bots[i].waveAmplitude = (i + 1) * 0.1f;
            ////45度单位偏移
            //bots[i].waveRadianOffset = (i + 1) * Mathf.PI / 4.0f;
        }
    }
    // 承受伤害
    public void TakeDamage()
    {
        GroupBlowout();
    }

    private void Update()
    {
        var pos = transform.position;
        pos.z += runspeed * Time.deltaTime;

        transform.position = pos;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

}
