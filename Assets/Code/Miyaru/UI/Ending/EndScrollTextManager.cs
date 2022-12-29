using System.Collections;
using UnityEngine;

public class EndScrollTextManager : MonoBehaviour
{
    public ScrollTextWindow textWindow;
    public CanvasGroupFader blackScreenFader;
    public string[] sentences;

    int currentIndex = -1;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        ReadNextSentence();
    }

    void Update()
    {
        if (canRead && Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            ReadNextSentence();
        }
    }

    void ReadNextSentence()
    {
        currentIndex++;
        string next = "";
        if (currentIndex < sentences.Length)
        {
            next = sentences[currentIndex];
            textWindow.Show(next);
        }
        else //Reached end
        {
            textWindow.Close();
            blackScreenFader.FadeTo100();
        }
    }

    bool canRead => textWindow != null && sentences.Length > 0 && currentIndex < sentences.Length;
}