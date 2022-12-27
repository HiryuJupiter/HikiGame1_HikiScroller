using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuSceneUIManager : MonoBehaviour
{
    #region Fields
    public CanvasGroupFader cvsMenu;
    public CanvasGroupFader cvsInfo;
    public CanvasGroupFader blackScreen;

    Animator anim;
    #endregion

    #region Start
    void Awake()
    {
        anim = GetComponent<Animator>();

        
    }

    private void Start()
    {
        cvsMenu.Instant0();
        cvsInfo.Instant0();
        cvsMenu.FadeTo100();
    }
    #endregion

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void InfoToMain()
    {
        cvsMenu.FadeTo100();
        cvsInfo.FadeTo0();
    }

    public void ToInfo()
    {
        cvsMenu.FadeTo0();
        cvsInfo.FadeTo100();
    }
    public void ToQuitGame() => StartCoroutine(DoQuitGame());

    IEnumerator DoQuitGame ()
    {
        blackScreen.FadeTo100();
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }
}