using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisManGarlic : MonoBehaviour {

    float frequency = 4;
    public float popTime = 1;
    public float popDistance = 2;

    Coroutine popCoroutine;

    void OnEnable() {
        popCoroutine = StartCoroutine(Pop());
    }
    IEnumerator Pop() {
        Vector2 outPos = transform.position + (transform.forward * popDistance);
        while (true) {
            yield return new WaitForSeconds(frequency);

        }
    }
}
