using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instant;
    public static T instant => _instant;

    protected virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(_instant == null)
        {
            _instant = this.GetComponent<T>();
        }
        else if (instant.GetInstanceID() != this.GetInstanceID())
        {
            Destroy(this);
        }
    }
 
}
