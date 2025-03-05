using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Conditions
{
    SLOW,
    BURNING,
    BLEEDING
}

public class EnemyDebuffManager: MonoBehaviour
{
    public List<Conditions> currentConditions = new List<Conditions>();

    public void ApplyCondition(EnemyStats targetStats, Conditions condition, int intensity, float duration)
    {
        if (!currentConditions.Contains(condition))
        {
            currentConditions.Add(condition);
            switch (condition)
            {
                case Conditions.SLOW:
                    targetStats.EnemyController.ChangeSpeed(intensity, duration);
                    break;
                case Conditions.BURNING:
                    StartCoroutine(ApplyDOT(condition, targetStats, intensity, duration));
                    break;
                case Conditions.BLEEDING:
                    StartCoroutine(ApplyDOT(condition, targetStats, intensity, duration));
                    break;
                default:
                    Debug.LogError("Unknown condition. Fix your damn code");
                    break;
            }
        }
        
        
    }

    IEnumerator ApplyDOT(Conditions condition, EnemyStats stats, int intensity, float duration, float tickInterval = 1f)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            stats.TakeDamage(intensity);
            switch (condition)
            {
                case Conditions.BURNING:
                    stats.DamageVulnerability = 1;
                    break;
                case Conditions.BLEEDING:
                    stats.EnemyController.ChangeSpeed(stats.EnemyController.maxSpeed / 2, duration - elapsedTime);
                    break;
                default:
                    Debug.LogError("Wrong condition being applied. Fix your damn code.");
                    break;
            }
            yield return new WaitForSeconds(tickInterval);
            elapsedTime += tickInterval;
        }
        ClearConditions(condition);
    }

    public void ClearConditions(Conditions condition)
    {
        if (currentConditions.Contains(condition))
        {
            currentConditions.Remove(condition);
        }
    }
}
