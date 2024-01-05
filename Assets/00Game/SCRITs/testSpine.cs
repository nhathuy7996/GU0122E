using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class testSpine : MonoBehaviour
{
    SkeletonAnimation skeletonAnimation;
    [SerializeField]
    [SpineAnimation] string idle,run,shoot;
    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            skeletonAnimation.AnimationState.SetAnimation(0, idle,true);
            skeletonAnimation.AnimationState.SetAnimation(1, run, true);
        }
    }
}
