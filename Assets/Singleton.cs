using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instant;
    public static T instant => _instant;

    private void Awake()
    {
        if(_instant == null)
        {
            _instant = this.GetComponent<T>();
        }
        else if(instant.GetInstanceID() != this.GetInstanceID())
        {
            Destroy(this);
        }
    }
 
}
