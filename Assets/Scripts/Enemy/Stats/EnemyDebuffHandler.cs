using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The conditions that can affect an enemy
public enum Conditions
{
    SLOW,
    BURNING,
    BLEEDING
}

/*
 * Script that determines the effect of each effect on the enemy
 * Handles applying the effects for the appropriate amount of time
 * as well as clearing the effects when necessary 
 */
public class EnemyDebuffHandler: MonoBehaviour
{
    public List<Conditions> currentConditions = new List<Conditions>();

    public void ApplyCondition(EnemyStats targetStats, ConditionParameters cp)
    {
        if (!currentConditions.Contains(cp.condition))
        {
            currentConditions.Add(cp.condition);
            switch (cp.condition)
            {
                case Conditions.SLOW:
                    targetStats.EnemyController.ChangeSpeed(cp.intensity, cp.duration);
                    break;
                case Conditions.BURNING:
                    StartCoroutine(ApplyDOT(targetStats, cp));
                    break;
                case Conditions.BLEEDING:
                    StartCoroutine(ApplyDOT(targetStats, cp));
                    break;
                default:
                    Debug.LogError("Unknown condition. Fix your damn code");
                    break;
            }
        }
    }

    public void ApplyCondition(EnemyStats targetStats, Conditions condition, int intensity, float duration)
    {
        ConditionParameters cp = new ConditionParameters();
        cp.condition = condition;
        cp.intensity = intensity;
        cp.duration = duration;
        ApplyCondition(targetStats, cp);
    }

    IEnumerator ApplyDOT(EnemyStats stats, ConditionParameters cp, float tickInterval = 1f)
    {
        float elapsedTime = 0f;
        while (elapsedTime < cp.duration)
        {
            stats.TakeDamage(cp.intensity);
            switch (cp.condition)
            {
                case Conditions.BURNING:
                    stats.DamageVulnerability = 1;
                    break;
                case Conditions.BLEEDING:
                    ApplyCondition(stats, Conditions.SLOW, (int)stats.EnemyController.maxSpeed / 2, cp.duration - elapsedTime);
                    break;
                default:
                    Debug.LogError("Wrong condition being applied. Fix your damn code.");
                    break;
            }
            yield return new WaitForSeconds(tickInterval);
            elapsedTime += tickInterval;
        }
        ClearConditions(cp.condition);
    }

    public void ClearConditions(Conditions condition)
    {
        if (currentConditions.Contains(condition))
        {
            currentConditions.Remove(condition);
        }
    }
}
