using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShader : MonoBehaviour
{
    // 这个材质就在面板里面将人物使用的材质拖进来就可以
    public Material mat;

    static Shader normal = null;
    static Shader outline = null;

    // 从鼠标发射的射线，检测有没有在人物的身上
    Ray ray;
    // 检测射线触碰的人物是否我们这个模型
    RaycastHit hit;


    private void Awake()
    {
        // 给这两个shader进行赋值
        normal = Shader.Find("Toon/Basic");
        outline = Shader.Find("Toon/Basic Outline");

        if(normal == null || outline == null)
        {
            Debug.Log("有shader赋值错误了");
        }
        else 
        {
            Debug.Log("normal's name:" + normal.name + "outline's name:" +outline.name); 
        }

        mat.shader = normal;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out hit))
        {
            if(hit.transform.name == "People")
            {
                mat.shader = outline;
            }
        }
        else
        {
            if (mat.shader.name != "Toon/Basic")
                mat.shader = normal; 
        }
    }
}
