using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Drac : MonoBehaviour {

    public float velocity = 1.4f;

    Rigidbody2D rb;

    Vector2 startingPosition;

    public UnityEvent onDeath;

    bool dead;
    public AudioSource audioSource;
    public AudioClip tapClip;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }

    public void Reset() {
        rb.velocity = Vector2.zero;
        transform.position = startingPosition;
        dead = false;
    }

    void Update() {
        if(!dead && Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
            rb.velocity = Vector2.up * velocity;
            audioSource.pitch = Random.Range(1.3f, 1.8f);
            audioSource.PlayOneShot(tapClip);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!dead) {
            dead = true;
            onDeath.Invoke();
        }
    }

}