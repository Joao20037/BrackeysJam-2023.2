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
        if(pontos == 3 || pontos == 6 || pontos == 9)
        {
            GameObject.FindGameObjectWithTag("Submarino").GetComponent<MovementSubmarine>().UpdateDeepLimit(10*(pontos/3));
        }
        else if (pontos == 2 || pontos == 4 || pontos == 8 )
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<MovementPlayer>().UpdatePlayerSpeed(2*(pontos/2));
        }
    }
    
    
}
