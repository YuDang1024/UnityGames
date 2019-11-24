using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    // 相机跟随人物进行移动的时候，有一点抖动，这个smoothing值就代表抖动的平滑程度
    public float smoothing = 5f;

    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        //offset = transform.position - target.transform.position;
        offset = target.position - this.transform.position;
    }


    private void FixedUpdate()
    {
        //Vector3 targetCamPos = target.position + offset;
        //transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        Vector3 CameraPosition = target.position - offset;
        transform.position = CameraPosition;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
