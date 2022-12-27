using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    Stats stats;

    public bool HasStats { get => stats; }


    void Awake() {
        stats = GetComponent<Stats>();
    } 

    public void TakeDamage(int amount) {
        if (HasStats) {
            stats.TakeDamage(amount);
        }
    }

}