using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexEffect : MonoBehaviour
{
    // 关联材质，手动拖
    public Material material;

    // 5秒钟半径从0扩大到1，所以速度就假设成0.2
    float radiusSpeed = 0.2f;
    // 5秒钟到2弧度，所以弧度的变化为1f
    float angleSpeed = 0.2f;

    //特效持续的时间
    float totalTime = 5.0f;
    // 当前特效进行时间
    float runTime = 0.0f; 

    // 当前半径以及当前角度
    float nowRadius = 0.0f;
    float nowAngle = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        runTime += dt;

        nowAngle += (dt * angleSpeed);
        nowRadius += (dt * radiusSpeed);

        material.SetFloat("radius",nowRadius);
        material.SetFloat("angle", nowAngle);

        if(runTime >= totalTime)
        {
            nowAngle = 0.0f;
            nowRadius = 0.0f;
            runTime = 0.0f;
        }
    }
}
