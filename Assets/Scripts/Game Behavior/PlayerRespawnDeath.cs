using System.Collections;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerRespawnDeath : MonoBehaviour
{
    public Vector3 respawnPoint;
    public ParticleSystem deathPS;
    public TMP_Text infoText;
    public Animator animator;
    Rigidbody2D playerRb;
    SpriteRenderer spriteRenderer;

    [Header("Audio Effect settings")]
    public AudioSource death;
    private void Update()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(respawnPoint);
        infoText.rectTransform.position = screenPos + new Vector2(40, 40);
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        respawnPoint = transform.position;
    }

    IEnumerator Respawn(int duration)
    {
        playerRb.simulated = false;
        infoText.enabled = true;
        //spriteRenderer.enabled = false;

        for (int i = 0; i < duration; i++) {
            infoText.text = string.Format("Respawn in {0}s", duration - i);
            yield return new WaitForSeconds(1);
        }

        animator.SetBool("is_alive", true);
        transform.position = respawnPoint;
        infoText.enabled = false;
        playerRb.simulated = true;
        //spriteRenderer.enabled = true;
    }

    public void Kill()
    {
        death.Play();
        animator.SetBool("is_alive", false);
        deathPS.transform.position = transform.position;
        deathPS.Emit(20);
        StartCoroutine(Respawn(3));
    }
}
