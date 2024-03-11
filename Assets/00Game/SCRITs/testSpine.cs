using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class testSpine : MonoBehaviour
{
    UnityArmatureComponent armature;
    // Start is called before the first frame update
    void Start()
    {
        armature = GetComponent<UnityArmatureComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            armature.animation.Play("idle",3); 
        }
    }
}
