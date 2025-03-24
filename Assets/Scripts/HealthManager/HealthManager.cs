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
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        EventBus<ModifyHealthEvent>.Publish(new ModifyHealthEvent(-damage));
    }
}
