
using System.Collections;
using UnityEngine;

public class ThisManPainting : MonoBehaviour
{
    [SerializeField] PlayerInteractionTrigger playerTrigger;

    bool opened;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        if (playerTrigger == null)
        {
            Debug.Log("Missing the trigger script used to toggle the painting.");
        }
        else
        {
            playerTrigger.OnInteract += TogglePainting;
            playerTrigger.OnPlayerExit += ClosePainting;
        }
    }

    // Update is called once per frame
    void TogglePainting()
    {
        opened = !opened;

        anim.Play(opened ? "Open" : "Close");
    }

    void ClosePainting()
    {
        if (opened)
        {
            anim.Play("Close");
            opened = false;
        }
    }
}