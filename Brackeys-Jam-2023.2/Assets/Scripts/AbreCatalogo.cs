using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbreCatalogo : MonoBehaviour
{
    private InputControls inputs;
    private bool ativo;
    [SerializeField] GameObject catalogo;
    [SerializeField] GameObject FOV;
    // Start is called before the first frame update
    private void Awake()
    {
        inputs = new InputControls();
        ativo = false;
    }
    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    // Update is called once per frame
    void Update()
    {
         if(inputs.PlayerControlMovement.AbrirCatalogo.triggered)
        {
            catalogo.SetActive(ativo);
            FOV.SetActive(!ativo);
            ativo = !ativo;
        }
            
    }
}
