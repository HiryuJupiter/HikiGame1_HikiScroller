using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Hallway : MonoBehaviour {

    public Transform player;

    public GameObject exitSectionPrefab;
    public List<GameObject> hallwaySectionPrefabs;

    public GameObject startingSection;

    [ReadOnly]
    public List<GameObject> activelySpawnedSections;

    public int targetSectionCount = 7;

    public float sectionDespawnDistance = 40;

    int spawnedRoomsSinceLastExit;
    public int exitFrequency = 10;

    public float distanceBetweenHallways = 10;

    void Awake() {
        activelySpawnedSections.Add(startingSection);
    }

    void Update() {
        //Do we need to spawn additional sections?
        if(activelySpawnedSections.Count < targetSectionCount) {
            //Is the player closer to the right or left side of the hallway
            int sectionsToLeft = activelySpawnedSections.FindAll((section) => {
                return section.transform.position.x < player.position.x;
            }).Count;

            bool moreOnLeft = sectionsToLeft > activelySpawnedSections.Count - sectionsToLeft;

            //If there's more on the left, spawn a section on the right
            if (moreOnLeft) {
                SpawnSection(GetEmptyRightPosition());
            } else {
                //else spawn a section on the left
                SpawnSection(GetEmptyLeftPosition());
            }
        }
        //If sections have left spawn range then remove them
        List<GameObject> sectionsToDespawn = activelySpawnedSections.FindAll((section) => {
            return Vector2.Distance(section.transform.position, player.transform.position) > sectionDespawnDistance;
        });
        foreach(GameObject sectionToDespawn in sectionsToDespawn) {
            activelySpawnedSections.Remove(sectionToDespawn);
            Destroy(sectionToDespawn);
        }
    }

    Vector2 GetEmptyRightPosition() {
        Vector2 rightMostSectionPosition = new Vector2(-100000000, 0);
        foreach(GameObject section in activelySpawnedSections) {
            if(section.transform.position.x > rightMostSectionPosition.x) {
                rightMostSectionPosition = section.transform.position;
            }
        }
        rightMostSectionPosition.x += distanceBetweenHallways;
        return rightMostSectionPosition;
    }
    Vector2 GetEmptyLeftPosition() {
        Vector2 leftMostSectionPosition = new Vector2(100000000, 0);
        foreach (GameObject section in activelySpawnedSections) {
            if (section.transform.position.x < leftMostSectionPosition.x) {
                leftMostSectionPosition = section.transform.position;
            }
        }
        leftMostSectionPosition.x -= distanceBetweenHallways;
        return leftMostSectionPosition;
    }

    void SpawnSection(Vector2 position) {
        spawnedRoomsSinceLastExit++;
        if(spawnedRoomsSinceLastExit > exitFrequency) {
            //Spawn exit
            GameObject exit = Instantiate(exitSectionPrefab);
            exit.transform.position = position;
            spawnedRoomsSinceLastExit = 0;
            activelySpawnedSections.Add(exit);
        } else {
            GameObject room = Instantiate(hallwaySectionPrefabs.Random());
            room.transform.position = position;
            activelySpawnedSections.Add(room);
        }

    }

}