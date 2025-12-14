using UnityEngine;

public class CamZoom : MonoBehaviour
{
    [Header("Refs")]
    public Collider2D flowerCollider;
    public CameraFollow cameraFollow;
    private FlowerAnimation flowerAnimation;

    [Header("Zoom settings")]
    private float zoomInDistance = 5f;
    private float zoomOutDistance = 1f;
    private float zoomFactor = 1.75f;
    private float zoomDuration = 0.5f;

    private bool isZoomed = false;
    private void Start()
    {
        flowerAnimation = flowerCollider.GetComponent<FlowerAnimation>();
    }

    void Update()
    {
        if (!flowerCollider || !cameraFollow)
            return;

        // Point réel le plus proche sur la flower
        Vector2 closestPoint = flowerCollider.ClosestPoint(transform.position);

        float distance = Vector2.Distance(transform.position, closestPoint);

        // Approche -> zoom
        if (flowerAnimation.newFlower && distance <= zoomInDistance && !isZoomed)
        {
            cameraFollow.TweenZoom(zoomFactor, zoomDuration * 2);
            isZoomed = true;
        }
        // Éloignement -> dezoom
        else if (!flowerAnimation.newFlower || distance >= zoomOutDistance && isZoomed)
        {
            cameraFollow.TweenZoom(0.8f, zoomDuration * 5);
            isZoomed = false;
        }
    }
}
