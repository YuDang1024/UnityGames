using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public enum AttackMotion
    {
        LEFT,
        RIGHT,
    }

    AttackMotion attackmotion = AttackMotion.LEFT;
    public Rigidbody rigid;
    public Animator animator;

    public float runSpeed = 5f;
    public const float runSpeedMax = 20f;
    public const float runSpeedAcc = 5f;

    public ParticleSystem swordEffectRight;
    public ParticleSystem swordEffectLeft;

    public bool canattack = true;
    const float attacktime = 0.3f;
    public float attackingtime;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocityTemp = rigid.velocity;
        runSpeed += runSpeedAcc * Time.deltaTime;
        runSpeed = Mathf.Clamp(runSpeed,0f,runSpeedMax);

        velocityTemp.z = runSpeed;

        if(velocityTemp.y >0f || velocityTemp.x >0f)
        {
            velocityTemp.y = 0;
            velocityTemp.x = 0;
        }

        rigid.velocity = velocityTemp;
        attackingtime = Time.deltaTime + attacktime;
    }

    public void Attack()
    {
        if(attackmotion == AttackMotion.LEFT)
        {
            animator.SetTrigger("AttackLeft");
            swordEffectLeft.Play();
            attackmotion = AttackMotion.RIGHT;        
        }
        else {
            animator.SetTrigger("AttackRight");
            swordEffectRight.Play();
            attackmotion = AttackMotion.LEFT; 
        }
        canattack = false;

        CancelInvoke("ResetCanAttack");
        Invoke("ResetCanAttack",attackingtime+1);
    }

    public void ResetCanAttack()
    {
        canattack = true;
    }

}
