using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class PauseMenuUI : MonoBehaviour
{
    public static bool IsPaused = false;

    [SerializeField]
    float timeBeforeEnabled = 2.5f; //Need to wait for Generic black fader to fade it.

    //Canvas groups
    [SerializeField] CanvasGroupFader pauseMenu;
    [SerializeField] CanvasGroupFader blackScreen;
    [SerializeField] Scene MenuScene;

    bool interactable;

    #region Start Awake 
    void Awake()
    {
        pauseMenu.Instant0();
    }

    IEnumerator Start ()
    {
        yield return new WaitForSeconds(timeBeforeEnabled); //Delay for a bit before player can toggle pause
        interactable = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    #endregion

    #region Public methods
    public void ToStartMenu()
    {
        if (interactable)
        {            
            TogglePause();
            interactable = false;
            StartCoroutine(DoBackToMain());            
        }            
    }

    public void TogglePause()
    {
        if (!interactable)
            return;

        IsPaused = !IsPaused;

        if (IsPaused)
        {
            pauseMenu.Instant100();
            Time.timeScale = 0f;
        }
        //Unpause
        else
        {
            Time.timeScale = 1f;
            pauseMenu.Instant0();
        }
    }
    #endregion

    #region Private methods
    IEnumerator DoBackToMain()
    {
        blackScreen.FadeTo100();
        yield return new WaitForSeconds(blackScreen.FadeDuration);
        SceneManager.LoadScene(0);
    }
    #endregion
}
