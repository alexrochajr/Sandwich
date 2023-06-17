using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClienteBehaviour : MonoBehaviour
{
    private GameController gc;
    void Awake()
    {
        gc = FindObjectOfType<GameController>();
    }

    void FazPedido() //Acontece por meio de um evento ao fim da animação do cliente andando até a janela
    {
        gc.NovoSanduiche();
    }
    public void Sair() //Toca a animação do cliente saindo, que no fim ativa a função ChamaNovoCliente()
    {
        this.GetComponent<Animator>().Play("Saindo");
    }
    public void ChamarNovoCliente() //Cria um novo cliente e destroi essa instancia
    {
        gc.GerarCliente();
        Destroy(this);
    }
}
