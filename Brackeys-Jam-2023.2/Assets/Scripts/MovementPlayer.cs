using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpStrength;
    [SerializeField] public float deepLimit;
    private Rigidbody2D rb;
    private InputControls inputs;
    private float x_scale;
    [SerializeField] Animator animator;

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputs = new InputControls();
        x_scale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        Move(inputs.PlayerControlMovement.Move.ReadValue<Vector2>());
        Jump(inputs.PlayerControlMovement.Jump.ReadValue<float>());
        animator.SetFloat("VelocityY", rb.velocity.y);
    }

    private void Move(Vector2 movementDirection)
    {
        rb.velocity = new Vector3(movementDirection.x,movementDirection.y, 0) * speed;

        if (rb.velocity.x > 0)
        {
            animator.SetBool("isMoving", true);
            if (transform.localScale.x < 0) x_scale = -transform.localScale.x;
            transform.localScale = new Vector3(x_scale, transform.localScale.y, transform.localScale.z);
        }
        else if(rb.velocity.x < 0)
        {
            animator.SetBool("isMoving", true);
            if (transform.localScale.x > 0) x_scale = -transform.localScale.x;
            transform.localScale = new Vector3(x_scale, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (transform.position.y < -deepLimit)
        {
            transform.position = new Vector3(transform.position.x, -(deepLimit), transform.position.z);
        }
    }
    public void UpdateDeepLimit(float newDeepLimit)
    {
        this.deepLimit += newDeepLimit;
    }
    private void Jump(float action)
    {
        if (action == 1)
        {
            rb.AddForce(new Vector2(0, 1) * jumpStrength);
            animator.SetBool("Space", true);
        }
        else
        {
            animator.SetBool("Space", false);
        }
    }

    public void UpdatePlayerSpeed(float speed)
    {
        this.speed += speed;
    }

}
