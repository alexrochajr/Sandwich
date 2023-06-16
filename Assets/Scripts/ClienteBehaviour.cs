using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClienteBehaviour : MonoBehaviour
{
    public GameController gc;
    void Awake()
    {
        gc = FindObjectOfType<GameController>();
    }

    void FazPedido() //Acontece por meio de um evento ao fim da animação do cliente andando até a janela
    {
        gc.NovoSanduiche();
    }
}
