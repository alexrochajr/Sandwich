using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NovoSanduiche", menuName = "Sanduiche/Novo Sanduiche")]
public class Sanduiche : ScriptableObject
{
    public string Nome;
    public Sprite icone;

    //Escolher quais ingredientes serão usados no sanduiche
    public bool tomate, alface, carne, queijo, cogumelo;
}
