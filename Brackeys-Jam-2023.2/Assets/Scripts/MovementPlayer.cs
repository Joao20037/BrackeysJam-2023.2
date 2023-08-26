using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private PlayerControl inputs;

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
        inputs = new PlayerControl();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move(inputs.PlayerControlMovement.Move.ReadValue<Vector2>());
    }

    private void Move(Vector2 movementDirection)
    {
        //Debug.Log("entrou");    
        rb.velocity = new Vector3(movementDirection.x,movementDirection.y, 0) * speed;
        //Debug.Log(movementDirection.x);
        //Debug.Log(movementDirection.y);
    }

}
