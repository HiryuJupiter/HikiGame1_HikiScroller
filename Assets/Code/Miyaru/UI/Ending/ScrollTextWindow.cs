using System.Collections;
using UnityEngine;
using TMPro;

public class ScrollTextWindow : MonoBehaviour
{
    public TMP_Text Text;
    string currentText;

    CanvasGroup cvs;

    void Start()
    {
        cvs = GetComponent<CanvasGroup>();
        cvs.alpha = 0;
    }

    public void Show(string text)
    {
        cvs.alpha = 1;
        currentText = text;
        StartCoroutine(DoDisplayText());
    }

    public void Close ()
    {
        cvs.alpha = 0;
        StopAllCoroutines();
    }

    IEnumerator DoDisplayText()
    {
        Text.text = "";

        foreach (char c in currentText.ToCharArray())
        {
            Text.text += c;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
