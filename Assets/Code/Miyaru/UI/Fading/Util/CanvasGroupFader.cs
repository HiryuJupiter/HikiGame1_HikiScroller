using System.Collections;
using UnityEngine;

public class CanvasGroupFader : MonoBehaviour
{
    [SerializeField] bool fadeInOnStart;
    [SerializeField] bool fadeOutOnStart;

    [SerializeField] float fadeSpeed = 1f;

    bool fadingTo0;
    bool fadingTo100;
    CanvasGroup cvsGrp;


    void Awake()
    {
        cvsGrp = GetComponent<CanvasGroup>();

        
    }

    IEnumerator Start()
    {
        if (fadeInOnStart)
        {
            Instant0();
            yield return new WaitForSeconds(0.5f);
            FadeTo100();
        }

        if (fadeOutOnStart)
        {
            Instant100();
            yield return new WaitForSeconds(0.5f);
            FadeTo0();
        }
    }

    void Update()
    {
        if (fadingTo0)
        {
            if (cvsGrp.alpha > 0)
            {
                cvsGrp.alpha -= Time.deltaTime * fadeSpeed;
                if (cvsGrp.alpha <= 0f)
                {
                    fadingTo0 = false;
                }
            }
        }
        else if (fadingTo100)
        {
            if (cvsGrp.alpha < 1)
            {
                cvsGrp.alpha += Time.deltaTime * fadeSpeed;
                if (cvsGrp.alpha >= 1f)
                {
                    fadingTo100 = false;
                    cvsGrp.blocksRaycasts = true;
                    cvsGrp.interactable = true;
                }
            }
        }
    }

    public void FadeTo100()
    {
        fadingTo100 = true;
    }

    public void FadeTo0()
    {
        fadingTo0 = true;
        cvsGrp.blocksRaycasts = false;
        cvsGrp.interactable = false;
    }

    public void Instant0()
    {
        cvsGrp.blocksRaycasts = false;
        cvsGrp.interactable = false;
        cvsGrp.alpha = 0f;
    }

    public void Instant100()
    {
        cvsGrp.blocksRaycasts = true;
        cvsGrp.interactable = true;
        cvsGrp.alpha = 1f;
    }
}