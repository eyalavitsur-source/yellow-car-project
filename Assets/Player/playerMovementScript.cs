using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class playerMovementScript : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 9f;
    public float gravity = -9.8f;
    public float JumpHight = 3f;
    public static bool isCar = false;
    public Rigidbody rb = null;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update`
    void Start()
    {
        //rb = gameObject.AddComponent<Rigidbody>();
        //rb.mass = 500;
        //rb.useGravity = false;
    }

    [SerializeField] float moveForce = 2000f;
    [SerializeField] float maxSpeed = 20f;
    [SerializeField] float turnSpeed = 100f;

    void FixedUpdate()
    {
        if (!isCar) return;

        // Forward and backward
        if (Input.GetKey(KeyCode.W))
        {
            if (rb.velocity.magnitude < maxSpeed)
            {
                rb.AddForce(transform.forward * moveForce * Time.fixedDeltaTime, ForceMode.Acceleration);
            }
            while (Input.GetKey(KeyCode.LeftShift))
            {
                maxSpeed = maxSpeed * 2;
                rb.AddForce(transform.forward * 4000 * Time.fixedDeltaTime, ForceMode.Acceleration);
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * moveForce * 0.5f * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
        else
        {
            // Apply drag when not accelerating
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 0.05f);
        }

        // Clamp max speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        float movementDirection = Vector3.Dot(rb.velocity, transform.forward);
        float steerAmount = turnSpeed * Time.fixedDeltaTime * Mathf.Clamp01(rb.velocity.magnitude / maxSpeed);

        if (movementDirection > 0.1f)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.up, -steerAmount);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.up, steerAmount);
            }
        }
        else if (movementDirection < -0.1f)
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.up, -steerAmount);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.up, steerAmount);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isCar) return;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            velocity.y = Mathf.Sqrt(JumpHight * -2 * gravity);
        }
        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}