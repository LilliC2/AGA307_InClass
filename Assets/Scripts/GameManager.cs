using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Title, Paused, Playing, GameOver}
public enum Difficulty { Easy, Medium, Hard}

public class GameManager : MonoBehaviour
{
    public GameState gameState;
    public Difficulty difficulty;

    public int score;
    int scoreMultipler = 1;

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
