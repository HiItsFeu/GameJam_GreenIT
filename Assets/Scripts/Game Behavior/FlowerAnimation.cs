using System.Collections;
using UnityEngine;

public class FlowerAnimation : MonoBehaviour
{
    public bool newFlower = true;
    public Vector2 center = new Vector2(0.5f, 0.5f);
    public ParticleSystem flowerPS;

    [Header("Grayscale targets")]
    public Renderer[] targetRenderers;

    [Header("Audio Effect settings")]
    public AudioSource success;
    
    private Material[] grayScaleMaterials;
    private Material grayScaleMaterial;
    private Coroutine currentTween;

    private void Start()
    {
        grayScaleMaterials = new Material[targetRenderers.Length];

        for (int i = 0; i < targetRenderers.Length; i++)
        {
            grayScaleMaterials[i] = targetRenderers[i].material;
            grayScaleMaterials[i].SetVector("_Center", center);
            grayScaleMaterials[i].SetFloat("_Radius", 0f);
        }
    }


    public void Launch()
    {
        if (!newFlower) return;
        newFlower = false;
        success.Play();
        flowerPS.transform.position = transform.position;
        flowerPS.Emit(50);
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

        float[] startProgress = new float[grayScaleMaterials.Length];
        for (int i = 0; i < grayScaleMaterials.Length; i++)
            startProgress[i] = grayScaleMaterials[i].GetFloat("_Radius");

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            float progress = Mathf.Lerp(0f, targetProgress, smoothT);

            foreach (Material mat in grayScaleMaterials)
                mat.SetFloat("_Radius", progress);

            yield return null;
        }

        foreach (Material mat in grayScaleMaterials)
            mat.SetFloat("_Radius", targetProgress);

        currentTween = null;
    }
}
