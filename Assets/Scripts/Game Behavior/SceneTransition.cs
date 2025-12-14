using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Material transitionMaterial;
    public string levelName;
    public float duration;
    private bool isNew = true;

    public void ChangeScene()
    {
        if (!isNew) return;
        isNew = false;
        StartCoroutine(ChangeSceneRoutine());
    }

    IEnumerator ChangeSceneRoutine()
    {
        yield return StartCoroutine(AnimateTransition(true, duration));
        SceneManager.LoadScene(levelName);
        yield return null;
    }

    public void BlackScene()
    {
        float feather = transitionMaterial.GetFloat("_Feather");
        transitionMaterial.SetFloat("_Progress", 1f + feather);
    }

    private IEnumerator AnimateTransition(bool inverted, float duration)
    {
        float elapsed = 0f;
        float feather = transitionMaterial.GetFloat("_Feather");
        float startProgress = inverted ? 0f - feather : 1f + feather;
        float targetProgress = inverted ? 1f + feather : 0f - feather;

        transitionMaterial.SetFloat("_Progress", startProgress);
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
    }
    private void Start()
    {
        BlackScene();
        StartCoroutine(AnimateTransition(false, duration));
    }
}
