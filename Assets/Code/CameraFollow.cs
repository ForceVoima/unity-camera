using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public float cameraMoveSpeed = 10f;
    public float cameraDistance = 10f;
    public float cameraHeight = 2f;

    public float height;
    public float abovePlayer;

    public float radius = 1f;

    public LayerMask hitmask;

    Vector3 targetPosition;
    Quaternion targetDirection;

    public bool floorHit;
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 behindPlayer = -player.forward + Vector3.up * 0.8f;
        behindPlayer.Normalize();

        RaycastHit hit;
        bool check = Physics.SphereCast(player.position, radius, behindPlayer, out hit, cameraDistance, hitmask);
        float distance = 10f;

        if (!check)
        {
            // Raycast didn't hit anything:
            distance = cameraDistance;
        }
        else
        {
            distance = hit.distance - 0.1f;
        }

        targetPosition = player.position + behindPlayer * distance;

        Debug.DrawLine(player.transform.position, targetPosition, Color.red);

        abovePlayer = Mathf.Abs(player.position.y - targetPosition.y);

        floorHit = Physics.SphereCast(targetPosition, radius, Vector3.down, out hit, abovePlayer, hitmask);

        if (floorHit)
        {
            height = hit.distance;

            // Place camera one meter above ground
            targetPosition -= Vector3.up * (hit.distance - cameraHeight);
        }
        else
        {
            height = abovePlayer;

            //targetPosition -= Vector3.down * (height - cameraHeight);
            targetPosition -= Vector3.up * (abovePlayer - cameraHeight);
        }

        Debug.DrawLine(targetPosition, (targetPosition + (height - cameraHeight) * Vector3.up), Color.red);

        targetDirection = Quaternion.LookRotation(player.position - targetPosition);

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * cameraMoveSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetDirection, Time.deltaTime * cameraMoveSpeed);
	}
}
