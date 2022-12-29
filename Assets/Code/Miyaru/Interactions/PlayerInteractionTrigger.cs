using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionTrigger : MonoBehaviour
{
    public delegate void OnInteractHandler();
    public event OnInteractHandler OnInteract;
    public delegate void OnEnterHandler();
    public event OnEnterHandler OnPlayerEnter;
    public delegate void OnExitHandler();
    public event OnExitHandler OnPlayerExit;

    bool playerNear;


    void Update()
    {
        if (playerNear && pressedActionKey)
        {
            OnInteract?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("player entered trigger zone");
            playerNear = true;
            OnPlayerEnter?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("player exited trigger zone");
            playerNear = false;
            OnPlayerExit?.Invoke();
        }
    }

    //(Keyboard.current.jKey.wasPressedThisFrame)
    bool pressedActionKey => (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Return));
}
