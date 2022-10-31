using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    public TMP_Text scoreText;
    public TMP_Text enemyCountText;
    public TMP_Text difficultyText;
    public TMP_Text timerText;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void UpdateScore(int _score)
    {
        scoreText.text = "Score: " + _score;

    }

    public void EnemyCountUpdate(int _count)
    {
        enemyCountText.text = "Enemies: " + _count;
    }

    public void DifficultyUpdate(Difficulty _difficulty)
    {
        difficultyText.text = "Difficulty: " + _difficulty.ToString();
    }

    public void TimerUpdate(float _time)
    {
        timerText.text = "Time: " + _time.ToString("F2"); //number of floating point percision
    }
}
