using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControli : MonoBehaviour
{
    private Rigidbody rB;
    private SpriteRenderer sR;
    private Animator aM;
    private AudioSource aS;
    private Toggle_World tW;

    private float speed = 1f;
    [SerializeField] private float normalSpeed = 2f;
    [SerializeField] private float sprintSpeed = 4f;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private float jumpForceMultiplyer = 1.5f;
    [SerializeField] private float maxBackwardsMovment = 10;

    [SerializeField] private float maxSpeed = 20;
    [SerializeField] private float maxRotAngle = 20;

    [SerializeField] private GameObject groundCheckOrigin;
    [SerializeField] private float groundCheckLength;
    [SerializeField] private float airTimeCheckLength;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private AudioClip walkDarkSound;
    [SerializeField] private AudioClip walkNormSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip miaoSound;

    private AudioSource jumpSource;
    private AudioSource miaoSource;

    private bool grounded = false;
    private bool didJump = false;
    private float airTime = 0;
    private int jumpsLeft = 2;
    private float maxPosX = 0;

    private void Start()
    {
        rB = GetComponent<Rigidbody>();
        sR = GetComponent<SpriteRenderer>();
        aM = GetComponent<Animator>();
        aS = GetComponent<AudioSource>();
        tW = GetComponent<Toggle_World>();
        jumpSource = transform.AddComponent<AudioSource>();
        jumpSource.loop = false;
        jumpSource.playOnAwake = false;
        miaoSource = transform.AddComponent<AudioSource>();
        miaoSource.clip = miaoSound;
        miaoSource.loop = false;
        miaoSource.playOnAwake = false;
        miaoSource.Stop();
        speed = normalSpeed;

        GameManager.Instance.movePoints = 0;
        GameManager.Instance.bonusPoints = 0;
    }

    private void Update()
    {
        SprintCheck();
        InputCheck();
        MaxSpeedClampCheck();
        MaxRotAngleCheck();
        GroundCheck();
        JumpCheck();
        UpdateAnims();
        int rnd = UnityEngine.Random.Range(0, 3500);
        if (rnd == 0 && !miaoSource.isPlaying)
        {
            miaoSource.Play();
        }
    }

    private void UpdateAnims()
    {
        aM.SetFloat("WalkSpeed", Mathf.Abs(rB.linearVelocity.x) / 10);
        aM.SetFloat("airTime", airTime);
        aM.SetBool("falling", rB.linearVelocity.y < 0);
        aM.SetBool("didJump", didJump);
    }

    private void MaxSpeedClampCheck()
    {
        if (rB.linearVelocity.x > maxSpeed || rB.linearVelocity.x < -maxSpeed)
        {
            rB.linearVelocity = new Vector3(Mathf.Clamp(rB.linearVelocity.x, -maxSpeed, maxSpeed), rB.linearVelocity.y, rB.linearVelocity.z);
        }
        if (rB.linearVelocity.x > 0.2 || rB.linearVelocity.x < -0.2)
        {
            if (aS.isPlaying) return;
            if(!tW.isWorldActive)
            {
                aS.clip = walkDarkSound;
            }
            else
            {
                aS.clip = walkNormSound;
            }
            aS.Play();
        }
        else
        {
            aS.Stop();
        }
    }
    private void MaxRotAngleCheck()
    {
        transform.localEulerAngles = new Vector3(0,0, ClampAngle(transform.localEulerAngles.z, -maxRotAngle, maxRotAngle));
    }
    float ClampAngle(float angle, float from, float to)
    {
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }

    private void GroundCheck()
    {
        grounded = false;
        if (Physics.Raycast(groundCheckOrigin.transform.position, Vector2.down, groundCheckLength, groundLayer))
        {
            grounded = true;
            jumpsLeft = maxJumps;
        }

        airTime += Time.deltaTime;
        if (Physics.Raycast(groundCheckOrigin.transform.position, Vector2.down, airTimeCheckLength, groundLayer))
        {
            airTime = 0;
        }
    }

    private void InputCheck()
    {

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (maxPosX - transform.position.x > maxBackwardsMovment) return;
            if (0.0f <= airTime)
            {
                rB.AddForce(Vector2.left * speed * (Time.deltaTime * 100));
                sR.flipX = true;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (0.0f <= airTime)
            {
                rB.AddForce(Vector2.right * speed * (Time.deltaTime * 100));
                sR.flipX = false;
            }
        }
        if (transform.position.x > maxPosX)
        {
            maxPosX = transform.position.x;
            GameManager.Instance.movePoints = (int)transform.position.x;
        }
    }

    public void JumpCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (jumpsLeft-1 > 0 || grounded))
        {
            Jump();
            didJump = true;
            StartCoroutine(DidJump());
            jumpsLeft--;
            jumpSource.clip = jumpSound;
            jumpSource.Play();
        }
    }

    public void Jump()
    {
        Vector2 force = Vector2.up * jumpForceMultiplyer;
        rB.AddForce(force, ForceMode.Impulse);
    }

    IEnumerator DidJump()
    {
        yield return new WaitForSeconds(2);
        didJump = false;
        StopCoroutine(DidJump());
    }

    public void SprintCheck()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = normalSpeed;
        }
    }
}