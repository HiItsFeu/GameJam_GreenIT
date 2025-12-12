using UnityEngine;

public class PlayerRespawnDeath : MonoBehaviour
{
    public Vector3 respawnPoint;

    public void Respawn()
    {
        Debug.Log("Player has respawn!");
        // effect
        transform.position = respawnPoint;
    }

    public void Kill()
    {
        Debug.Log("Player has died!");
        // effect timer and other thing
        Respawn();
    }
}
