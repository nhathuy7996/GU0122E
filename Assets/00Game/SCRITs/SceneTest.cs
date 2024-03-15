using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadSceneController.instant.LoadScene("Level2" );
        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            LoadSceneController.instant.LoadScene("Level3");
        }
    }
}
