using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicSpawner : MonoBehaviour {

    public float spawnFrequency = 1;
    private float timer;
    public GameObject garlicPrefab;
    public float height;

    void Start() {
        SpawnGarlic();
    }

    void Update() {
        if(timer > spawnFrequency) {
            SpawnGarlic();   
        }
        timer += Time.deltaTime;
    }

    void SpawnGarlic() {
        timer = 0;
        GameObject garlic = Instantiate(garlicPrefab);
        garlic.transform.position = transform.position + new Vector3(0, Random.Range(-height, height), 0);
        garlic.SetParent(this);
        Destroy(garlic, 15);
    }

    public void DestroyChildren() {
        this.DestroyAllChildren();
    }
}