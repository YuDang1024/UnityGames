using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMageger : MonoBehaviour
{
    // 检测玩家血量大于0的时候，才能生成敌人
    public PlayerHealth playerHealth;
    // 使用敌人的Prefab，来生成对象
    public GameObject enemy;
    //
    public float spawnTime;
    //
    public Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        // 重复调用Spawn函数，第二个参数指的是等多久调用，第三个参数是指等多久重复调用
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        if(playerHealth.currentHealth <0)
        {
            return; 
        }

        int spawmPointIndex = Random.Range(0, spawnPoints.Length);
        // 实例化一个对象，在哪里生成，生成时候的旋转角度
        Instantiate(enemy,spawnPoints[spawmPointIndex].position,spawnPoints[spawmPointIndex].rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
