using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
   
   
    public Transform[] spawnPoints;
    public GameObject[] enemyTypes;

    public List<GameObject> enemies; //a list of enemy game objects
    public float spawnDelay = 2f;
    public int spawnCount = 10;

    public string killCondition = "Two";


    public enum EnemyType
    {
        OneHand,TwoHand,Archer
    }

    public enum PatrolType
    {
        Linear, Loop, Random
    }

    private void OnEnable()
    {
        Enemy.OnEnemyDie += KillEnemy;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDie -= KillEnemy; //parameters line up, same data type
    }

    // Start is called before the first frame update
    void Start()
    {
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
            SpawnEnemy();
        if(enemies.Count <= spawnCount)
        {
            StartCoroutine(SpawnDelay());
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) SpawnEnemy();
        if (Input.GetKeyDown(KeyCode.K)) KillAllEnemies();
        if (Input.GetKeyDown(KeyCode.O)) KillSpecificEnemy(killCondition);
    }


    void SpawnEnemy()
    {
        int enemyNumber = Random.Range(0, enemyTypes.Length);
        int spawnPoint = Random.Range(0, spawnPoints.Length);
        GameObject enemy = Instantiate(enemyTypes[enemyNumber], spawnPoints[spawnPoint].position, spawnPoints[spawnPoint].rotation, transform);
        enemies.Add(enemy);
        print("Enemy Count: " + enemies.Count);
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
    
    public void KillEnemy(GameObject _enemy)
    {
        if (enemies.Count == 0)
            return;

        Destroy(_enemy);
        enemies.Remove(_enemy);
    }

    void KillSpecificEnemy(string _condition)
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].name.Contains(_condition))
                KillEnemy(enemies[i]);
        }
    }

    void KillAllEnemies()
    {
        if (enemies.Count == 0)
            return;

        for(int i = 0; i < enemies.Count; i++)
        {
            Destroy(enemies[i]);
        }
        enemies.Clear();
    }
    //void SpawnEnemy()
    //{
    //    int enemyNumber = Random.Range(0, enemyTypes.Length);
    //    int spawnPoint = Random.Range(0, spawnPoints.Length);
    //    GameObject enemy = Instantiate(enemyTypes[enemyNumber])
    //}

  
    public Transform GetRandomSpointPoint()
    {
        //return sppawnpoint between 0 and length of array
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }
}
