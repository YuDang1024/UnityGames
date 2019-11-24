using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    // 挂掉之后，重新开始需要多久
    public float restartDelay = 5f;

    Animator anim;
    //
    float restartTimer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
            restartTimer += Time.deltaTime;

            if(restartTimer >= restartDelay)
            {
                // 重新读取Scene，然后加载当前场景
                Application.LoadLevel(Application.loadedLevel); 
            }
        }
    }
}
