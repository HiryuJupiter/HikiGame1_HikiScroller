using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class PauseMenuUI : MonoBehaviour
{
    //Event
    public delegate void OnPause(bool isTrue);
    public static OnPause onPause;

    public static bool IsPaused;

    [SerializeField]
    int menuLevelIndex = 0;
    [SerializeField]
    float timeBeforeEnabled = 2.5f; //Need to wait for Generic black fader to fade it.

    //Canvas groups
    [SerializeField] CanvasGroupFader pauseMenu;
    [SerializeField] CanvasGroupFader blackScreen;

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

    public void Quit() {
        Application.Quit();
    }

    public void TogglePause()
    {
        if (!interactable)
            return;

        IsPaused = !IsPaused;
        onPause?.Invoke(IsPaused);

        if (IsPaused)
        {
            pauseMenu.Instant100();
        }
        //Unpause
        else
        {
            pauseMenu.Instant0();
        }
    }
    #endregion

    #region Private methods
    IEnumerator DoBackToMain()
    {
        blackScreen.FadeTo100();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(menuLevelIndex);
    }
    #endregion
}
