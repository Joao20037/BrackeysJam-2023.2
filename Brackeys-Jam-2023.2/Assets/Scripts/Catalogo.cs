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
        registros.Add("Palhaco", 0);
        registros.Add("AguaViva", 0);
        registros.Add("Axolote", 0);
        registros.Add("Coringa", 0);
        registros.Add("Espada", 0);
        registros.Add("Machado", 0);
        registros.Add("Piranha", 0);
        registros.Add("Agua-Viva", 0);
        registros.Add("pexeflor", 0);
        registros.Add("monstrovrum", 0);
        registros.Add("vagalume", 0);
        //Tipo de peixe e quantos necessários para ganhar o ponto de Upgrade
        Checkpoints.Add("Palhaco", 6);
        Checkpoints.Add("AguaViva", 5);
        Checkpoints.Add("Axolote", 4);
        Checkpoints.Add("Coringa", 3);
        Checkpoints.Add("Espada", 4);
        Checkpoints.Add("Machado", 3);
        Checkpoints.Add("Piranha", 2);
        Checkpoints.Add("Agua-Viva", 5);
        Checkpoints.Add("pexeflor", 2);
        Checkpoints.Add("monstrovrum", 1);
        Checkpoints.Add("vagalume", 5);
    }
    
    public void Efeito(int pontos)
    {
        if(pontos == 3 || pontos == 6 || pontos == 9)
        {
            GameObject.Find("Submarine").GetComponent<MovementSubmarine>().UpdateDeepLimit(10*(pontos/3));
        }
        else if (pontos == 2 || pontos == 4 || pontos == 8 )
        {
            GameObject.Find("player").GetComponent<MovementPlayer>().UpdatePlayerSpeed(2*(pontos/2));
        }
    }
    
    
}
