using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneController : Singleton<LoadSceneController>
{
    [SerializeField] Animation anim;
    [SerializeField] AnimationClip[] clips = new AnimationClip[2];
    string nameScene;
    // Start is called before the first frame update

    AnimCallBack AnimCallBack;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        AnimCallBack = this.GetComponentInChildren<AnimCallBack>();
        AnimCallBack.callback = onAnimInDone;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneName) {
        anim.clip = clips[0];
        anim.Play();

        this.nameScene = sceneName;

        
    }

    public void onAnimInDone()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync(this.nameScene, LoadSceneMode.Additive).completed += (load) =>
        {
            SceneManager.UnloadSceneAsync(scene);
            anim.clip = clips[1];
            anim.Play();
        };
    }
}
