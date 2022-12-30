using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Computer : MonoBehaviour {

    [SerializeField]
    UnityEvent OnComputerTurnedOn;
    [SerializeField]
    UnityEvent OnComputerTurnedOff;

    [SerializeField]
    SpriteRenderer screen;
    [SerializeField]
    float bootUpSpeed = 1;

    [SerializeField]
    Camera camera;
    [SerializeField]
    Vector3 cameraZoomPosition = new Vector3(-0.43f, -1.15f, -10);
    [SerializeField]
    float cameraZoomSize = 0.4108412f;

    [SerializeField]
    float cameraZoomTime = 1;


    bool nearComputer;
    bool isOn;
    

    void Update() {
        if(nearComputer && !isOn && Input.GetKeyDown(KeyCode.Z)) {
            TurnOn();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            nearComputer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            nearComputer = false;
        }
    }
    
    public void TurnOn() {
        isOn = true;
        StartCoroutine(TurnOnComputer());
    }

    IEnumerator TurnOnComputer() {
        Color currentColor = new Color(1,1,1, 0);
        screen.color = currentColor;
        if(bootUpSpeed <= 0) {
            screen.color = Color.white;
        } else {
            while(currentColor.a < 1) {
                currentColor.a += Time.deltaTime * bootUpSpeed;
                screen.color = currentColor;
                yield return new WaitForEndOfFrame();
            }
        }
        yield return ZoomIntoScreen();
        OnComputerTurnedOn.Invoke();
    }

    IEnumerator ZoomIntoScreen() {
        Vector3 cameraStartPosition = camera.transform.position;
        float cameraStartSize = camera.orthographicSize;
        float timePassed = 0;
        float progress = 0;

        while(progress < 1) {
            camera.transform.position = Vector3.Lerp(cameraStartPosition, cameraZoomPosition, progress);
            camera.orthographicSize = Mathf.Lerp(cameraStartSize, cameraZoomSize, progress);
            yield return new WaitForFixedUpdate();
            timePassed += Time.fixedDeltaTime;
            progress = timePassed / cameraZoomTime;
        }

    }

    [Button]
    public void TurnOff() {
        screen.color = new Color(1, 1, 1, 0);
        camera.transform.position = new Vector3(0, 0, -10);
        camera.orthographicSize = 5;
        isOn = false;
        OnComputerTurnedOff.Invoke();
    }

    
}