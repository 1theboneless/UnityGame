using UnityEngine;

public class Controller : MonoBehaviour
{
    public float moveSpeed = 5f; // movement speed
    public float rotateSpeed = 100f; // rotation speed

    void Update()
    {
        // Get the horizontal and vertical input values
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Move the player based on the input values
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // Rotate the player based on the horizontal input
        transform.Rotate(Vector3.up * horizontalInput * rotateSpeed * Time.deltaTime);
    }
}
