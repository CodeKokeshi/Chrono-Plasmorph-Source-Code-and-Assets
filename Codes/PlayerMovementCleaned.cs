using Cinemachine;
using System.Collections;
using UnityEngine;

public class PlayerMovementCleaned : MonoBehaviour
{
    // Movement variables
    private float horizontalInput;
    private float JumpPower;
    private float MoveSpeed;

    // Components
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer playerSprite;
    public Transform LeftFoot;
    public Transform RightFoot;
    public Animator PlayerAnimation;
    public CinemachineVirtualCamera PlayerCam;
    public GameObject Trail;
    [SerializeField] private AudioSource Footstep;

    // Ground checking variables
    private readonly float Radius = 0.075f;
    public LayerMask Platform;
    private readonly float DistanceFromGround = 0.01f;

    // Power-up variables
    public float BulkedJump = 15f;
    public float BulkedSpeed = 7f;
    public float DefaultPower = 12f;
    public float DefaultSpeed = 5;
    public float SpeedDuration = 5f;
    public float AdditionalSpeed = 3f;

    // Instruction handling variables
    private bool ShowInstructionDone = false;
    private bool PressOnce = false;

    private void Start()
    {
        ShowInstructionDone = false;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");


        // Check if the player is bulked
        bool Bulked = GetComponent<BulkedOrNot>().Bulked;

        // Set JumpPower and MoveSpeed based on bulked status
        if (Bulked)
        {
            JumpPower = BulkedJump;
            MoveSpeed = GetComponent<BulkedOrNot>().OrangeEaten ? BulkedSpeed : BulkedSpeed + AdditionalSpeed;
        }
        else
        {
            JumpPower = DefaultPower;
            MoveSpeed = GetComponent<BulkedOrNot>().OrangeEaten ? DefaultSpeed + AdditionalSpeed : DefaultSpeed;
        }

        // Handle speed boost
        if (GetComponent<BulkedOrNot>().OrangeEaten && !ShowInstructionDone && Input.GetButtonDown("Fire3") && !PressOnce)
        {
            Trail.GetComponent<TrailRenderer>().emitting = true;
            PressOnce = true;
            StartCoroutine(SpeedBoost());
        }

        // Ground check
        bool isGrounded1 = Physics2D.CircleCast(LeftFoot.position, Radius, Vector2.down, DistanceFromGround, Platform);
        bool isGrounded2 = Physics2D.CircleCast(RightFoot.position, Radius, Vector2.down, DistanceFromGround, Platform);

        // Adjust player constraints based on ground contact
        if (!isGrounded1 && isGrounded2 || isGrounded1 && !isGrounded2)
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        // Play footstep sound
        if (horizontalInput != 0 && (isGrounded1 && isGrounded2))
        {
            if (!Footstep.isPlaying)
            {
                Footstep.Play();
            }
        }
        else
        {
            Footstep.Stop();
        }

        // Handle jumping
        if (Input.GetButtonDown("Jump") && (isGrounded1 || isGrounded2))
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpPower);
        }

        // Handle animation states
        if (!isGrounded1 && !isGrounded2)
        {
            PlayerAnimation.SetBool("Jumping", true);
            PlayerAnimation.SetBool("Running", false);
        }
        else
        {
            PlayerAnimation.SetBool("Jumping", false);
        }

        // Handle player movement
        rb.velocity = new Vector2(horizontalInput * MoveSpeed, rb.velocity.y);

        // Flip player sprite and play running animation
        if (horizontalInput < 0)
        {
            HandleSpriteFlip(true, isGrounded1 || isGrounded2);
        }
        else if (horizontalInput > 0)
        {
            HandleSpriteFlip(false, isGrounded1 || isGrounded2);
        }
        else
        {
            PlayerAnimation.SetBool("Running", false);
        }
    }

    private void HandleSpriteFlip(bool flipX, bool isGrounded)
    {
        if (isGrounded)
        {
            PlayerAnimation.SetBool("Running", true);
        }

        playerSprite.flipX = flipX;
    }

    private IEnumerator SpeedBoost()
    {
        float Duration = 2f;
        float TimeElapsed = 0;

        Color defaultColor = playerSprite.color;
        Color newColor = new Color(1f, 0.57f, 0.98f, 1f); // Purple color

        while (TimeElapsed < Duration)
        {
            playerSprite.color = Color.Lerp(defaultColor, newColor, TimeElapsed / Duration);
            TimeElapsed += Time.deltaTime;
            yield return null;
        }

        FindObjectOfType<ShowSelf>().HideInstruction();
        ShowInstructionDone = true;
        yield return new WaitForSeconds(SpeedDuration);

        GetComponent<BulkedOrNot>().OrangeEaten = false;
        float Duration2 = 2f;
        float TimeElapsed2 = 0;

        while (TimeElapsed2 < Duration2)
        {
            playerSprite.color = Color.Lerp(newColor, defaultColor, TimeElapsed2 / Duration2);
            TimeElapsed2 += Time.deltaTime;
            yield return null;
        }

        Trail.GetComponent<TrailRenderer>().emitting = false;
    }
}
