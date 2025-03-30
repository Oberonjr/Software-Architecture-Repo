using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script that manages the healthBar and spawning floating texts on enemy.TakeDamage() and .Die()
[RequireComponent(typeof(Animator))]
public class EnemyVisualsHandler : MonoBehaviour
{
    [SerializeField] private GameObject enemyVFX;
    [SerializeField] private FloatingText floatingTextPrefab;
    [SerializeField] private Slider healthBar;
    private Camera _camera;
    private Canvas _localCanvas;
    void Start()
    {
        _camera = Camera.main;
        _localCanvas = GetComponentInChildren<Canvas>();
        healthBar.transform.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        _localCanvas.transform.rotation = Quaternion.LookRotation(transform.position - _camera.transform.position);
    }

    public void SetHealth(float health)
    {
        healthBar.maxValue = health;
        healthBar.value = health;
    }
    
    public void UpdateHealth(float health)
    {
        healthBar.transform.gameObject.SetActive(true);
        healthBar.value = health;
    }

    public void ShowDamage(int damage)
    {
        SpawnFloatingObject("-" + damage, Color.red);
    }

    public void ShowGold(int gold)
    {
        healthBar.transform.gameObject.SetActive(false);
        enemyVFX.SetActive(false);
        SpawnFloatingObject("+" + gold, Color.yellow);
    }

    void SpawnFloatingObject(string text, Color color)
    {
        FloatingText floatingObject = Instantiate(floatingTextPrefab, _localCanvas.transform);
        floatingObject.SetText(text, color);
    }
}
