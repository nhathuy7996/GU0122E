using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
using Unity.VisualScripting;

public class AnimcontrollerDG : AnimControllerbase
{
    UnityArmatureComponent armatureComponent;

    // Start is called before the first frame update
    void Start()
    {
        armatureComponent = GetComponent<UnityArmatureComponent>();
        armatureComponent.animation.Play("idle");
    }
    public override AnimControllerbase ChangeAnim(PlayerController.PLAYER_STATE state)
    {
       
        return this;
    }

    public override AnimControllerbase ChangeBlend(string name, float val)
    {
        return this;
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }
}
