using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class FadeableUI : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private GraphicRaycaster gr;

    [SerializeField] private float fadeTime = 0.5f;

    private Coroutine fadeCoroutine;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        gr = GetComponent<GraphicRaycaster>();
    }

    public void FadeIn(bool instant)
    {
        gr.enabled = true;
        Fade(1f, instant);
    }

    public void FadeOut(bool instant)
    {
        gr.enabled=false;
        Fade(0f, instant);
    }

    private void Fade(float targetAlpha, bool instant)
    {
        if(instant)
            canvasGroup.alpha = targetAlpha;
        else
        {
            if(fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = StartCoroutine(FadeRoutine(targetAlpha));
        }
    }

    private IEnumerator FadeRoutine(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;

        for(float timer =0f; timer<fadeTime; timer += Time.deltaTime)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, timer/fadeTime);
            yield return null;  
        }
        canvasGroup.alpha = targetAlpha;
        fadeCoroutine = null;
    }
}
