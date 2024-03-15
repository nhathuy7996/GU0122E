using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{

    public enum LEVEL_STATE
    {
        Init,
        Playing,
        Pause,
        Over
    }

    public LEVEL_STATE state;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instant == null)
            return;

        PlayerController.instant.gameObject.SetActive(true);
        state = LEVEL_STATE.Playing;
    }
}
