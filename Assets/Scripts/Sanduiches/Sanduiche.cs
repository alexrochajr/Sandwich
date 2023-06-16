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

    public string[] IngredientesEscolhidos () //Irá retornar um array somente com os ingredientes escolhidos
    {
        List<string> ingredientes = new List<string>();

        if (tomate) ingredientes.Add(nameof(tomate));
        if (alface) ingredientes.Add(nameof(alface));
        if (carne) ingredientes.Add(nameof(carne));
        if (queijo) ingredientes.Add(nameof(queijo));
        if (cogumelo) ingredientes.Add(nameof(cogumelo));

        return ingredientes.ToArray();
    }

}
