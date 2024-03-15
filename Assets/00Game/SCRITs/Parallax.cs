using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    [SerializeField] List<Transform> mTrans;
    PlayerController playerController;

    [SerializeField] List<float> _Speeds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.instant.state != LevelManager.LEVEL_STATE.Playing)
            return;

        if (playerController == null)
            playerController = PlayerController.instant;

        if (playerController.state == PlayerController.PLAYER_STATE.IDLE)
            return;

        for (int i = 0; i< mTrans.Count; i++)
        {
            mTrans[i].Translate(Vector3.right * playerController.transform.localScale.x * _Speeds[i] * Time.deltaTime);
        }
    }
}
