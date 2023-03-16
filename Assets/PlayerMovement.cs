using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;

    public float jumpForce = 5f;
    public float jumpCooldown = 1f;
    public float jumpCooldownTimer = 0f;

    public float rotationSpeed = 5f;

    public bool canJump = true;

    public Transform characterTransform;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        if (movementDirection != Vector3.zero)
        {
            transform.forward = movementDirection;
        }

        if (Input.GetButtonDown("Jump") && canJump)
        {
            Jump();
        }

        if (!canJump)
        {
            jumpCooldownTimer -= Time.deltaTime;
            if (jumpCooldownTimer <= 0f)
            {
                canJump = true;
            }
        }
    }

    private void Jump()
    {
        rb.velocity = Vector3.up * jumpForce;
        canJump = false;
        jumpCooldownTimer = jumpCooldown;
    }
}