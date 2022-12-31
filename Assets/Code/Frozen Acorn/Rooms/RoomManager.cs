using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RoomManager : MonoBehaviour {


    public void LeaveBedroom() {
        SceneManager.LoadScene(2);
    }

    public void EnterBedroom() {
        SceneManager.LoadScene(1);
    }

    public void PlayFlappyDrac() {
        SceneManager.LoadScene(3);
    }
}