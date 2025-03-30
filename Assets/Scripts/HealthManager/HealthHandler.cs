using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Handler script placed on the targets for the enemies
 * Requires a trigger collider placed on the same object
 * Kills the incoming enemy and notifies GameManager how much damage to take
 */
public class HealthHandler : MonoBehaviour
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
