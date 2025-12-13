using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerCollision : MonoBehaviour
{
    private PlayerRespawnDeath playerRespawnDeath;

    void Start()
    {
        playerRespawnDeath = GetComponent<PlayerRespawnDeath>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            playerRespawnDeath.Kill();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Flower"))
        {
            FlowerAnimation targetScript = collision.gameObject.GetComponent<FlowerAnimation>();
            targetScript.Launch();
            playerRespawnDeath.respawnPoint = collision.gameObject.transform.position;
        }
    }
}
