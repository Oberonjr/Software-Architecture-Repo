using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField]private int maxHealth;
    [HideInInspector]public int currentHealth;
    
    void Start()
    {
        currentHealth = maxHealth;
    }
    

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject, 0.5f);
    }
}
