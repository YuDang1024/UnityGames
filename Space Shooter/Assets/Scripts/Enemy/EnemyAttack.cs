using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // 两次攻击的间隔时间
    public float timeBetweenAttacks = 0.5f;
    // 攻击的伤害
    public int attackDamage = 5;

    // 敌人的动画
    Animator anim;
    // 
    GameObject player;
    EnemyHealth enemyHealth;
    // 判断玩家生命值的脚本
    PlayerHealth playerHealth;
    // 判断player是不是在敌人的攻击范围之内
    bool playerInRange;
    // 使用这个变量来确定敌人攻击的速度不会太慢也不会太快
    float timer;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyHealth = GetComponent<EnemyHealth>();
        playerHealth = player.GetComponent<PlayerHealth>();

        anim = GetComponent<Animator>();
    }

    // 当玩家进入攻击范围的时候
    // 当前的方法是在游戏对象有触发器（Trigger）的时候，有物体进入了触发器，这个函数就会被自动的调用
    // 谁进入了这个触发器，传进来的参数就是谁
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true; 
        }
    }

    // 当玩家离开攻击范围的时候
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false; 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Time.deltaTime：上一帧的结束到当前帧的时间
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth >0)
        {
            Attack(); 
        }

        if(playerHealth.currentHealth < 0)
        {
            anim.SetTrigger("PlayerDead"); 
        }
    }

    void Attack()
    {
        timer = 0f;

        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage); 
        }
        else
        {
            // 
        }
    }
}
