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
    //[SerializeField]
    //bool dealDamageOverTime = false;
    //[SerializeField, HideIf("@!dealDamageOverTime")]
    //float damageOverTimeInterval = 0.5f;

    //List<Stats> damageOverTimeTargets = new List<Stats>();

    List<Stats> hurtTargets = new List<Stats>();

    [SerializeField]
    UnityEvent onDamageDealt;
    [SerializeField]
    bool disableOnDamageDealt;
    /*
    private void OnCollisionEnter2D(Collision2D collision) {
        OnEnterDamageCollision(collision.gameObject.GetComponent<Stats>());
    }
    private void OnCollisionExit2D(Collision2D collision) {
        OnExitDamageCollision(collision.gameObject.GetComponent<Stats>());
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log(collision.gameObject.name);
        Stats s = collision.gameObject.GetComponent<Stats>();
        Debug.Log(s);
        OnEnterDamageCollision(s);
    }


    private void OnTriggerExit2D(Collider2D collision) {
        OnExitDamageCollision(collision.gameObject.GetComponent<Stats>());
    }

    
    void OnEnterDamageCollision(Stats target) {
        if(target == owner) {
            Debug.Log("Was owner");
            return;
        }
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
        if (target == owner) {
            return;
        }
        if (target && dealDamageOverTime) {
            damageOverTimeTargets.Remove(target);
        }
    }*/

    void Update() {
        Collider2D col = GetComponent<Collider2D>();
        List<Collider2D> hits = col.GetHits();
        if(hits.Count > 0) {
            List<Stats> hitsWithStats = hits.WithComponent<Stats>();
            
            foreach(Stats stats in hitsWithStats) {
                Damage(stats);
            }
        }
    }

    void Damage(Stats target) {
        //Debug.Log(target?.gameObject.name);
        if (target == owner || hurtTargets.Contains(target)) {
            return;
        }
        if (target) {
            int targetDamage = owner ? Formulas.CalculateDamage(owner, target) : damage;
            target.TakeDamage(targetDamage);
            onDamageDealt.Invoke();
            if (disableOnDamageDealt) {
                gameObject.SetActive(false);
            }
            hurtTargets.Add(target);
        }
    }

    void OnDisable() {
        hurtTargets.Clear();
    }

}