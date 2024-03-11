using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFakeTest : MonoBehaviour
{
    [SerializeField] Text HP;
    // Start is called before the first frame update
    void Start()
    {
        Observer.instant.AddListener("HP", changeUI);
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
