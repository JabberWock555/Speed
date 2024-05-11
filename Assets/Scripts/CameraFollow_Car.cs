using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow_Car : MonoBehaviour
{
   public Transform target;  // The car's transform
    public Vector3 offset;  // Base offset from the car
    public float distanceMultiplier = 0.1f;  // Multiplier for distance based on car speed
    public float maxDistance = 10f;  // Maximum distance behind the car
    public float minSmoothSpeed = 0.1f;  // Minimum smooth speed
    public float maxSmoothSpeed = 0.5f;  // Maximum smooth speed
    private Vector3 velocity = Vector3.zero;  // To store the velocity for SmoothDamp
    public float maxExpectedSpeed = 180f;
    void LateUpdate()
    {
        // Get the car's current speed
        float currentSpeed = target.GetComponent<Rigidbody>().velocity.magnitude;
        
        // Calculate smooth speed dynamically based on current speed
        float adaptiveSmoothSpeed = Mathf.Lerp(minSmoothSpeed, maxSmoothSpeed, currentSpeed / maxExpectedSpeed);

        // Calculate the desired position based on the current speed
        Vector3 desiredPosition = target.position - target.forward * (offset.z + Mathf.Clamp(currentSpeed * distanceMultiplier, 0, maxDistance)) + new Vector3(0, offset.y, 0);

        // Smoothly interpolate between the camera's current position and the desired position using SmoothDamp
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, adaptiveSmoothSpeed);

        // Make sure the camera is always facing the target
        transform.LookAt(target);
    }

}
