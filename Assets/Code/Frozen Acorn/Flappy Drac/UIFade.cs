using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIFade : MonoBehaviour {

    Image image;
    [SerializeField]
    float fadeTime = 0.4f;

    [SerializeField]
    bool fadeOutOnEnable;

    [SerializeField]
    UnityEvent onFadeOutComplete;

    void OnEnable() {
        if (fadeOutOnEnable) {
            FadeOut();
        }
    }

    void Awake() {
        image = GetComponent<Image>();
    }

    public void FadeOut() {
        StartCoroutine(FadeOutRoutine());
    }

    IEnumerator FadeOutRoutine() {
        Color currentColor = image.color;
        float progress = 0;
        float timePassed = 0;
        while(currentColor.a > 0) {
            currentColor.a = Mathf.Lerp(1, 0, progress);
            yield return new WaitForFixedUpdate();
            timePassed += Time.fixedDeltaTime;
            progress = timePassed / fadeTime;
        }
        onFadeOutComplete.Invoke();
    }



}