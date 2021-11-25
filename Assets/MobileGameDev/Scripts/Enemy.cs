using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;

    public int health;

    private NavMeshAgent navMeshAgent;
    private PlayerController player;

    public enum EnemyState
    {
        Idle,
        Chasing,
        Attacking,
        Dying
    }

    public EnemyState currentEnemyState = EnemyState.Idle;

    void Idle()
    {
        
    }

    void Chasing()
    {
        navMeshAgent.SetDestination(player.transform.position);
    }

    void Attacking()
    {
        
    }

    void Dying()
    {
        
    }

    void ChangeState(EnemyState newState)
    {
        switch(currentEnemyState)
        {
            case EnemyState.Dying :
                break;
            case EnemyState.Chasing :
               
                break;
            case EnemyState.Attacking :
                
                break;
            case EnemyState.Idle :
                
                break;
        }

        currentEnemyState = newState;
    }

    public void Alert()
    {
        if(currentEnemyState == EnemyState.Idle)
        {
            ChangeState(EnemyState.Chasing);
        }
    }
    


private void Awake()
    {
        health = maxHealth;
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindObjectOfType<PlayerController>();
    }

    public void TakeDamage(int damageAmmount)
    {
        health -= damageAmmount;
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        switch(currentEnemyState)
        {
            case EnemyState.Dying : 
                Dying();
                break;
            case EnemyState.Chasing :
                Chasing();
                break;
            case EnemyState.Attacking :
                Attacking();
                break;
            case EnemyState.Idle :
                Idle();
                break;
        }
    }
}
