using System.Collections;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Material transitionMaterial;

    private Coroutine currentTween;

    public void ChangeScene(bool inverted, float duration)
    {
        if (currentTween != null)
            StopCoroutine(currentTween);

        float feather = transitionMaterial.GetFloat("_Feather");
        transitionMaterial.SetFloat("_Progress", inverted ? 0f - feather : 1f + feather);
        currentTween = StartCoroutine(AnimateTransition(inverted, duration));
    }
    public void BlackScene()
    {
        if (currentTween != null)
            StopCoroutine(currentTween);

        float feather = transitionMaterial.GetFloat("_Feather");
        transitionMaterial.SetFloat("_Progress", 1f + feather);
    }

    private IEnumerator AnimateTransition(bool inverted, float duration)
    {
        float elapsed = 0f;
        float startProgress = transitionMaterial.GetFloat("_Progress");
        float targetProgress = inverted ? 1f : 0f;

        transitionMaterial.SetFloat("_Invert", inverted ? 1f : 0f);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            float smoothT = Mathf.SmoothStep(0f, 1f, t);
            float progress = Mathf.Lerp(startProgress, targetProgress, smoothT);

            transitionMaterial.SetFloat("_Progress", progress);

            yield return null;
        }

        transitionMaterial.SetFloat("_Progress", targetProgress);
        currentTween = null;
    }
}
