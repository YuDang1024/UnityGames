using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject gameObject;

    Vector3 offset;
    Quaternion rotation;

    private void Awake()
    {
        offset = gameObject.transform.position - this.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject == null)
        {
            return;
        }
        //this.transform.position = gameObject.transform.position;
        //Vector3 CameraPosition = this.transform.position;
        //CameraPosition.z -= 4;
        //this.transform.position = CameraPosition;
        //this.transform.LookAt(gameObject.transform);

        this.transform.position = gameObject.transform.position;

        Vector3 CameraPosition = this.transform.position;
        rotation = Quaternion.Euler(0,gameObject.transform.eulerAngles.y,0);
        CameraPosition += rotation * Vector3.back * 4;
        CameraPosition.y += Vector3.up.z;
        this.transform.position = CameraPosition;

        this.transform.LookAt(gameObject.transform);
    }
}
