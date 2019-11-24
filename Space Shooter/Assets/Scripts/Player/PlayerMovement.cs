using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    // 储存玩家移动信息
    Vector3 movement;
    // 动画
    Animator anim;
    Rigidbody playerRigidBody;
    // 要告诉Raycast只照射Floor的方法
    int floorMask;
    float cameraRayLength = 10;

    // 无论脚本有没有执行都会被调用的函数
    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
    }

    // 跟随物理系统一起更新
    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h,v);
        Turing();
        Animation(h,v);
    }

    void Move(float h, float v) 
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidBody.MovePosition(transform.position + movement);
    }

    void Turing()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;
        /*
         *最后一个参数是我们在添加Floor游戏对象的时候将他的Layer更改为Floor，
         *然后在Awake函数初始化的时候，floorMask被“Floor”赋值
         *使用这个参数的目的是为了射线只能击中Floor       
        */
        //if (Physics.Raycast(ray, out Hit,cameraRayLength,floorMask))
        if (Physics.Raycast(ray, out Hit))
        {
            Vector3 playerToMouse = Hit.point - transform.position;
            playerToMouse.y = 0;

            /*
             * 四元数就是为了控制旋转的一种数学表
             * 创建一个新的四元数使用刚才我们获取的角度
             * 刚才获取的角度就是从人物朝向鼠标的四元数          
            */
            Quaternion Qua = Quaternion.LookRotation(playerToMouse);
            // 将人物的刚体朝向一个新的角度
            playerRigidBody.MoveRotation(Qua);
        }
    }

    void Animation(float h, float v)
    {
        // 判断x z轴山有没有行走
        bool walking = (h != 0f || v != 0f);
        anim.SetBool("IsWalking", walking);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
