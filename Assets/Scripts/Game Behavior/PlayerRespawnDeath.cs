using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerRespawnDeath : MonoBehaviour
{
    public Vector3 respawnPoint;
    public ParticleSystem deathPS;
    Rigidbody2D playerRb;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        respawnPoint = transform.position;
    }

    IEnumerator Respawn(float duration)
    {
        playerRb.simulated = false;
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(duration);
        transform.position = respawnPoint;
        playerRb.simulated = true;
        spriteRenderer.enabled = true;
    }

    public void Kill()
    {
        deathPS.transform.position = transform.position;
        deathPS.Emit(20);
        StartCoroutine(Respawn(1f));
    }
}
