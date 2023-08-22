using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ExitEnterSubmarine : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject submarine;
    private InputControls inputs;
    private bool isPlayer = false;
    private Rigidbody2D rbSub;
    [SerializeField] private float delay = 2f;
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
        elapsedtime += Time.deltaTime;

        if (elapsedtime >= delay)
        {
            if (!isPlayer)
            {
                ExitSubmarine(inputs.EnterSubmarino.Enter.ReadValue<float>());
            }
            else
            {
                EnterSubmarine(inputs.EnterSubmarino.Enter.ReadValue<float>());
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
        }
    }

    private void ExitSubmarine(float action)
    {
        if (action == 1)
        {
            player.SetActive(true);
            player.transform.position = submarine.transform.position + new Vector3(-2, 0, 0);
            vcam.Follow = player.transform;
            rbSub.constraints = RigidbodyConstraints2D.FreezeAll;
            isPlayer = true;
            elapsedtime = 0f;
        }
    }
}
