using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 5f;
    public float rotateSpeed;
    public VariableJoystick variableJoystick;
    public Rigidbody rb;
    private Animator animator;

    private bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        // Obține direcția de mișcare de la joystick
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        direction = direction.normalized;

        // Viteza de mișcare (alergare când joystick-ul este deplasat complet într-o direcție)
        float currentSpeed = variableJoystick.Direction.magnitude > 0.7f ? runSpeed : walkSpeed;
        Vector3 targetVelocity = direction * currentSpeed;

        // Aplică forța pe Rigidbody pentru a mișca jucătorul
        rb.velocity = new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z);

        // Rotire către direcția de mișcare
        if (direction.sqrMagnitude > 0)
        {
            var targetDirection = Vector3.RotateTowards(transform.forward, direction, rotateSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(targetDirection);
        }

        // Actualizează animațiile în funcție de mișcare și alergare
        if (currentSpeed == runSpeed)
        {
            Debug.Log("ALEARGA");
            animator.SetBool("Run", true);
            animator.SetBool("Walk", false);
        }
        else if (IsMoving())
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Run", false);
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
        }
    }

    private void HandleJump()
    {
        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }
    }

    private bool IsMoving()
    {
        return variableJoystick.Horizontal != 0 || variableJoystick.Vertical != 0;
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);
    }
}
