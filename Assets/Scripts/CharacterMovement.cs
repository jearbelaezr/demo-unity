using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float maxSpeed = 6.0f;
    public float moveDirection;
    public bool facingRight = true;
    private new Rigidbody rigidbody;
    private Animator anim;
    public float jumpSpeed = 800.0f;
    public bool grounded = false;
    public Transform groundCheck;
    public float groundRadious = 0.2f;
    public LayerMask whatIsGround;
    public float knifeSpeed = 600.0f;
    public Transform knifeSpawn;
    public Rigidbody knifePrefab;
    Rigidbody clone;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        groundCheck = GameObject.Find("GroundCheck").transform;
        knifeSpawn = GameObject.Find("KnifeSpawn").transform;
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = Input.GetAxis("Horizontal");
        if (grounded && Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("IsJumping");
            rigidbody.AddForce(new Vector2(0, jumpSpeed));
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(moveDirection * maxSpeed, rigidbody.velocity.y);
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadious, whatIsGround);
        if (moveDirection > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveDirection < 0 && facingRight)
        {
            Flip();
        }
        anim.SetFloat("Blend", Mathf.Abs(moveDirection));
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }

    void Attack()
    {
        clone = Instantiate(knifePrefab, knifeSpawn.position, knifeSpawn.rotation);
        clone.AddForce(knifeSpawn.transform.right * knifeSpeed);
    }
}
