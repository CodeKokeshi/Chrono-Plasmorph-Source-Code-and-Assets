using Cinemachine;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement Variables
    float horizontalInput;
    float JumpPower;
    float MoveSpeed;

    // Power Up Variables
    [Header("Power Up Variables")]
    public float BulkedJump = 15f;
    public float BulkedSpeed = 7f;
    public float DefaultPower = 12f;
    public float DefaultSpeed = 5;
    public float SpeedDuration = 5f;
    public float AdditionalSpeed = 3f;

    // Ground Checking Variables
    [Header("Ground Checking Variables")]
    private readonly float Radius = 0.075f;
    public LayerMask Platform;
    private readonly float DistanceFromGround = 0.05f;

    // Components
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer playerSprite;
    public Transform LeftFoot;
    public Transform RightFoot;
    public Animator PlayerAnimation;
    public CinemachineVirtualCamera PlayerCam;

    // Booleans
    bool ShowInstructionDone = false; // For checking if boost instruction was to be showed or not
    bool PressOnce = false; // For checking if the boost was activated already then disable it

    [Header("VFX")]
    public GameObject Trail;
    public GameObject BoostInstructionText;
    public GameObject BoostVFX;

    [Header("Audio")]
    [SerializeField] AudioSource Footstep;
    [SerializeField] AudioSource Jump;
    [SerializeField] AudioSource BoostActivated;



    void Update()
    {
        // Bulk-Up Logic
        bool Bulked = GetComponent<BulkedOrNot>().Bulked;
        if (Bulked == true)
        {
            BoostBulkedMovement();
        }
        else if (Bulked == false)
        {
            BoostDefaultMovement();
        }

        // Ground Check Logic
        bool isGrounded1 = Physics2D.CircleCast(LeftFoot.position, Radius, Vector2.down, DistanceFromGround, Platform);
        bool isGrounded2 = Physics2D.CircleCast(RightFoot.position, Radius, Vector2.down, DistanceFromGround, Platform);


        // Edge Detection Logic
        if (!isGrounded1 && isGrounded2 || isGrounded1 && !isGrounded2)
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        // Walking Logic
        horizontalInput = Input.GetAxis("Horizontal");
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

        rb.velocity = new Vector2(horizontalInput * MoveSpeed, rb.velocity.y);
        if (horizontalInput < 0)
        {
            if (isGrounded1 || isGrounded2)
            {
                PlayerAnimation.SetBool("Running", true);

            }
            playerSprite.flipX = true;
        }
        else if (horizontalInput > 0)
        {

            if (isGrounded1 || isGrounded2)
            {
                PlayerAnimation.SetBool("Running", true);

            }


            playerSprite.flipX = false;
        }
        else
        {
            PlayerAnimation.SetBool("Running", false);

        }

        // Jump Logic
        if (Input.GetButtonDown("Jump") && (isGrounded1 || isGrounded2))
        {

            rb.velocity = new Vector2(rb.velocity.x, JumpPower);


            if (Jump != null)
            {
                if (!Jump.isPlaying)
                {
                    Jump.Play();
                }
            }


        }



        // Walking And Jump Animation Logic
        if (!isGrounded1 && !isGrounded2)
        {
            PlayerAnimation.SetBool("Jumping", true);
            PlayerAnimation.SetBool("Running", false);
        }

        else
        {
            PlayerAnimation.SetBool("Jumping", false);
        }



    }

    void BoostBulkedMovement() {
        JumpPower = BulkedJump;
        if (GetComponent<BulkedOrNot>().OrangeEaten == true)
        {
            if (ShowInstructionDone == false)
            {
                if (BoostInstructionText != null) { 
                BoostInstructionText.SetActive(true);
                }
            }
            if (Input.GetButtonDown("Fire3") && PressOnce == false)
            {
                if (BoostActivated != null)
                {
                    if (!BoostActivated.isPlaying)
                    {
                        BoostActivated.Play();

                    }
                }
                Trail.GetComponent<TrailRenderer>().emitting = true;
                BoostVFX.SetActive(true);
                PressOnce = true;
                StartCoroutine(SpeedBoost());
                IEnumerator SpeedBoost()
                {
                    float Duration = 2f;
                    float TimeElapsed = 0;

                    Color defaultColor = playerSprite.color;
                    float r = 0xFF / 255.0f;
                    float g = 0x92 / 255.0f;
                    float b = 0xFA / 255.0f;
                    float a = 1.0f;  // Alpha value, set to 1 for full visibility

                    Color newColor = new Color(r, g, b, a);
                    MoveSpeed = BulkedSpeed + AdditionalSpeed;


                    while (TimeElapsed < Duration)
                    {
                        playerSprite.color = Color.Lerp(defaultColor, newColor, TimeElapsed / Duration);
                        TimeElapsed += Time.deltaTime;
                        yield return null;
                    }

                    ShowInstructionDone = true;
                    yield return new WaitForSeconds(SpeedDuration);
                    if (BoostActivated != null)
                    {
                        if (!BoostActivated.isPlaying)
                        {
                            BoostActivated.Play();

                        }
                    }
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
                    BoostVFX.SetActive(false);





                }
            }


        }
        else
        {
            MoveSpeed = BulkedSpeed;

        }
    }

    void BoostDefaultMovement()
    {
        JumpPower = DefaultPower;

        if (GetComponent<BulkedOrNot>().OrangeEaten == true)
        {
            if (ShowInstructionDone == false)
            {
                if (BoostInstructionText != null)
                {

                
                BoostInstructionText.SetActive(true);
                }

            }
            if (Input.GetButtonDown("Fire3") && PressOnce == false)
            {
                if (BoostActivated != null)
                {
                    if (!BoostActivated.isPlaying)
                    {
                        BoostActivated.Play();

                    }
                }
                PressOnce = true;
                Trail.GetComponent<TrailRenderer>().emitting = true;
                BoostVFX.SetActive(true);
                StartCoroutine(SpeedBoost());
                IEnumerator SpeedBoost()
                {
                    float Duration = 2f;
                    float TimeElapsed = 0;

                    Color defaultColor = playerSprite.color;
                    float r = 0xFF / 255.0f;
                    float g = 0x92 / 255.0f;
                    float b = 0xFA / 255.0f;
                    float a = 1.0f;  // Alpha value, set to 1 for full visibility

                    Color newColor = new Color(r, g, b, a);
                    MoveSpeed = DefaultSpeed + AdditionalSpeed;
                    while (TimeElapsed < Duration)
                    {
                        playerSprite.color = Color.Lerp(defaultColor, newColor, TimeElapsed / Duration);
                        TimeElapsed += Time.deltaTime;
                        yield return null;
                    }


                    ShowInstructionDone = true;
                    yield return new WaitForSeconds(SpeedDuration);
                    if (BoostActivated != null)
                    {
                        if (!BoostActivated.isPlaying)
                        {
                            BoostActivated.Play();

                        }
                    }
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
                    BoostVFX.SetActive(false);



                }
            }


        }
        else
        {
            MoveSpeed = DefaultSpeed;

        }
    }
}
