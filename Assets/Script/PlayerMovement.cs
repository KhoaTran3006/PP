using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField]
    private float groundDrag;
    [SerializeField]
    private float walkSpeed;

    [Header("Crounching")]
    [SerializeField]
    private float crounchSpeed;
    [SerializeField]
    private float crounchYScale;
    private float startYScale;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        startYScale = transform.localScale.y;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        MovePlayer();

    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        Myinput();
        SpeedControl();
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }
    }

    private void Myinput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(transform.localScale.x, crounchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            moveSpeed = crounchSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl)) 
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }

    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
