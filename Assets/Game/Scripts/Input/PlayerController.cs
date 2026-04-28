using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 12f;
    public float gravity = -20f;
    public float rotationSpeed = 10f;

    private CharacterController controller;
    private Animator animator;

    private Vector3 velocity;
    private Vector2 moveInput;
    private Vector3 currentMove;

    public bool canInput = true;
    bool isDead = false;

    [Header("Mobile")]
    public Joystick joystick;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return;

        Vector2 input = GetInput();

        Move(input);
        ApplyGravity();
        UpdateAnimation(input);

        CheckFallDeath();
    }

    Vector2 GetInput()
    {
        Vector2 keyboard = moveInput;
        Vector2 joy = joystick != null ? joystick.Input : Vector2.zero;

        if (joy.magnitude > 0.2f)
            return joy;

        return keyboard;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Jump();
        }
    }

    void Move(Vector2 input)
    {
        if (!canInput) return;

        Vector3 targetMove = new Vector3(input.x, 0, input.y);

        currentMove = Vector3.Lerp(currentMove, targetMove, 15f * Time.deltaTime);

        if (currentMove.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(currentMove);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        controller.Move(currentMove * speed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    public void Jump()
    {
        if (!canInput) return;

        if (controller.isGrounded)
        {
            velocity.y = jumpForce;
            AudioManager.Instance.PlayJump();
        }
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void UpdateAnimation(Vector2 input)
    {
        if (animator == null) return;

        bool grounded = controller.isGrounded;

        float speedValue = (grounded && input.magnitude > 0.1f) ? 1f : 0f;

        animator.SetFloat("Speed", speedValue);
        animator.SetBool("IsGrounded", grounded);
    }

    void CheckFallDeath()
    {
        if (!controller.isGrounded && velocity.y < -15f)
        {
            Die();
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (isDead) return;

        var tile = hit.collider.GetComponent<GroundTile>();

        if (tile != null)
        {
            tile.TryTrigger();
        }
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        canInput = false;

        if (animator != null)
        {
            animator.SetBool("IsDead", true);
        }

        GameManager.Instance.OnPlayerDead();
    }

    public void ResetState(Vector3 spawnPos)
    {
        isDead = false;
        canInput = true;

        transform.position = spawnPos;

        velocity = Vector3.zero;
        moveInput = Vector2.zero;
        currentMove = Vector3.zero;

        if (animator != null)
        {
            animator.SetBool("IsDead", false);
        }
    }

    public void DisableInput() => canInput = false;
    public void EnableInput() => canInput = true;
}