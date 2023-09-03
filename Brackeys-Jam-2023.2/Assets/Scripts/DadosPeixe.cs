using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DadosPeixe", order = 1)]
public class DadosPeixe : ScriptableObject
{
    public string nomePeixe;
    public int fotografados;
    public int meta;
}
