using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    // 绕Z轴的摇摆
    float z_Speed = 3.0f;
    // 绕X轴的摇摆
    float x_Speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 绕Z轴摇晃
        if(this.transform.eulerAngles.z >= 4 && this.transform.eulerAngles.z <= 180)
        {
            z_Speed = -z_Speed; 
        }
        else if(this.transform.eulerAngles.z <= (360 -4) && this.transform.eulerAngles.z >=180)
        {
            z_Speed = -z_Speed;
        }

        // 绕X轴摇晃
        if(this.transform.eulerAngles.x >= 4 && this.transform.eulerAngles.x <= 180)
        {
            x_Speed = -x_Speed; 
        }
        else if(this.transform.eulerAngles.x >= 180 && this.transform.eulerAngles.x <= (360 -4))
        {
            x_Speed = -x_Speed; 
        }

        this.transform.Rotate(z_Speed * Time.deltaTime, 0,z_Speed * Time.deltaTime);
    }
}
