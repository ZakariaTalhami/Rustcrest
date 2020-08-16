using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{

    public Transform target;

    public Vector3 offset;
    public float currentZoom = 10f;
    public float zoomSpeed = 4f;
    public float maxZoomDistance = 20f;
    public float minZoomDistance = 5f;
    public float pitch = 2f;
    public float yawSpeed = 300f;
    public float currentYaw = 0f; 

    private void Update() {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, minZoomDistance, maxZoomDistance);
        } 

        if(Input.GetMouseButton(2)) {
            currentYaw += Input.GetAxis("Mouse X") * yawSpeed * Time.deltaTime;
        }
    }

    private void LateUpdate() {
        transform.position  = target.position - (offset * currentZoom);
        transform.LookAt(target.position);

        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }
}