using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {


    protected Stats stats;

    public bool HasStats { get => stats; }


    void Awake() {
        stats = GetComponent<Stats>();
    } 

    public virtual void TakeDamage(int amount) {
        if (HasStats) {
            stats.TakeDamage(amount);
        }
    }

}