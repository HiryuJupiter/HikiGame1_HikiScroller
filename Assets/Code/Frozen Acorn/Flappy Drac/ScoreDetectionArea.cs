using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDetectionArea : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        DracGameManager.Instance.IncrementScore();
    }

}
