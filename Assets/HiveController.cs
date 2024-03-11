using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveController : Singleton<HiveController>
{
    [SerializeField] GameObject _bee;
    [SerializeField] int _numBees;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawBee()
    {
        for(int i = 0; i< _numBees; i++)
        {
            LazyPooling.Instance.getObj(_bee).SetActive(true);
        }
    }
}
