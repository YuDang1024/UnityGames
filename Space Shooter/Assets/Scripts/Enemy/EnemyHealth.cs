using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    // 敌人尸体下沉的速度
    public float sinkSpeed = 2.5f;
    // 击败敌人获取的分数
    public int scoreValue = 10;
    public AudioClip deathClip;

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;

    bool isDead;
    bool isSinking;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent <CapsuleCollider>();

        currentHealth = startingHealth;
    }

    public void TakeDamage(int amout, Vector3 hitPoint)
    {
        if (isDead)
            return;

        enemyAudio.Play();
        currentHealth -= amout;

        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if(currentHealth <= 0)
        {
            Death(); 
        }
    }

    void Death()
    {
        isDead = true;
        // 敌人死后，玩家可以穿过尸体
        capsuleCollider.isTrigger = true;

        anim.SetTrigger("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play();
    }

    // 尸体开始下沉
    public void StartSinking() 
    {
        GetComponent<NavMeshAgent>().enabled = false;
        // 使其不受物理控制
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy(gameObject, 2f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //检测敌人是否下沉
        if(isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime); 
        }
    }

}
