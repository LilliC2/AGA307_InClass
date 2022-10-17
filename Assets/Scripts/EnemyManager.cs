using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyTypes;

    public List<GameObject> enemies; //a list of enemy game objects
    public float spawnDelay = 2f;
    public int spawnCount = 10;

    GameManager _GM;

    public enum EnemyType
    {
        OneHand,TwoHand,Archer
    }

    public enum PatrolType
    {
        Linear, Loop, Random
    }

    // Start is called before the first frame update
    void Start()
    {
        _GM = FindObjectOfType<GameManager>();
        StartCoroutine(SpawnDelay());

    }

    /// <summary>
    /// spawns enemy with delay until enemy count is reached
    /// </summary>
    /// <returns></returns>

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(spawnDelay);
        if(_GM.gameState == GameState.Playing)
            SpawnEnemies();
        if(enemies.Count <= spawnCount)
        {
            StartCoroutine(SpawnDelay());
        }

    }

    void SpawnEnemies()
    {
        //loop from 0 until lengthof our spawnPoints array
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            //get a random int between 0 and our enemy types length
            int rndEnemy = Random.Range(0, enemyTypes.Length);

            //instantiate tghe first element of our enemy types array at the
            //position and rotation of the current spawnpoint [i] in the loop
            GameObject enemy = Instantiate(enemyTypes[rndEnemy], spawnPoints[i].position, spawnPoints[i].rotation);
            //add the newly created enemy to our enemies list
            enemies.Add(enemy);
        }
        //print total number of enemies we have spawned
        print("Enemy Count: " + enemies.Count);
    }

    //void SpawnEnemy()
    //{
    //    int enemyNumber = Random.Range(0, enemyTypes.Length);
    //    int spawnPoint = Random.Range(0, spawnPoints.Length);
    //    GameObject enemy = Instantiate(enemyTypes[enemyNumber])
    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform GetRandomSpointPoint()
    {
        //return sppawnpoint between 0 and length of array
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }
}
