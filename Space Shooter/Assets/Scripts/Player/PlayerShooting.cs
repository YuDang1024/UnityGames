using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    // 每一颗子弹的伤害
    public int damagePerShot = 20;
    // 两次射击之间的间隔
    public float timeBetweenBullets = 0.15f;
    // 子弹的范围
    public float range = 100f;

    //
    float timer;
    // 射击的射线
    Ray shootRay;
    // 射击中的物体
    RaycastHit shootHit;
    int shootableMask;
    // 射击枪的时候的例子特效
    ParticleSystem gunParticleSystem;
    // 射出去的子弹的渲染线
    LineRenderer gunLineRender;
    // 射击枪的时候的声音
    AudioSource gunAudio;
    // 射击时候的光线
    Light gunLight;
    // 效果消失之前，会持续多久可见
    float effectDisplayTime = 0.2f;


    private void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticleSystem = GetComponent<ParticleSystem>();
        gunLineRender = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(Input.GetButton("Fire1") && timer >= timeBetweenBullets)
        {
            Shoot(); 
        }
        // 开火完毕之后关闭效果
        if(timer >= timeBetweenBullets * effectDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        gunLight.enabled = false;
        gunLineRender.enabled = false; 
    }

    void Shoot()
    {
        timer = 0f;

        gunAudio.Play();
        gunLight.enabled = true;

        gunParticleSystem.Stop();
        gunParticleSystem.Play();

        gunLineRender.enabled = true;
        // 第一个参数是代表Line的第一个点，第二个参数是代表Line的要设置Line的第一个点的坐标
        gunLineRender.SetPosition(0, transform.position);
        // 以下开始设置射线的第二个点
        // 射线的起点
        shootRay.origin = transform.position;
        // 射线的方向
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit))
        {
            // 获取脚本
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if(enemyHealth != null)
            {
                // 把伤害传进去，然后把射击到的点穿进去，在这个点生成例子效果
                enemyHealth.TakeDamage(damagePerShot,shootHit.point); 
            }
            gunLineRender.SetPosition(1,shootHit.point);
        }
        else 
        {
            gunLineRender.SetPosition(1, shootRay.origin + shootRay.direction); 
        }
    }
}
