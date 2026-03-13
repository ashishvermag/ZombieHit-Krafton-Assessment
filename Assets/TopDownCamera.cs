using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Transform target;

    [Header("Camera Settings")]
    public Vector3 offset = new Vector3(0f, 15f, -15f); // 15 units up, 15 units behind
    public float smoothSpeed = 10f;

    void LateUpdate()
    {
        if (target == null) return;

        // 1. Calculate the position behind and above the car as it turns
        Vector3 desiredPosition = target.position + (target.forward * offset.z) + (target.up * offset.y);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // 2. Rotate the camera to always look exactly at the car
        Vector3 lookDirection = target.position - transform.position;
        if (lookDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);
        }
    }
}