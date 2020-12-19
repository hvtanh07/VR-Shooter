using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float enemyHealth = 20;
    GameObject player;
    Animator anim;
    Player playerscript;
    Vector3 originalPos;
    NavMeshAgent nav;
    bool followingPlayer;

    public float timeBetweenAttacks = 2f;    
    public int attackDamage = 10;
    public float distoDetect = 15;

    bool playerInRange;
    bool attacked;
    float timer;

    void Start()
    {
        originalPos = transform.position;
        followingPlayer = true;
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        playerscript = player.GetComponent<Player>();
        nav = GetComponent<NavMeshAgent>();       
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }
    void OnTriggerExit(Collider other)
    {       
        if (other.gameObject == player)
        {           
            playerInRange = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= timeBetweenAttacks && playerInRange && playerscript.currentHealth > 0 && enemyHealth > 0)
        {           
            Attack();
        }
        
        if (playerscript.currentHealth <= 0)
        {
            followingPlayer = false;
        }
        if (enemyHealth > 0)
        {
            RaycastHit hit;
            float dis = Vector3.Distance(transform.position, player.transform.position);
            if (Physics.Raycast(transform.position, (player.transform.position - transform.position), out hit, 200f))
            {
                if (hit.transform.tag == "Player")
                {
                    if (dis < distoDetect || attacked)
                    {
                        if (enemyHealth > 0 && followingPlayer)
                        {
                            anim.SetBool("Walk Forward", true);
                            nav.SetDestination(player.transform.position);
                        }
                    }
                    else nav.SetDestination(originalPos);
                }
                else
                {
                    nav.SetDestination(originalPos);
                    attacked = false;
                }
                if (nav.velocity.magnitude < 0.15f)
                {
                    anim.SetBool("Walk Forward", false);
                }
            }
        }
    }
    void Attack()
    {      
        timer = 0f;
        anim.SetTrigger("Attack");
        if (playerscript.currentHealth > 0 && enemyHealth >= 0)
        {
            playerscript.TakeDamage(attackDamage);
        }
    }
    public void TakeDamege(float amount)
    {
        attacked = true;
        enemyHealth -= amount;       
        if (enemyHealth <= 0)
        {
            death();
            gameObject.layer = 2; 
        }else anim.SetTrigger("Damaged");
    }

    private void death()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        anim.SetTrigger("Die");              
        Destroy(gameObject,1.4f);
    }
}
