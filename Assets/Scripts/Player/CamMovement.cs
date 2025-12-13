using System.Collections;
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
    private Coroutine zoomCoroutine;

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

        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        float clampedX = Mathf.Clamp(target.position.x, minBounds.x + camWidth, maxBounds.x - camWidth);
        float clampedY = Mathf.Clamp(target.position.y, minBounds.y + camHeight, maxBounds.y - camHeight);

        Vector3 desiredPosition = new Vector3(clampedX, clampedY, zOffset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // test for zoom cam
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    TweenZoom(1.5f, 0.5f);
        //}
    }

    public void TweenZoom(float zoomValue, float duration)
    {
        float targetZoom = Zoom / Mathf.Max(0.01f, zoomValue);

        if (zoomCoroutine != null)
            StopCoroutine(zoomCoroutine);

        zoomCoroutine = StartCoroutine(ZoomRoutine(targetZoom, duration));
    }

    IEnumerator ZoomRoutine(float targetZoom, float duration)
    {
        float startZoom = cam.orthographicSize;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            // équivalent Easing Roblox (QuadOut)
            t = 1f - Mathf.Pow(1f - t, 2f);

            cam.orthographicSize = Mathf.Lerp(startZoom, targetZoom, t);
            yield return null;
        }

        cam.orthographicSize = targetZoom;
    }
}
