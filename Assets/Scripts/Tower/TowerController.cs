using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerController : MonoBehaviour, IPointerClickHandler
{
    private enum TargetingType
    {
        FIRST,
        LAST,
        CLOSE,
        ALL
    }

    [SerializeField] private TowerStats towerStats;
    [SerializeField] private TargetingType _currentTargeting;
    
    [SerializeField] private AbstractAttacker _attacker;
    [SerializeField] private Transform _shootingPoint;
    
    private EnemyCheck _enemyCheck;
    private RangeIndicatorManager _rangeIndicatorManager;

    private List<Transform> target = new List<Transform>();

    public TowerStats TowerStats {get => towerStats;}
    
    void Start()
    {
        //Move all this to an OnTowerPlaced() that triggers from an event
        InvokeRepeating(nameof(AttackRepeating), 0f, towerStats.stats.towerAttackInterval);
        _enemyCheck = GetComponentInChildren<EnemyCheck>();
        _rangeIndicatorManager = GetComponentInChildren<RangeIndicatorManager>();
        _enemyCheck.Range.radius = towerStats.stats.towerRange;
        _enemyCheck.OnEnemyEnterRange += UpdateEnemy;
    }

    void OnDestroy()
    {
        _enemyCheck.OnEnemyEnterRange -= UpdateEnemy;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        EventBus<SelectTowerEvent>.Publish(new SelectTowerEvent(this));
    }

    public void ToggleRangeIndicator()
    {
        _rangeIndicatorManager.ToggleRangeIndicator(this);
    }
    
    private void UpdateEnemy(Transform firstEnemy, Transform lastEnemy, Transform closestEnemy, List<EnemyStats> allEnemies)
    {
        target.Clear();
        switch (_currentTargeting)
        {
            case TargetingType.FIRST:
                target.Add(firstEnemy.transform);
                break;
            case TargetingType.LAST:
                target.Add(lastEnemy.transform);
                break;
            case TargetingType.CLOSE:
                target.Add(closestEnemy.transform);
                break;
            case TargetingType.ALL:
                foreach (EnemyStats enemy in allEnemies)
                {
                    target.Add(enemy.transform);
                }
                break;
            default:
                target.Add(firstEnemy.transform);
                break;
        }
        
    }
    
    private void AttackRepeating()
    {
        if (target.Count > 0)
        {
            List<GameObject> targetPositions = new List<GameObject>();
            foreach (Transform targetLocation in target)
            {
                if(targetLocation != null) targetPositions.Add(targetLocation.gameObject);
            }
            _attacker.Attack(transform, towerStats, targetPositions);    
            
        }
    }

    
}
