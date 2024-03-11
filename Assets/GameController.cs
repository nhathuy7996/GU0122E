using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    [SerializeField] Text timerTxt, endGameTxt;
    [SerializeField] Transform _player;
    public Transform player => _player;

    [SerializeField]
    GAME_STATE GameState = GAME_STATE.init;

    float timer = 10;
    public GAME_STATE _GameState => GameState;
    public enum GAME_STATE
    {
        init,
        play,
        pause,
        over
    }

    public void InitGame()
    {
        timer = 10;
    }

    // Start is called before the first frame update
    void Start()
    {
        timerTxt.gameObject.SetActive(false);
        endGameTxt.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GameState != GAME_STATE.play)
            return;

        timer -= Time.deltaTime;
        timerTxt.text = $"{timer:00}";
        if(timer <= 0)
        {
            this.GameState = GAME_STATE.over;
            endGameTxt.text = "WIN";
            endGameTxt.color = Color.yellow;
            endGameTxt.gameObject.SetActive(true);
        }

        if(timer < 4)
            timerTxt.color = Color.red;
    }

    public void ChangeGameState(GAME_STATE gameState)
    {
        if (gameState == this.GameState)
            return;

        this.GameState = gameState;
        if(this.GameState == GAME_STATE.play)
        {
            HiveController.instant.SpawBee();
            player.GetComponent<Rigidbody2D>().simulated = true;
            timerTxt.gameObject.SetActive(true);
        }

        if (this.GameState == GAME_STATE.over)
        {
            endGameTxt.text = "LOSE";
            endGameTxt.color = Color.red;
            endGameTxt.gameObject.SetActive(true);
        }
    }
}
