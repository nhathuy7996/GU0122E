using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    AsyncOperation loading;
    // Start is called before the first frame update
    void Start()
    {
        
        loading = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        // StartCoroutine(wwaitLoading());     
        loading.completed += (load) =>
        {
            SceneManager.UnloadSceneAsync(0);
        };
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(loading.progress);
         
    }

    IEnumerator wwaitLoading()
    {
        yield return new WaitUntil(()=> loading.isDone);
        SceneManager.UnloadSceneAsync(0);
    }
}
