using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Velocidades")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float crouchSpeed = 2.5f;

    [Header("Gravedad")]
    public float gravity = -9.81f;

    [Header("Agacharse")]
    public float standingHeight = 2f;
    public float crouchHeight = 1f;

    [Header("Audio")]
    public AudioSource audioSource;

    public AudioClip walkSound;
    public AudioClip runSound;
    public AudioClip crouchSound;

    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MovePlayer();
        ApplyGravity();
        Crouch();
        HandleFootsteps();
    }

    void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float currentSpeed = walkSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            currentSpeed = crouchSpeed;
        }

        Vector3 move =
            transform.right * x +
            transform.forward * z;

        controller.Move(move * currentSpeed * Time.deltaTime);
    }

    void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            controller.height = crouchHeight;
        }
        else
        {
            controller.height = standingHeight;
        }
    }

    void HandleFootsteps()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        bool isMoving =
            Mathf.Abs(x) > 0.1f ||
            Mathf.Abs(z) > 0.1f;

        if (!isMoving)
        {
            audioSource.Stop();
            return;
        }

        AudioClip currentClip = walkSound;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentClip = runSound;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            currentClip = crouchSound;
        }

        if (audioSource.clip != currentClip)
        {
            audioSource.clip = currentClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}