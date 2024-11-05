using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float walkSpeed = 6f;
    [SerializeField] float runSpeed = 12f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float lookSpeed = 2f;
    [SerializeField] float maxLookAngle = 70f;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] Transform playerCamera;

    private float currentRotationX = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    void Update()
    {
        HandleMovementInput();
        HandleMouseLook();
        HandleJumpInput();
    }

    void HandleMovementInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        Vector3 movement = (forward.normalized * verticalInput + right.normalized * horizontalInput).normalized * currentSpeed;
        movement.y = rb.velocity.y;

        rb.velocity = movement;
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up * mouseX * lookSpeed);

        currentRotationX -= mouseY * lookSpeed;
        currentRotationX = Mathf.Clamp(currentRotationX, -maxLookAngle, maxLookAngle);

        playerCamera.transform.localRotation = Quaternion.Euler(currentRotationX, 0f, 0f);
    }

    void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.1f, ground);
    }
}
