using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource parent;
    [Header("Themes")]
    public AudioSource s1;
    public AudioSource s2;

    [Header("Settings")]
    public bool playOnStart = true;
    public float fadeDuration = 2f;

    AudioSource current;
    AudioSource next;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // 🔊 Jouer un thème avec crossfade
    public void PlayTheme(AudioClip clip)
    {
        next.clip = clip;
        next.volume = 0f;
        next.Play();

        StopAllCoroutines();
        StartCoroutine(CrossFade());
    }
    void Start()
    {
        current = s2;
        next = s1;
        if (playOnStart) PlayTheme(next.clip);
    }

    IEnumerator CrossFade()
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float k = t / fadeDuration;

            current.volume = Mathf.Lerp(1f, 0f, k);
            next.volume = Mathf.Lerp(0f, 1f, k);

            yield return null;
        }

        current.Stop();
        current.volume = 0f;

        // swap
        (current, next) = (next, current);
    }

    // 🔇 Fade out global
    public void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutRoutine());
    }

    IEnumerator FadeOutRoutine()
    {
        float startVolume = current.volume;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            current.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }

        current.Stop();
        Destroy(gameObject);
    }
}