using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class AnimControllerSpine : AnimControllerbase
{
    SkeletonAnimation _skeletonAnimation;

    private void Start()
    {
        _skeletonAnimation = GetComponent<SkeletonAnimation>();
    }
    public override AnimControllerbase ChangeAnim(PlayerController.PLAYER_STATE state)
    {
        if (state == _currentState)
            return this;

        _currentState = state;
        _skeletonAnimation.AnimationState.SetAnimation(0,state.ToString().ToLower(),true);
        return this;
    }

    public override AnimControllerbase ChangeBlend(string name, float val)
    {
        return this;
    }
}
