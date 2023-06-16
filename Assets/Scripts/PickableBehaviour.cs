using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableBehaviour : MonoBehaviour
{
    public GameObject item;
    public string nomeIngrediente;
    
    /*Instancia a copia do ingrediente selecionado na variavel item, cria uma variavel temporaria para fazer possiveis alterações 
    e devolve o gameobject alterado para quem chamou a função*/
    public void Pegar(out GameObject itemSaida)
    {
        GameObject itemGerando = Instantiate(item);
        itemGerando.name = nomeIngrediente;
        itemSaida = itemGerando;
    }
}
