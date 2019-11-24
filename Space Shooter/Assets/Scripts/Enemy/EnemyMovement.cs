using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
     /*
     * 摄影机把游戏对象设置成为了publi
     * 但是此时没有设置游戏对象为Publi
     * 因为我们的敌人会一直生成，每生成一个敌
     * 这个敌人就要自动的去寻找玩家，我们不能一个的拖
     * 所以我们设置成private
     */ 
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;

    NavMeshAgent nav;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHealth.currentHealth >0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination(player.position);
        }
        else
        {
            nav.enabled = false; 
        }
    }
}
