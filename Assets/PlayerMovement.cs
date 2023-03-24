using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 5f;
    public float jumpCooldown = 1f;
    public float jumpCooldownTimer = 0f;
    public float rotationTime = 0.1f;
    private float rotationVelocity;
    private bool canJump = true;
    private bool isSprinting = false;
    public Transform mainCamera;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on player object.");
        }

        if (mainCamera == null)
        {
            mainCamera = Camera.main.transform;
        }

        rb.isKinematic = false;
        canJump = true; // set canJump to true in the Start method
    }

    // Update is called once per frame
    void Update()
    {
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

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }
    }

    // FixedUpdate is called at a fixed interval and is synchronized with the physics engine
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            if (float.IsNaN(targetAngle))
            {
                targetAngle = 0f;
            }

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, rotationTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            if (isSprinting)
            {
                rb.MovePosition(rb.position + moveDir.normalized * speed * Time.fixedDeltaTime * 2f);
            }
            else
            {
                rb.MovePosition(rb.position + moveDir.normalized * speed * Time.fixedDeltaTime);
            }
        }
    }

    // LateUpdate is called once per frame after all other Update functions have been called
    void LateUpdate()
    {
        // update the position and rotation of the character model or camera
        transform.position = rb.position;
        // assuming the camera is a child of the character, update its rotation relative to the character's rotation
        mainCamera.localRotation = Quaternion.Euler(mainCamera.localEulerAngles.x, transform.localEulerAngles.y, mainCamera.localEulerAngles.z);
    }

    private void Jump()
    {
        rb.velocity = Vector3.up * jumpForce;
        rb.isKinematic = false;
        canJump = false;
        jumpCooldownTimer = jumpCooldown;
    }
}