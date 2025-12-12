using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerRespawnDeath playerRespawnDeath;

    void Start()
    {
        playerRespawnDeath = GetComponent<PlayerRespawnDeath>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Death")) // Kill the player
        {
            Debug.Log("Player has collided with an enemy!");
            playerRespawnDeath.Kill();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Flower")) // Launch the flower animation
        {
            Debug.Log("Player has touched a flower!");
            playerRespawnDeath.respawnPoint = collision.gameObject.transform.position;
            //collision.gameObject.LaunchCinematic();
        }
    }
}
