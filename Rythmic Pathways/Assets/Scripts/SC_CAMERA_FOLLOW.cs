using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CAMERA_FOLLOW : MonoBehaviour
{
    public Transform target; // Reference to the player's transform
    public float smoothSpeed = 0.125f; // Smoothness of camera movement

    private Vector3 offset; // Offset between the camera and the player

    void Start()
    {
        // Calculate the initial offset between the camera and the player
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        // Calculate the target position for the camera (excluding the Y-axis)
        Vector3 targetPosition = target.position + offset;
        targetPosition.y = transform.position.y; // Maintain constant Y-axis position

        // Smoothly move the camera towards the target position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
