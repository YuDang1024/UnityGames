using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // 行走的速度
    public AudioSource walkAudioSource;
    public float walkSpeed;
    public Transform eye;
    public Transform rightHand;

    // 绕Y轴旋转
    // 左右的旋转是按照Y轴旋转的
    float rota_y;
    // 绕X轴旋转
    // 上下的移动是绕X轴旋转
    float rota_x;

    //鼠标移动的像素的距离对应的角度的比例，俗称：鼠标灵敏度
    public float sensitivity = 5.0f; // 移动1，对应5度

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Walk())
        {
            walkAudioSource.Play(); 
        }

        LookAround();
    }

    bool Walk()
    {
        if(Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(0,0,walkSpeed * Time.deltaTime);
            return true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(-walkSpeed * Time.deltaTime, 0, 0);
            return true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(0, 0, -walkSpeed * Time.deltaTime);
            return true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(walkSpeed * Time.deltaTime, 0, 0);
            return true;
        }
        return false;
    }

    void LookAround() 
    {
        // 左右移动，绕Y
        rota_y += (Input.GetAxis("Mouse X") * sensitivity);
        rota_y = Mathf.Clamp(rota_y,-360,360);

        Vector3 rotaY = this.transform.localEulerAngles;
        rotaY.y = rota_y;
        this.transform.localEulerAngles = rotaY;

        // 上下移动,绕X
        rota_x += (Input.GetAxis("Mouse Y") * sensitivity);
        rota_x = Mathf.Clamp(rota_x, -45, 45);
        Vector3 rotaX = this.eye.localEulerAngles;
        rotaX.x = -rota_x;
        this.eye.localEulerAngles = rotaX;

        // 握枪的手也要上下摇摆
        this.rightHand.localEulerAngles = rotaX;

    }
}
