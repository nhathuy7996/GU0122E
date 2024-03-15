using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimCallBack : MonoBehaviour
{
    public Action callback;

    public void OnAnimDone()
    {
        callback?.Invoke();
    }
}
