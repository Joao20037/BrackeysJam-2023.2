using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catalogo : MonoBehaviour
{
    public List<DadosPeixe> peixes;
    public int upgradePoints;
    private GameObject canva;
    private void Awake()
    {
        canva = gameObject.transform.GetChild(0).gameObject;
    }

    public void Efeito(int pontos)
    {
        if(pontos == 3 || pontos == 6 || pontos == 9)
        {
            GameObject.Find("Submarine").GetComponent<MovementSubmarine>().UpdateDeepLimit(100 * (pontos/3));
            GameObject.Find("Player").GetComponent<MovementPlayer>().UpdateDeepLimit(100 * (pontos / 3));
        }
        else if (pontos == 2 || pontos == 4 || pontos == 8 )
        {
            GameObject.Find("player").GetComponent<MovementPlayer>().UpdatePlayerSpeed(2*(pontos/2));
        }
    }

    private void Update()
    {


    }
    
    
}
