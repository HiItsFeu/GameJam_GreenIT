using UnityEngine;

public class CamZoom : MonoBehaviour
{
    [Header("Refs")]
    public Collider2D flowerCollider;
    public CameraFollow cameraFollow;

    [Header("Zoom settings")]
    private float zoomInDistance = 1f;
    private float zoomOutDistance = 1f;
    private float zoomFactor = 1.5f;
    private float zoomDuration = 0.5f;

    private bool isZoomed = false;

    void Update()
    {
        if (!flowerCollider || !cameraFollow)
            return;

        // Point réel le plus proche sur la flower
        Vector2 closestPoint = flowerCollider.ClosestPoint(transform.position);

        float distance = Vector2.Distance(transform.position, closestPoint);

        // Approche -> zoom
        if (distance <= zoomInDistance && !isZoomed)
        {
            Debug.Log("ZOOM IN");
            cameraFollow.TweenZoom(zoomFactor, zoomDuration);
            isZoomed = true;
        }
        // Éloignement -> dezoom
        else if (distance >= zoomOutDistance && isZoomed)
        {
            Debug.Log("ZOOM OUT");
            cameraFollow.TweenZoom(1f, zoomDuration);
            isZoomed = false;
        }
    }
}
