using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;

public class DamageDealer : MonoBehaviour {

    [SerializeField]
    Stats owner;
    [HideIf("@owner != null"), SerializeField]
    int damage = 1;

    [SerializeField]
    bool dealDamageOnCollisionEnter = true;
    [SerializeField]
    bool dealDamageOverTime = false;
    [SerializeField, HideIf("@!dealDamageOverTime")]
    float damageOverTimeInterval = 0.5f;

    List<Stats> damageOverTimeTargets = new List<Stats>();

    [SerializeField]
    UnityEvent onDamageDealt;

    private void OnCollisionEnter2D(Collision2D collision) {
        OnEnterDamageCollision(collision.gameObject.GetComponent<Stats>());
    }
    private void OnCollisionExit2D(Collision2D collision) {
        OnExitDamageCollision(collision.gameObject.GetComponent<Stats>());
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        OnEnterDamageCollision(collision.gameObject.GetComponent<Stats>());
    }


    private void OnTriggerExit2D(Collider2D collision) {
        OnExitDamageCollision(collision.gameObject.GetComponent<Stats>());
    }


    void OnEnterDamageCollision(Stats target) {
        if (dealDamageOnCollisionEnter) {
            Damage(target);
        }
        if (dealDamageOverTime) {
 
            if (target) {
                damageOverTimeTargets.Add(target);
                this.DoEvery(damageOverTimeInterval,
                    () => {
                        Damage(target);
                    },
                    () => {
                        return !damageOverTimeTargets.Contains(target);
                    }
                );
            }
        }
    }

    void OnExitDamageCollision(Stats target) {
        if (target && dealDamageOverTime) {
            damageOverTimeTargets.Remove(target);
        }
    }

    void Damage(Stats target) {
        if (target) {
            int targetDamage = owner ? Formulas.CalculateDamage(owner, target) : damage;
            target.TakeDamage(targetDamage);
            onDamageDealt.Invoke();
        }
    }

}