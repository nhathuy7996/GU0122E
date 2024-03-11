using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //[SerializeField] GameObject objectSpawn;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    DeActive clone;
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //Instantiate(objectSpawn, Vector3.up * 5, Quaternion.identity);
            clone = DeactiveObjectPooling.instance.GetObjectFromPool();

            clone.transform.position = Vector3.up * 6;
            clone.GetComponent<DeActive>().DelayDeactive();

            // Set up lai bullet
            clone.gameObject.SetActive(true);
        }
    }
}
