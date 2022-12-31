using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DracGameManager : Singleton<DracGameManager> {

    public List<GameObject> disableOnQuit;
    public List<GameObject> enableOnQuit;

    public GameObject gameOverWindow;
    public GarlicSpawner spawner;
    public Drac drac;
    public int score;
    public UnityEvent<int> onScoreChange;

    public void Lost() {
        gameOverWindow.SetActive(true);
    }

    public void Retry() {
        gameOverWindow.SetActive(false);
        spawner.DestroyChildren();
        drac.Reset();
        score = 0;
        onScoreChange.Invoke(score);
    }

    public void IncrementScore() {
        score++;
        onScoreChange.Invoke(score);
    }

    public void Quit() {
        foreach(GameObject go in disableOnQuit) {
            go.SetActive(false);
        }
        foreach(GameObject go in enableOnQuit) {
            go.SetActive(true);
        }
        gameObject.SetActive(false);
    }


}