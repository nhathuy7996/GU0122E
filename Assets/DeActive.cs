using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeActive : MonoBehaviour
{
    public void DelayDeactive() => Invoke(nameof(DeActiveObj), 2);
    void DeActiveObj()
    {
        DeactiveObjectPooling.instance.ReturnToPool(this);
    }
}
