using System;
using System.Collections;
using UnityEngine;

public class PlayerControli : MonoBehaviour
{
    private Rigidbody rB;
    private SpriteRenderer sR;
    private Animator aM;

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

    [SerializeField] private bool grounded = false;
    [SerializeField] private bool airTime = false;
    private bool didJump = false;
    private int jumpsLeft = 2;
    private float maxPosX = 0;

    private void Start()
    {
        rB = GetComponent<Rigidbody>();
        sR = GetComponent<SpriteRenderer>();
        aM = GetComponent<Animator>();
        speed = normalSpeed;
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
    }

    private void UpdateAnims()
    {
        aM.SetFloat("WalkSpeed", Mathf.Abs(rB.linearVelocity.x) / 10);
        aM.SetBool("airTime", !grounded);
        aM.SetBool("falling", rB.linearVelocity.y < 0);
        aM.SetBool("didJump", didJump);
    }

    private void MaxSpeedClampCheck()
    {
        if (rB.linearVelocity.x > maxSpeed || rB.linearVelocity.x < -maxSpeed)
        {
            rB.linearVelocity = new Vector3(Mathf.Clamp(rB.linearVelocity.x, -maxSpeed, maxSpeed), rB.linearVelocity.y, rB.linearVelocity.z);
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
        airTime = false;
        if (Physics.Raycast(groundCheckOrigin.transform.position, Vector2.down, airTimeCheckLength, groundLayer))
        {
            airTime = true;
        }
    }

    private void InputCheck()
    {

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (maxPosX - transform.position.x > maxBackwardsMovment) return;
            if (airTime)
            {
                rB.AddForce(Vector2.left * speed * (Time.deltaTime * 100));
                sR.flipX = true;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (airTime)
            {
                rB.AddForce(Vector2.right * speed * (Time.deltaTime * 100));
                sR.flipX = false;
            }
        }
        if (transform.position.x > maxPosX) maxPosX = transform.position.x;
    }

    public void JumpCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (jumpsLeft-1 > 0 || grounded))
        {
            Jump();
            didJump = true;
            StartCoroutine(DidJump());
            jumpsLeft--;
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