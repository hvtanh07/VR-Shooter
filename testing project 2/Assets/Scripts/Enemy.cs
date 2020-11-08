using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemyHealth = 20;
    public void TakeDamege(float amount)
    {
        enemyHealth -= amount;
        Debug.Log("taken damage!");
        if (enemyHealth <= 0)
        {
            death();
        }
        
    }

    private void death()
    {
        Destroy(gameObject);
    }
}
