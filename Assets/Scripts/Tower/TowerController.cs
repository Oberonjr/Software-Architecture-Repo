using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Main tower script
 * Contains all essential logic and refferences
 * Acts as the refference point for each individual tower
 * Handles logic such as upgrading, attacking, targeting and being clicked
 * Uses IPointerClickHandler to register being clicked on as the Towers are quite tall
 * and trying to get them by reading the clicked node at their base could end up being awkward
 */
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
    private RangeIndicatorHandler rangeIndicatorHandler;
    private TowerAnimationHandler animHandler;

    private System.Action<Vector3> AttackTarget;

    private List<Transform> target = new List<Transform>();

    public TowerStats TowerStats {get => towerStats;}
    
    void Start()
    {
        if (TryGetComponent(out TowerAnimationHandler aM))
        {
            animHandler = aM;
        }
        //Move all this to an OnTowerPlaced() that triggers from an event
        InvokeRepeating(nameof(AttackRepeating), 0f, towerStats.stats.towerAttackInterval);
        _enemyCheck = GetComponentInChildren<EnemyCheck>();
        rangeIndicatorHandler = GetComponentInChildren<RangeIndicatorHandler>();
        _enemyCheck.Range.radius = towerStats.stats.towerRange;
        _enemyCheck.OnEnemyEnterRange += UpdateEnemy;
        _enemyCheck.OnEnemyLeaveRange += RemoveEnemyTarget;
        if (animHandler != null)
        {
            AttackTarget += animHandler.RotateToTarget;
        }
    }

    void OnDestroy()
    {
        _enemyCheck.OnEnemyEnterRange -= UpdateEnemy;
        _enemyCheck.OnEnemyLeaveRange -= RemoveEnemyTarget;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("OnPointerClick");
        EventBus<SelectTowerEvent>.Publish(new SelectTowerEvent(this));
    }

    public void ToggleRangeIndicator()
    {
        rangeIndicatorHandler.ToggleRangeIndicator(this);
    }
    
    private void UpdateEnemy(Transform firstEnemy, Transform lastEnemy, Transform closestEnemy, List<EnemyStats> allEnemies)
    {
        target.Clear();
        switch (_currentTargeting)
        {
            case TargetingType.FIRST:
                if(!target.Contains(firstEnemy)) target.Add(firstEnemy.transform);
                break;
            case TargetingType.LAST:
                if(!target.Contains(lastEnemy)) target.Add(lastEnemy.transform);
                break;
            case TargetingType.CLOSE:
                if(!target.Contains(closestEnemy)) target.Add(closestEnemy.transform);
                break;
            case TargetingType.ALL:
                foreach (EnemyStats enemy in allEnemies)
                {
                    if(!target.Contains(enemy.transform)) target.Add(enemy.transform);
                }
                break;
            default:
                if(!target.Contains(firstEnemy)) target.Add(firstEnemy.transform);
                break;
        }
        
    }

    private void RemoveEnemyTarget(Transform enemy)
    {
        if(target.Contains(enemy)) target.Remove(enemy);
    }

    public void UpgradeTower()
    {
        Node currentNode = GridManager.Instance.GetNode(transform.position);
        TowerController upgradeTower = Instantiate(towerStats.stats.upgradeTower, currentNode.GridPosition, Quaternion.identity);
        EconomyManager.Instance.SpendMoney(upgradeTower.TowerStats.stats.cost);
        currentNode.placedObject = upgradeTower.gameObject;
        Destroy(this.gameObject);
    }
    
    private void AttackRepeating()
    {
        if (target.Count > 0)
        {
            List<GameObject> targetPositions = new List<GameObject>();
            
            for (int i = 0; i < towerStats.stats.maxIndividualTargets; i++)
            {
                if(i >= target.Count) continue;
                if(target[i] != null) targetPositions.Add(target[i].gameObject);
            }

            if (animHandler != null)
            {
                foreach (GameObject targetLocation in targetPositions)
                {
                    AttackTarget?.Invoke(targetLocation.transform.position);
                }
            }
            _attacker.Attack(_shootingPoint, towerStats, targetPositions);    
            
        }
    }

    
}
