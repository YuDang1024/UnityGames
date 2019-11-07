using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgrpundRepeat : MonoBehaviour
{
    const float Width = 50f;
    const int BackgroundNum = 2;

    Transform maincCamera = null;
    Vector3 initbackgroundPos;

    // Start is called before the first frame update
    void Start()
    {
        maincCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        initbackgroundPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float Totalwidth = BackgroundNum * Width;
        // 相机在Z轴的位置减去背景在Z轴的位置，也就是相机和背景在Z轴的差值
        float distz = maincCamera.position.z - initbackgroundPos.z;

        int n = Mathf.RoundToInt(distz / Totalwidth);

        Vector3 pos = initbackgroundPos;

        pos.z +=Totalwidth * n;
        transform.position = pos;

    }
}
