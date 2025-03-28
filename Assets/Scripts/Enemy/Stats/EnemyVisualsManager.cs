using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class EnemyVisualsManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyVFX;
    [SerializeField] private FloatingText floatingTextPrefab;
    [SerializeField] private Slider healthBar;
    private Animator animator;
    private Camera camera;
    private Canvas localCanvas;
    void Start()
    {
        camera = Camera.main;
        localCanvas = GetComponentInChildren<Canvas>();
        animator = GetComponent<Animator>();
        healthBar.transform.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        localCanvas.transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position);
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
        FloatingText floatingObject = Instantiate(floatingTextPrefab, localCanvas.transform);
        floatingObject.SetText(text, color);
    }
}
