using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneController : Singleton<LoadSceneController>
{
    [SerializeField] Animation anim;
    [SerializeField] AnimationClip[] clips = new AnimationClip[2];
    [SerializeField] Text loadingProgress;
    string nameScene;
    // Start is called before the first frame update

    AnimCallBack AnimCallBack;
    AsyncOperation progress;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        AnimCallBack = this.GetComponentInChildren<AnimCallBack>();
        AnimCallBack.callback = onAnimInDone;
    }

    // Update is called once per frame
    void Update()
    {
        if (progress == null)
        {
            return;
        }
        loadingProgress.text = string.Format("{0:00.0}%", progress.progress * 100);
    }

    public void LoadScene(string sceneName) {
        anim.clip = clips[0];
        anim.Play();

        this.nameScene = sceneName;

        loadingProgress.text = "0%";
    }

    public void onAnimInDone()
    {
        Scene scene = SceneManager.GetActiveScene();
        progress = SceneManager.LoadSceneAsync(this.nameScene, LoadSceneMode.Additive);
        progress.completed += (load) =>
        {
            SceneManager.UnloadSceneAsync(scene);
            anim.clip = clips[1];
            anim.Play();
        };


    }
}
