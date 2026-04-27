using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 8f;
    public float gravity = -20f;

    private CharacterController controller;
    private Animator animator;
    private Vector3 velocity;
    private Vector2 moveInput;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        ApplyGravity();
        UpdateAnimation();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded)
        {
            velocity.y = jumpForce;
        }
    }

    void Move()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

        if (move.magnitude > 0.1f)
        {
            // Xoay theo hướng di chuyển
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                10f * Time.deltaTime
            );
        }

        controller.Move(move * speed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void UpdateAnimation()
    {
        if (animator == null) return;

        float speedValue = moveInput.magnitude > 0.1f ? 1f : 0f;

        animator.SetFloat("Speed", speedValue);
        animator.SetBool("IsGrounded", controller.isGrounded);
    }
}