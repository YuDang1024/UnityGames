using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public EasyJoystick easyJoystick;

    float Speed = 5.0f;
    // 假设3秒转一圈，那么一秒就算是转120度
    float w_speed = 120f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ControlMove();
    }

    void ControlMove()
    {
        float Joystick_x = easyJoystick.JoystickAxis.x;
        float Joystick_y = easyJoystick.JoystickAxis.y;
        //Vector3 targetDirection;

        // 船的移动
        if (Joystick_y >= 0.5f)
        {
            // Translate:向某个方向移动多少距离
            this.transform.Translate(0, 0, Speed * Time.deltaTime); 
        }
        else if(Joystick_y <= -0.5f)
        {
            this.transform.Translate(0, 0, -Speed * Time.deltaTime);
        }

        if (Joystick_x >= 0.5f)
        {
            // Translate:向某个方向移动多少距离
            this.transform.Translate(Speed * Time.deltaTime, 0, 0);
        }
        else if (Joystick_x <= -0.5f)
        {
            this.transform.Translate(-Speed * Time.deltaTime, 0, 0);
        }

        // 船的转向
        if(Joystick_x > 0.5f)
        {
            this.transform.Rotate(0,this.w_speed * Time.deltaTime,0);
        }
        else if(Joystick_x < -0.5f)
        {
            this.transform.Rotate(0, -this.w_speed * Time.deltaTime, 0);
        }

    }
}
