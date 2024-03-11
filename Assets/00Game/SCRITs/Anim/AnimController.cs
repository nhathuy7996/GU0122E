using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PLAYER_STATE = PlayerController.PLAYER_STATE;

public class AnimController : AnimControllerbase
{
     
    Animator animator; 
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        
    } 

    public override AnimControllerbase ChangeBlend(string name, float val)
    {
        animator.SetFloat(name, val);
        return this;
    }

    public override AnimControllerbase ChangeAnim(PLAYER_STATE state)
    {
        _currentState = state;
        animator.SetTrigger(state.ToString());

        return this ;
    }
}
