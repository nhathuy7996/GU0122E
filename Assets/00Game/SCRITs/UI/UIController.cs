using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] Text Score;

    // Start is called before the first frame update
    void Start()
    {
        Observer.instant.AddListener("SCORE",updateScore);
    }

    // Update is called once per frame
    void updateScore(object[] score)
    {
        Debug.Log(score);
        Score.text = score[0].ToString();
    }

    private void OnDestroy()
    {
        Observer.instant.RemoveListener("SCORE", updateScore);
    }
}
