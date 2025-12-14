using System.Collections;
using UnityEngine;

public class FlowerAnimation : MonoBehaviour
{
    public bool newFlower = true;
    public Vector2 center = new Vector2(0.5f, 0.5f);
    public ParticleSystem flowerPS;
    private Material grayScaleMaterial;
    private Coroutine currentTween;

    [Header("Audio Effect settings")]
    public AudioSource success;

    private void Start()
    {
        GameObject backgroundObject = GameObject.Find("background");
        Renderer renderer = backgroundObject.GetComponent<Renderer>();
        grayScaleMaterial = renderer.material;
        grayScaleMaterial.SetVector("_Center", center);
        grayScaleMaterial.SetFloat("_Radius", 0f);
    }

    public void Launch()
    {
        if (!newFlower) return;
        newFlower = false;
        success.Play();
        flowerPS.transform.position = transform.position;
        flowerPS.Emit(20);
        currentTween = StartCoroutine(AnimateGrayScale(5f));

        // sound
        GameObject backgroundObject = GameObject.Find("Audio Source");
        AudioManager audioManager = backgroundObject.GetComponent<AudioManager>();
        audioManager.PlayTheme(audioManager.s2.clip);
    }
    private IEnumerator AnimateGrayScale(float duration)
    {
        float elapsed = 0f;
        float targetProgress = 1f;
        float startProgress = grayScaleMaterial.GetFloat("_Radius");

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            float smoothT = Mathf.SmoothStep(0f, 1f, t);
            float progress = Mathf.Lerp(startProgress, targetProgress, smoothT);

            grayScaleMaterial.SetFloat("_Radius", progress);

            yield return null;
        }

        grayScaleMaterial.SetFloat("_Radius", targetProgress);
        currentTween = null;
    }
}
