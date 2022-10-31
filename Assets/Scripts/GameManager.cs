using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Title, Paused, Playing, GameOver}
public enum Difficulty { Easy, Medium, Hard}

public class GameManager : Singleton<GameManager> //singleto script makes this a singleton
{

    public static event Action<Difficulty> OnDifficultityChanged = null;

    public GameState gameState;
    public Difficulty difficulty;

    float timer;

    public int score;
    int scoreMultipler = 1;

    private void Start()
    {
        timer = 0;
        SetUp();
        OnDifficultityChanged?.Invoke(difficulty);
    }

    private void Update()
    {
        if (gameState == GameState.Playing)
        {
            timer += Time.deltaTime;
            _UI.TimerUpdate(timer);


        }
    }
    public void ScoreCalculations(int _score)
    {
        score += _score * scoreMultipler;
        //print("Score is: " + score);
        _UI.UpdateScore(score);
    }

    //scene mangement

    public void LoadGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeDifficulty(int _difficulty)
    {
        difficulty = (Difficulty)_difficulty;
        SetUp();
    }
    

    private void OnEnable() //happens anytime a script is enabled or when it hears a event happening
    {
        Enemy.OnEnemyHit += OnEnemyHit; //not same data type as ScoreCalc so it needs another level
        Enemy.OnEnemyDie += OnEnemyDie;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyHit -= OnEnemyHit;
        Enemy.OnEnemyDie -= OnEnemyDie;
    }

    void OnEnemyHit(GameObject _enemy)
    {
        ScoreCalculations(10);
    }

    void OnEnemyDie(GameObject _enemy)
    {
        ScoreCalculations(100);
    }

    void SetUp()
    {
        switch(difficulty)
        {
            case Difficulty.Easy:
                scoreMultipler = 1;
                break;
            case Difficulty.Medium:
                scoreMultipler = 2;
                break;
            case Difficulty.Hard:
                scoreMultipler= 3;
                break;
        }    


    }

}
