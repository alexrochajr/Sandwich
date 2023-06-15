using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableBehaviour : MonoBehaviour
{
    public GameObject item;
    
    //Instancia a copia do ingrediente selecionado na variavel item e devolve qual foi ele para quem chamou a função
    public void Pegar(out GameObject itemSaida)
    {
        itemSaida = Instantiate(item);
    }
}
