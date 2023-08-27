using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catalogo : MonoBehaviour
{   
    public Dictionary<string, int> registros = new Dictionary<string, int>();
    public Dictionary<string, int> Checkpoints = new Dictionary<string, int>();
    public int upgradePoints;
    
    private void Start() 
    {
        //Tipo de peixe e quantos fotografou
        registros.Add("Tipo de peixe", 0);
        //Tipo de peixe e quantos necessários para ganhar o ponto de Upgrade
        Checkpoints.Add("Tipo de peixe", 0);
    }
    
    public void Efeito(int pontos)
    {
        
    }
    
    
}
