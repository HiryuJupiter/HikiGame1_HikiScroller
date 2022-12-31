using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

    public RangeDetector targetDetectionRange;
    public RangeDetector attackRange;
    public DamageDealer attackDamageDealer;
    public Animator animator;
    public Rigidbody2D rb;
    public SpriteRenderer sr;

    private Entity target;
    public float speed = 3;
    bool isAttacking;
    bool attackInterrupted;
    bool isBeingHurt;
    public float turnChance = 0.01f;
    bool obtainedTargetSight;
    public float attackCooldown = 3;
    float lastAttackTime;
    public float knockbackForce = 3;
    public float spawnTime = 0.6f;
    bool spawned = true;
    public AudioSource audioSource;
    public AudioClip attackSFX;
    public AudioClip ambientSFX;

    private float pitch;
    float lastAmbientTime;

    public void DamageFeedback() {
        if (stats.IsDead) {
            StartCoroutine(Death());
        } else {
            StartCoroutine(Hurt());
        }
    }

    void OnEnable() {
        if (!target) {
            target = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Entity>();
        }
        pitch = Random.Range(0.5f, 1.5f);
        StartCoroutine(Spawn());
    }

    void PlaySound(AudioClip sfx) {
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(sfx);
    }

    IEnumerator Spawn() {
        if (GetComponent<Stats>().IsDead) {
            Destroy(gameObject);
        } else {
            _2dxFX_Smoke smoke = GetComponent<_2dxFX_Smoke>();
            float timePassed = 0;
            float startingValue = smoke._Value2;
            while (smoke._Value2 > 0) {
                smoke._Value2 = Mathf.Lerp(startingValue, 0, timePassed / spawnTime);
                timePassed += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            spawned = true;
            Destroy(smoke);
            gameObject.AddComponent<_2dxFX_DestroyedFX>().Destroyed = 0;
        }
    }
    void OnDisable() {
        this.StopAllCoroutines();
        target = null;
    }

    IEnumerator Attack() {
        lastAttackTime = 0;
        isAttacking = true;
        animator.SetTrigger("Attack");
        attackDamageDealer.gameObject.SetActive(true);
        PlaySound(attackSFX);
        yield return StartCoroutine(WaitForCurrentClipToEnd());
        if (attackInterrupted) {
            attackInterrupted = false;
        } else {
            attackDamageDealer.gameObject.SetActive(false);
            isAttacking = false;
        }
    }

    void Update() {
        
        if (!spawned) {
            return;
        }
        lastAmbientTime += Time.deltaTime;
        if (!isAttacking) {
            lastAttackTime += Time.deltaTime;
        }
        if (isBeingHurt) {
            return;
        }
        if (targetDetectionRange.HasTarget || obtainedTargetSight) {
            obtainedTargetSight = true;
            if (attackRange.HasTarget) {
                //Attack
                rb.velocity = Vector2.zero;
                if (!isAttacking && lastAttackTime > attackCooldown) {
                    StartCoroutine(Attack());
                }
            } else {
                //Move towards Target
                MoveTowardsTarget();
                if(lastAttackTime > 2 && lastAmbientTime > 3) {
                    PlaySound(ambientSFX);
                    lastAmbientTime = 0;
                }
            }
        } else {
            //Idle
            animator.SetBool("IsWalking", false);
            if(Random.Range(0f,100f) < turnChance) {
                sr.flipX = !sr.flipX;
                
            }
        }
    }

    void MoveTowardsTarget() {
        Vector3 targetPosition = target.transform.position;
        Vector2 direction = targetPosition.x < transform.position.x ? Vector2.left : Vector2.right;
        rb.velocity = direction * speed * Time.deltaTime;
        animator.SetBool("IsWalking", true);
        sr.flipX = target.transform.position.x < transform.position.x;
    }

    void ReceiveKnockback() {
        Vector2 direction = Vector2.left;
        if (sr.flipX) {
            direction = -direction;
        }
        direction *= knockbackForce;
        direction = direction + Vector2.up;
        rb.AddForce(direction);
    }

    IEnumerator Hurt() {
        obtainedTargetSight = true;
        isBeingHurt = true;
        if (isAttacking) {
            attackInterrupted = true;
            isAttacking = false;
        }
        animator.SetTrigger("Hurt");
        ReceiveKnockback();
        yield return StartCoroutine(WaitForCurrentClipToEnd());
        isBeingHurt = false;
    }

    IEnumerator WaitForCurrentClipToEnd() {
        yield return new WaitForEndOfFrame();
        float clipTime = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        yield return new WaitForSeconds(clipTime);
    }
    IEnumerator Death() {
        isBeingHurt = true;
        if (isAttacking) {
            attackInterrupted = true;
            isAttacking = false;
        }
        animator.SetTrigger("Death");
        
        yield return StartCoroutine(WaitForCurrentClipToEnd());
        
        _2dxFX_DestroyedFX destroyFX = gameObject.GetComponent<_2dxFX_DestroyedFX>();
        float timePassed = 0;
        float targetTime = spawnTime * 0.5f;
        float startingValue = destroyFX.Destroyed;
        while(destroyFX.Destroyed < 1) {
            destroyFX.Destroyed = Mathf.Lerp(startingValue, 1, timePassed / targetTime);
            yield return new WaitForEndOfFrame();
            timePassed += Time.deltaTime;
        }

        CleanUpDeadEnemy();
    }

    void CleanUpDeadEnemy() {
        gameObject.SetActive(false);
    }
}