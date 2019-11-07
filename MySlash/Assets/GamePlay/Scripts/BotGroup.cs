using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotGroup : MonoBehaviour
{
    public float runspeed;
    BotCharacter[] bots;
    public BotCharacter botPrefab;

    // 怪物飞散
    public void GroupBlowout()
    {
         //
    }

    // 生成怪物
    public void SpawnBot()
    {
        // 
    }
    // 承受伤害
    public void TakeDamge()
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
