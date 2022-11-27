using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : GameBehaviour
{
    public EnemyType myType;
    public float mySpeed;
    public float myHealth;
    float myMaxHealth;
    public float attackRadius = 5;

    [Header("AI")]
    public PatrolType myPatrol;
    //public int patrolPoint = 0;            //Needed for linear patrol movement
    //public bool reverse = false;           //Needed for repeat patrol movement
    //public Transform startPos;             //Needed for repeat patrol movement
    //public Transform endPos;               //Needed for repeat patrol movement
    //public Transform moveToPos;

    NavMeshAgent agent;
    int currentWayPoint;
    float detectDistance = 10f;
    float detectTime = 5f;
    float attackDistance = 2f;

    [Header("Health Bar")]
    public Slider healthBarSlider;
    public TMP_Text healthBarText;



    Animator anim;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        Setup();
        SetupAI();
        transform.SetPositionAndRotation(transform.position, transform.rotation);
    }

    void Setup()
    {
        float healthModifier = 1;
        float speedModifier = 1;
        switch (_GM.difficulty)
        {
            case Difficulty.Easy:
                healthModifier = 1f;
                speedModifier = 1f;
                break;
            case Difficulty.Medium:
                healthModifier = 2f;
                speedModifier = 1.2f;
                break;
            case Difficulty.Hard:
                healthModifier = 3f;
                speedModifier = 1.5f;
                break;
            default:
                healthModifier = 1f;
                speedModifier = 1f;
                break;
        }

        switch (myType)
        {
            case EnemyType.OneHand:
                myHealth = 100f * healthModifier;
                mySpeed = 2f * speedModifier;
                myPatrol = PatrolType.Patrol;
                break;
            case EnemyType.TwoHand:
                myHealth = 200f * healthModifier;
                mySpeed = 1f * speedModifier;
                myPatrol = PatrolType.Patrol;
                break;
            case EnemyType.Archer:
                myHealth = 60f * healthModifier;
                mySpeed = 5f * speedModifier;
                myPatrol = PatrolType.Patrol;
                break;
        }

        myMaxHealth = myHealth;
        healthBarSlider.maxValue = myHealth;
        UpdateHealthBar();

    }

    void SetupAI()
    {
        currentWayPoint = UnityEngine.Random.Range(0, _EM.spawnPoints.Length);
        agent.SetDestination(_EM.spawnPoints[currentWayPoint].position);
        ChangeSpeed(mySpeed);

        //startPos = Instantiate(new GameObject(), transform.position, transform.rotation).transform;
        //endPos = _EM.GetRandomSpawnPoint();
        //moveToPos = endPos;
    }

    void ChangeSpeed(float _speed)
    {
        agent.speed = _speed;
    }


    //IEnumerator Move()
    //{
    //    switch(myPatrol)
    //    {
    //        case PatrolType.Linear:
    //            moveToPos = _EM.spawnPoints[patrolPoint];
    //            patrolPoint = patrolPoint != _EM.spawnPoints.Length ? patrolPoint + 1 : 0;
    //            break;
    //        case PatrolType.Random:
    //            moveToPos = _EM.GetRandomSpawnPoint();
    //            break;
    //        case PatrolType.Loop:
    //            moveToPos = reverse ? startPos : endPos;
    //            reverse = !reverse;
    //            break;
    //    }

    //    transform.LookAt(moveToPos);
    //    while (Vector3.Distance(transform.position, moveToPos.position) > 0.3f)
    //    {
    //        if(Vector3.Distance(_P.transform.position, transform.position) < attackRadius)
    //        {
    //            Attack();
    //            yield break;
    //        }

    //        transform.position = Vector3.MoveTowards(transform.position, moveToPos.position, Time.deltaTime * mySpeed);
    //        yield return null;
    //    }

    //    yield return new WaitForSeconds(1);

    //    StartCoroutine(Move());
    //}

    private void Update()
    {

        anim.SetFloat("Speed", mySpeed);

        //find distance between player and me
        float distanceToPlayer = Vector3.Distance(transform.position, _P.transform.position);

        if(distanceToPlayer <= detectDistance && myPatrol != PatrolType.Attack)
        {
            if(myPatrol != PatrolType.Chase)
            {
                myPatrol = PatrolType.Detect;
            }
        }

        switch(myPatrol)
        {
            case PatrolType.Patrol:
                float distanceToWayPoint = Vector3.Distance(transform.position, _EM.spawnPoints[currentWayPoint].position);
                if (distanceToWayPoint < 1)
                    SetupAI();
                detectTime = 5;
                break;
            case PatrolType.Detect:
                agent.SetDestination(transform.position);   //set destination to ourself
                ChangeSpeed(0);                             //stops our movement
                detectTime -= Time.deltaTime;               //decrease our detect time
                if(distanceToPlayer <= detectDistance)      
                {
                    myPatrol = PatrolType.Chase;
                    detectTime = 5;
                }
                if(detectTime <= 0)
                {
                    myPatrol = PatrolType.Patrol;
                    SetupAI();
                }
                break;
            case PatrolType.Chase:
                //set destination to player
                agent.SetDestination(_P.transform.position);
                //increase speed
                ChangeSpeed(mySpeed * 2);
                //if player out of 'sight', enter decect patrol
                if(distanceToPlayer> detectDistance)
                    myPatrol=PatrolType.Detect;
                if(distanceToPlayer <= attackDistance)
                {

                    StartCoroutine(Attack());
                }
                break;
            //case PatrolType.Attack:
            //    break;


        }
    }

    public void Hit(int _damage)
    {
        myHealth -= _damage;
        UpdateHealthBar();
        if (myHealth <= 0)
        {
            Die();
        }
        else
        {
            PlayAnimation("Hit");

            GameEvents.ReportEnemyHit(this.gameObject);
        }
    }
    
    IEnumerator Attack()
    {
        myPatrol = PatrolType.Attack;
        print("ATTACK");
        ChangeSpeed(0);
        PlayAnimation("Attack");

        yield return new WaitForSeconds(2);
        ChangeSpeed(mySpeed);
        myPatrol = PatrolType.Patrol;

    }

    void UpdateHealthBar()
    {
        healthBarSlider.value = myHealth;
        healthBarText.text = myHealth + "/" + myMaxHealth;
    }

    void Die()
    {
        ChangeSpeed(0);
        myPatrol = PatrolType.Die;
        GetComponent<Collider>().enabled = false;
        PlayAnimation("Die");

        StopAllCoroutines();
        GameEvents.ReportEnemyDie(this.gameObject);
    }

    void PlayAnimation(string _anim)
    {
        int ranAnim = UnityEngine.Random.Range(1, 4);
        anim.SetTrigger(_anim + ranAnim);
    }
}
