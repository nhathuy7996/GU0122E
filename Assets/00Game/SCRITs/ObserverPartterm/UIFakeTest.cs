using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFakeTest : MonoBehaviour
{
    [SerializeField] Text HP;

    //public delegate bool Huynn(string a,int b);

    //Huynn A;

    public Func<string, int, bool> A;

    //public Predicate<string> A;

    //public Action<string> A;

    bool test1(string s, int b)
    {
        return false;
    }

    bool test2(string s, int b)
    {
        return false;
    }

    bool test3(string s, int b)
    {
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Observer.instant.AddListener("HP", changeUI);
 
        A += test1;
        A += test2;
        A += test3;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeUI(object HP)
    {
        this.HP.text = ((float)HP).ToString();
    }

    private void OnDestroy()
    {
        Observer.instant.RemoveListener("HP",changeUI);
    }
}
