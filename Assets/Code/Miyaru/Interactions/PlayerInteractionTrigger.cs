using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInteractionTrigger : MonoBehaviour
{
    public delegate void OnInteractHandler();
    public event OnInteractHandler OnInteract;
    public delegate void OnEnterHandler();
    public event OnEnterHandler OnPlayerEnter;
    public delegate void OnExitHandler();
    public event OnExitHandler OnPlayerExit;

    [SerializeField]UnityEvent onInteract;
    [SerializeField]UnityEvent onPlayerEnter;
    [SerializeField]UnityEvent onPlayerExit;

    bool playerNear;


    void Update()
    {
        if (playerNear && pressedActionKey)
        {
            OnInteract?.Invoke();
            onInteract.Invoke();
            SfxManager.Instance?.Play_UISelect();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("player entered trigger zone");
            playerNear = true;
            OnPlayerEnter?.Invoke();
            onPlayerEnter.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("player exited trigger zone");
            playerNear = false;
            OnPlayerExit?.Invoke();
            onPlayerExit.Invoke();
        }
    }

    //(Keyboard.current.jKey.wasPressedThisFrame)
    bool pressedActionKey => Input.GetKeyDown(KeyCode.Return);
}
