using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyManager;

public class Enemy : GameBehaviour
{
    //events
    public static event Action<GameObject> OnEnemyHit = null; //on signifies event
    public static event Action<GameObject> OnEnemyDie = null; //null meaning empty, no event is happening



    public EnemyType myType;
    
    public float myHealth = 20f;
    public float mySpeed = 100f;
    

    [Header("Ai")]
    public PatrolType myPatrol;
    Transform moveToPos;    //need for random patrol
    int patrolPoint = 0;    //need for linear patrol
    bool reverse = false;   //need for loop patrol
    Transform startPos;     //need for loop patrol
    Transform endPos;       //need for loop patrol

    void Start()
    {
        SetUp();
        SetUpAI();
        StartCoroutine(Move());
    }

    void SetUp()
    {
        switch(myType) //switch is essentially casewhere
        {
            case EnemyType.OneHand:
                myHealth = 100f;
                mySpeed = 2f;
                myPatrol = PatrolType.Linear;
                break;
            case EnemyType.TwoHand:
                myHealth = 200f;
                mySpeed = 1f;
                myPatrol = PatrolType.Loop;
                break;
            case EnemyType.Archer:
                myHealth = 75f;
                mySpeed = 4f;
                myPatrol = PatrolType.Random;
                break ;
        }

    }

    void SetUpAI()
    {
        startPos = transform;
        endPos = _EM.GetRandomSpointPoint();
        moveToPos = endPos;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            Hit(10);


    }

    public void Hit(int _damage)
    {
        myHealth -= _damage;
        print("health: " + myHealth);

        if (myHealth <= 0)
            Die();
        else
        {
            OnEnemyHit?.Invoke(this.gameObject);
        }
        
    }

    void Die()
    {
        StopAllCoroutines();
        OnEnemyDie?.Invoke(this.gameObject); //if OnEnemyDie
        
    }

    IEnumerator Move()
    {

        switch(myPatrol)
        {
            case PatrolType.Linear:
                moveToPos = _EM.spawnPoints[patrolPoint];
                //says if patrol point != _EM.spawnPoints.Length, patrolPoint ++, else patrolPoint = 0
                // ? is the if, : is the else (? is true, : is false)
                patrolPoint = patrolPoint != _EM.spawnPoints.Length - 1 ? patrolPoint + 1 : 0;
                break;
            
            case PatrolType.Loop:
                moveToPos = reverse ? startPos : endPos;
                reverse = !reverse; //flip reverse bool
                break;
            
            case PatrolType.Random:
                moveToPos = _EM.GetRandomSpointPoint();
                break;

        }





        while (Vector3.Distance(transform.position, moveToPos.position) > 0.3f)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveToPos.position, Time.deltaTime * mySpeed);
            Vector3 targetDirection = (moveToPos.position - transform.position).normalized;
            var rotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * mySpeed);


            //transform.rotation = Quaternion.LookRotation(newDirection);
            yield return null;
        }
        yield return new WaitForSeconds(1);

        StartCoroutine(Move());


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Projectile")) 
        {
            Hit(200);
            
        }
    }
}
