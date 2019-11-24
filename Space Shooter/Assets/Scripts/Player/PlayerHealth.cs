using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // 玩家开始的时候有多少血
    public int startingHealth = 100;
    // 当前血量
    public int currentHealth;
    // 用来展示玩家血量的Slider
    public Slider healthSider;
    // 创建的用来展示玩家受伤时候的图片，Alpha为0
    public Image damageImage;
    // 这个音乐只会在输掉游戏的时候播放一次
    public AudioClip deathClip;
    // 决定受到伤害的时候图片闪的有多块
    public float flashSpeed = 10f;
    // 受到伤害的时候，闪的图片的颜色
    public Color flashColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);

    // 玩家动画的引用
    Animator anim;
    // 玩家身上音乐的引用
    AudioSource playerAudio;
    // 写好的其他脚本的引用,防止玩家挂掉之后还要乱跑
    PlayerMovement playermovement;
    //
    PlayerShooting playerShooting;
    // 玩家是否死亡
    bool isDead;
    // 玩家是否受伤
    bool damage;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playermovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        currentHealth = startingHealth;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //如果受伤，就显示那个红色的图片，如果没有受伤，就让红色的图片淡出
        if(damage)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear,flashSpeed*Time.deltaTime);
        }
        damage = false;
    }


    // 敌人伤害了玩家，所以别的脚本就会调这个函数
    // 参数是受到伤害的值
    public void TakeDamage(int amout) 
    {
        //敌人攻击了玩家，所以我们需要先去显示那个红色的图片
        damage = true;
        // 修改当前生命值
        currentHealth -= amout;
        // 修改UI滑动条的值
        healthSider.value = currentHealth;
        // 播放玩家受伤的音效
        playerAudio.Play();

        // 如果血量小于了0且玩家之前是存活的
        if(currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    private void Death()
    {
        isDead = true;

        playerShooting.DisableEffects();
        // 播放死亡动画
        anim.SetTrigger("Die");
        // 更换游戏结束的音乐然后播放他
        playerAudio.clip = deathClip;
        playerAudio.Play();
        // 禁用玩家移动的脚本，
        playermovement.enabled = false;
        playerShooting.enabled = false;
    }
}
