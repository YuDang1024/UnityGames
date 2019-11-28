using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //public Transform player;
    public CharacterController cc;
    public float Speed = 2.0f;
    public EasyJoystick joyStick;

    float[] x_set;
    float[] z_set;
    float[] r_set;

    // 摄影机和人物的偏移量
    Vector3 camaraOffset;

    enum Direction
    {
        INVALID = -1,
        UP = 0,
        DOWM = 1,
        LEFT = 2,
        RIGHT = 3,
        LU = 4,
        RU = 5,
        LD = 6,
        RD = 7,
    }

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        // 这两个数组指的是当人物需要走的距离确定的时候，求X，Y轴对应分量时，需要乘的系数
        x_set = new float[8] { 0f, 0f, -1f, 1f, -0.707f, 0.707f, -0.707f, 0.707f };
        z_set = new float[8] { 1f, -1f, 0f, 0f, 0.707f, 0.707f, -0.707f, -0.707f };
        r_set = new float[8] { 0, 180, -90, 90, -45, 45, -135, 135 };

        camaraOffset = Camera.main.transform.position - this.transform.position;

        //Tip:以左上方向为例，因为半径是：1，求X轴Y轴的分量的等式就是：1*1 = x*x + y*y；
        //因为左上和右上，左下和右下都是等腰三角形，说明x=y,此时等式就变成：1*1 = 2*x*x 或者 1*1 = 2*y*y
        // 求的x = y =0.707
    }

    // Update is called once per frame
    void Update()
    {
        //float s = Time.deltaTime * Speed;
        //cc.Move(new Vector3(0, 0,s));
        //float r = Mathf.Atan2(1.0f,2.0f);
        Walk();
        // 这样的摄影机跟随是不会转变方向的，适合ARPG游戏的跟随，但是如果是FPS游戏，就不能用这种跟随方式，
        // 具体可以参：https://github.com/YuDang1024/UnityGames/blob/master/MovingBoat/Assets/Scripts/CameraFollow.cs
        Camera.main.transform.position = this.transform.position + camaraOffset;
    }

    void Walk()
    {
        // 因为EasyJoyStick遥感的半径是1，所以当遥感的位置在半径为0.5的圆内的时候，此时是不做任何的响应的
        float x = joyStick.JoystickTouch.x;
        float y = joyStick.JoystickTouch.y;
        float R = x * x + y * y;
        if(R <= (0.5 * 0.5))
        {
            return; 
        }

        // 控制人物朝八个方向分别行走
        // 人物应该行走的距离
        float s = Time.deltaTime * Speed;
        // 求出遥感的二维坐标中对应的角度，正切值返回的就是角度
        float angle = Mathf.Atan2(y,x);
        //float angle = Mathf.Atan2(0, -1) = π;
        int dir = GetDirection(angle);

        Vector3 offset = new Vector3(s * this.x_set[dir], 0, s * this.z_set[dir]);
        this.cc.Move(offset);

        // 控制人物的朝向
        Vector3 rotation = this.transform.eulerAngles;
        rotation.y = this.r_set[dir];
        this.transform.eulerAngles = rotation;

    }

    int GetDirection(float angle)
    {
        // 左方向的下半部分
        if(angle >= -Mathf.PI && angle <= -(7 * Mathf.PI)/8)
        {
            return (int)Direction.LEFT; 
        }
        else if(angle >= -(7 * Mathf.PI)/8 && angle <= -(5 * Mathf.PI)/8)
        {
            return (int)Direction.LD;
        }
        else if(angle >= -(5 * Mathf.PI)/8 && angle <= -(3 * Mathf.PI)/8)
        {
            return (int)Direction.DOWM; 
        }
        else if(angle >= -(3 * Mathf.PI)/8 && angle <= -(1 * Mathf.PI) / 8)
        {
            return (int)Direction.RD; 
        }
        else if(angle >= -(1 * Mathf.PI)/8 && angle <= (1 * Mathf.PI) / 8)
        {
            return (int)Direction.RIGHT; 
        }
        else if(angle >= (1 * Mathf.PI)/8 && angle <= (3 * Mathf.PI) / 8)
        {
            return (int)Direction.RU; 
        }
        else if(angle >= (3 * Mathf.PI)/8 && angle <= (5 * Mathf.PI) / 8)
        {
            return (int)Direction.UP;
        }
        else if(angle >= (5 * Mathf.PI)/8 && angle <= (7 * Mathf.PI) / 8)
        {
            return (int)Direction.LU; 
        }
        else if(angle >= (7 * Mathf.PI)/8 && angle <= Mathf.PI)
        {
            return (int)Direction.LEFT; 
        }
        else
        {
            return (int)Direction.INVALID; 
        }
    }

    // 逻辑里面没有用，可以测试你写的判断方向的函数一样不一样
    void DebugLog(int dir)
    {
        switch (dir)
        {
            case -1:
                Debug.Log("无效");
                break;
            case 0:
                Debug.Log("上");
                break;
            case 1:
                Debug.Log("下");
                break;
            case 2:
                Debug.Log("左");
                break;
            case 3:
                Debug.Log("右");
                break;
            case 4:
                Debug.Log("左上");
                break;
            case 5:
                Debug.Log("右上");
                break;
            case 6:
                Debug.Log("左下");
                break;
            case 7:
                Debug.Log("右下");
                break;
            default:
                Debug.Log("啥也不是");
                break;
        }
    }
}
