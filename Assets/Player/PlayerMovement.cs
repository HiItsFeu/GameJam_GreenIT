using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 move;

    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }

    void Update()
    {
        transform.position += (Vector3)move * speed * Time.deltaTime;
    }
}
