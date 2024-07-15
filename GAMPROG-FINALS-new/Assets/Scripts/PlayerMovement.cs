using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float health = 100f;

    [SerializeField] private TextMeshProUGUI goldText;
    public float moveSpeed; //too lazy for encapsulation zz
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    private bool readyToJump = true;

    [SerializeField] private float fallMultiplier; 

    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    [SerializeField] private Transform orientation;

    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask Ground;
    bool grounded;

    [SerializeField] private float groundDrag;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;

    [SerializeField] private int Gold = 0;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; 
        goldText.text = Gold.ToString();
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);

        if (grounded)
        rb.drag = groundDrag;
        else
        rb.drag = 0;

        PlayerInput();
        controlSpeed();
    }
    private void FixedUpdate()
    {
        MovePlayer();
        JumpPhysics();
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput =  Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(JumpReset), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(grounded)
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        else if(!grounded)
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

    }

    private void controlSpeed()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void JumpReset()
    {
        readyToJump = true;
    }

     private void JumpPhysics()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    internal void TakeDamage(float amount)
    {
        Debug.Log("Hit");
        health -= amount;
        if (health <= 0f)
        {
            Debug.Log("Dead");
        }
    }

    internal void AddGold()
    {
        Gold = Gold + 15;
        goldText.text = Gold.ToString();
        //Debug.Log(Gold);
    }
}