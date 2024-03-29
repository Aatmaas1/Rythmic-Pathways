using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.Playables;

public class SC_CHARACTER_MOVE : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of character movement
    public float tileSize = 1f; // Size of each tile in the grid
    public float jumpHeight = 0.5f; // Height of the jump
    public AnimationCurve jumpCurve; // Animation curve for jump interpolation
    public float waitTime;

    bool isColl;
    private Vector3 targetPosition;
    private Vector3 moveDirection;

    private void Start()
    {
        // Snap character to the nearest tile position
        SnapToGrid();
        moveDirection = Vector3.forward;

        // Start moving coroutine
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            // Wait for a short delay
            yield return new WaitForSeconds(waitTime);


            if(isColl)
            {
                // Calculate target position based on current position and input direction
                targetPosition = transform.position + moveDirection * tileSize;

                // Calculate jump target position (landing position)
                Vector3 jumpTargetPosition = targetPosition;
                jumpTargetPosition.y = transform.position.y; // Make sure character jumps only along the y-axis

                // Move character towards jump target position with a smooth jump effect
                float journeyLength = Vector3.Distance(transform.position, jumpTargetPosition);
                float startTime = Time.time;
                float elapsedTime = 0f;

                while (elapsedTime < 1f)
                {
                    elapsedTime += Time.deltaTime * moveSpeed / journeyLength;
                    float jumpProgress = jumpCurve.Evaluate(elapsedTime);
                    float interpolatedJumpHeight = Mathf.Lerp(0f, jumpHeight, jumpProgress);
                    Vector3 interpolatedPosition = Vector3.Lerp(transform.position, jumpTargetPosition, elapsedTime) + Vector3.up * interpolatedJumpHeight;

                    // Rotate character to face the movement direction
                    if (moveDirection != Vector3.zero)
                    {
                        Quaternion targetRotation = Quaternion.LookRotation(moveDirection); // Look along the move direction
                        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, elapsedTime);
                    }

                    // Set character position
                    transform.position = interpolatedPosition;

                    yield return null;
                }

                // Ensure the character lands exactly on the target position
                transform.position = targetPosition;
            }
         
        }
    }

    private void SnapToGrid()
    {
        // Round character position to the nearest tile position
        Vector3 newPosition = new Vector3(
            Mathf.Round(transform.position.x / tileSize) * tileSize,
            transform.position.y,
            Mathf.Round(transform.position.z / tileSize) * tileSize
        );

        transform.position = newPosition;
    }

    private void Update()
    {
        // Handle input for changing movement direction
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Z))
            moveDirection = Vector3.forward;
        else if (Input.GetKeyDown(KeyCode.S))
            moveDirection = Vector3.back;
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Q))
            moveDirection = Vector3.left;
        else if (Input.GetKeyDown(KeyCode.D))
            moveDirection = Vector3.right;
    }


    private void OnCollisionEnter(Collision collision)
    {
        isColl = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        isColl = false;
    }
}

