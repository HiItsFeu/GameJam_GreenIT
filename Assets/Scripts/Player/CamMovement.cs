using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    public Transform target;

    private float smoothSpeed = 5f;
    private float Zoom = 4f;

    [Header("Camera bounds")]
    public Vector2 minBounds;
    public Vector2 maxBounds;

    private float zOffset;
    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Start()
    {
        zOffset = transform.position.z;
    }

    void LateUpdate()
    {
        if (!target)
            return;

        // Taille visible de la caméra
        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        // Clamp en prenant en compte la taille de la caméra
        float clampedX = Mathf.Clamp(target.position.x, minBounds.x + camWidth, maxBounds.x - camWidth);
        float clampedY = Mathf.Clamp(target.position.y, minBounds.y + camHeight, maxBounds.y - camHeight);
        minBounds.x = -8;
        minBounds.y = -5;
        maxBounds.x = 13;
        maxBounds.y = 6;

        Vector3 desiredPosition = new Vector3(clampedX, clampedY, zOffset);

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }

    public void SetZoom(float zoomValue)
    {
        cam.orthographicSize = Zoom / zoomValue;
    }
}
