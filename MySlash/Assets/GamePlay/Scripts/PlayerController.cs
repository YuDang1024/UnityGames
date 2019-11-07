using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerCharacter charactor;
    // Start is called before the first frame update
    void Start()
    {
        charactor = FindObjectOfType<PlayerCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if(charactor.canattack)
            {
                charactor.Attack();
            }
        }
    }
}
