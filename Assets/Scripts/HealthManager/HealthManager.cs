using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            TakeDamage(other.gameObject.GetComponent<EnemyStats>().Damage);
        }
    }

    public void TakeDamage(int damage)
    {
        
    }
}
