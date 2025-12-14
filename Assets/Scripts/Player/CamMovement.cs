using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    public Transform target;

    [Header("Follow")]
    public float smoothSpeed = 5f;
    public float distance = 10f;

    [Header("Camera bounds (XY plane)")]
    public Vector2 minBounds;
    public Vector2 maxBounds;

    [Header("Zoom")]
    public float baseFOV = 60f;

    Camera cam;
    Coroutine zoomCoroutine;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (!target)
            return;

        float clampedX = Mathf.Clamp(target.position.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(target.position.y, minBounds.y, maxBounds.y);

        Vector3 desiredPosition = new Vector3(clampedX, clampedY, target.position.z - distance);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        //transform.LookAt(target);


        //test for zoom cam
        //if (Input.GetKeyDown(KeyCode.C))
        //    {
        //        TweenZoom(1.5f, 0.5f);
        //    }
    }

    public void TweenZoom(float zoomFactor, float duration)
    {
        float targetFOV = baseFOV / Mathf.Max(0.01f, zoomFactor);

        if (zoomCoroutine != null)
            StopCoroutine(zoomCoroutine);

        zoomCoroutine = StartCoroutine(ZoomRoutine(targetFOV, duration));
    }

    IEnumerator ZoomRoutine(float targetFOV, float duration)
    {
        float startFOV = cam.fieldOfView;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            t = 1f - Mathf.Pow(1f - t, 2f); // QuadOut

            cam.fieldOfView = Mathf.Lerp(startFOV, targetFOV, t);
            yield return null;
        }

        cam.fieldOfView = targetFOV;
    }
}