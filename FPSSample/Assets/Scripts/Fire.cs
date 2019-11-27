using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // 开火动画
    public ParticleSystem fireParticleSystem;
    // 开火音效
    public AudioSource fireAudioSource;
    // 击中生成的碎片
    public GameObject piece;
    // 发出射线的位置
    // 因为射线都是从眼睛的位置发射，然后方向是眼睛局部坐标的正前方，
    public Transform eye;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shooting(); 
        }
    }

    void Shooting()
    {
        // 播放开火动画
        this.fireParticleSystem.Play();
        // 播放开火音乐
        // 使用这个函数去播放的话，会更急促一些
        this.fireAudioSource.PlayOneShot(this.fireAudioSource.clip);

        // 检测射线碰撞的点，然后在碰撞的点的位置生成粒子特效
        RaycastHit hit;
        // 射线的起，射线的方向，击中的物体，射线的最远距离
        if (Physics.Raycast(this.eye.position,this.eye.forward,out hit, 100))
        {
            //击中之后，根据击中点的坐标，生成预制体
            MakePrefab(hit.point);
             
        }
    }

    void MakePrefab(Vector3 position)
    {
        for(int i = 0; i<5; i++)
        {
            GameObject p = Instantiate(piece);
            p.transform.position = position;
            p.GetComponent<Rigidbody>().AddForce(this.eye.forward * 100f);
            // 1秒之后消失
            Destroy(p, 1);
        }
    }
}
