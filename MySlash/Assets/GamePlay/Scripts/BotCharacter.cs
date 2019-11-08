using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCharacter : MonoBehaviour
{

    public Bounds bounds;
    Vector3 blowoutVelocity, blowoutAngularVelocity;

    bool isAlive = true;
    Rigidbody rigid;

    const float yheight = -0.55f;

    private void Awake()
    {
        bounds = GetComponent<Collider>().bounds;
        GetComponent<Collider>().enabled = false;
        rigid = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //bounds = GetComponent<Collider>().bounds;
        //rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive)
        {
            rigid.velocity = blowoutVelocity;
            rigid.angularVelocity = blowoutAngularVelocity; 
        }
    }

    // 飞散   
    public void Blowout(Vector3 blowout, Vector3 angularVelocity)
    {
        GetComponent<Animator>().SetTrigger("Collapse");
        blowoutVelocity = blowout;
        blowoutAngularVelocity = angularVelocity;
        transform.parent = null;
        isAlive = false;
        Destroy(gameObject, 2);
    }
}
