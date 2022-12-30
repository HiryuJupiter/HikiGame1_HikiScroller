using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stats : MonoBehaviour {

    [SerializeField]
    int strength = 3;
    [SerializeField]
    int maxHealth = 10;
    int health = 10;

    [ShowInInspector]
    public int Health { get => health; }
    public float HealthPercentage { get => (float)health / (float)maxHealth; }
    [ShowInInspector]
    public bool IsDead { get => health <= 0; }
    public int Strength { get => strength; }

    [SerializeField]
    UnityEvent onDeath;
    [SerializeField]
    UnityEvent onDamageTaken;

    void Awake() {
        health = maxHealth;
    }
    [Button]
    public void TakeDamage(int damage) {
        if(!IsDead) {
            health -= damage;
            //Debug.Log(onDamageTaken);
            onDamageTaken.Invoke();
            if (IsDead) {
                onDeath.Invoke();
            }
        }
    }





}