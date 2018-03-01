using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 inputDirection = Vector3.zero;
    CharacterController controller;

    // Minimap camera
    public Transform cameraTF;
    public Transform cubeTF;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        inputDirection.x = Input.GetAxis("Horizontal");
        inputDirection.z = Input.GetAxis("Vertical");

        if (inputDirection.magnitude > 0.1f)
            cubeTF.rotation = Quaternion.LookRotation(inputDirection);

        inputDirection = transform.TransformDirection(inputDirection);
        inputDirection *= speed;

        moveDirection.x = inputDirection.x;
        moveDirection.z = inputDirection.z;

        if (controller.isGrounded && Input.GetButton("Jump"))
        {
            moveDirection.y = jumpSpeed;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        // Minimap camera
        cameraTF.position = transform.position + Vector3.up * 15f;
    }
}
