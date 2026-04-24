using UnityEngine;

public class PlayerControli : MonoBehaviour
{
    private Rigidbody2D rB;
    private SpriteRenderer sR;

    private float speed = 1f;
    [SerializeField] private float normalSpeed = 2f;
    [SerializeField] private float sprintSpeed = 4f;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private float jumpForceMultiplyer = 1.5f;

    [SerializeField] private GameObject groundCheckOrigin;
    [SerializeField] private float groundCheckLength;
    [SerializeField] private LayerMask groundLayer;

    private bool grounded = false;
    private int jumpsLeft = 2;

    private void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        sR = GetComponent<SpriteRenderer>();
        speed = normalSpeed;
    }

    private void Update()
    {
        SprintCheck();
        InputCheck();
        GroundCheck();
        JumpCheck();
    }

    private void GroundCheck()
    {
        grounded = false;
        if (Physics2D.BoxCast(groundCheckOrigin.transform.position, new Vector2(1,0.1f), 0, Vector2.down, groundCheckLength, groundLayer))
        {
            grounded = true;
            jumpsLeft = maxJumps;
        }
    }

    private void InputCheck()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rB.AddForce(Vector2.left * speed * (Time.deltaTime * 100));
            sR.flipX = true;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rB.AddForce(Vector2.right * speed * (Time.deltaTime * 100));
            sR.flipX = false;
        }
    }

    public void JumpCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (jumpsLeft-1 > 0 || grounded))
        {
            Jump();
            jumpsLeft--;
        }
    }

    public void Jump()
    {
        Vector2 force = Vector2.up * jumpForceMultiplyer;
        rB.AddForce(force, ForceMode2D.Impulse);
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