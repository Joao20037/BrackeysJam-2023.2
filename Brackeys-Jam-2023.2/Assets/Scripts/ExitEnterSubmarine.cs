using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ExitEnterSubmarine : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject submarine;
    [SerializeField] private float delay = 2f;
    [SerializeField] private float radiusRange;


    private InputControls inputs;
    private Rigidbody2D rbSub;
    private bool rangedPlayer;
    private bool isPlayer = false;
    private float elapsedtime = 0f;
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
        inputs = new InputControls();
 
    }

    private void Start()
    {
        player.SetActive(false);
        rbSub = submarine.GetComponent<Rigidbody2D>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        rangedPlayer = isPlayerRanged(radiusRange, player);

        elapsedtime += Time.deltaTime;

        if (elapsedtime >= delay) // Delay para entrar ou sair no submarino 
        {

                if (!isPlayer) // Se esta controlando o player ou o submarino
                {
                    ExitSubmarine(inputs.EnterSubmarino.Enter.ReadValue<float>());
                }
                else
                {
                    if (rangedPlayer) // Se esta no range para entrar no submarino
                    {
                        EnterSubmarine(inputs.EnterSubmarino.Enter.ReadValue<float>());
                    }
                }

        }
    }

    private void EnterSubmarine(float action)
    {
        if (action == 1)
        {
            player.SetActive(false);
            vcam.Follow = submarine.transform;
            rbSub.constraints = RigidbodyConstraints2D.FreezeRotation;
            isPlayer = false;
            elapsedtime = 0f;
            submarine.GetComponent<MovementSubmarine>().enabled = true;

        }
    }

    private void ExitSubmarine(float action)
    {
        if (action == 1)
        {
            player.transform.position = submarine.transform.position + new Vector3(-2, 0, 0); 
            // Precisa colocar spawn aleatorio
            
            // if collider (hit something) try new position

            player.SetActive(true);
            vcam.Follow = player.transform;
            rbSub.constraints = RigidbodyConstraints2D.FreezeAll;
            isPlayer = true;
            elapsedtime = 0f;
            submarine.GetComponent<MovementSubmarine>().enabled = false;
        }
    }

    private bool isPlayerRanged(RaycastHit2D hit)
    {
        if (hit.collider.gameObject == player)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool isPlayerRanged(float radius, GameObject player)
    {
        RaycastHit2D hit = Physics2D.CircleCast(submarine.transform.position, radius, Vector2.zero);
        if (hit.collider)
        {
            if (hit.collider.gameObject == player)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(submarine.transform.position, radiusRange);
    }
}
