using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GeradorClientes gerador; //Guarda qual objeto é o gerador de clientes
    public UIController ui; //Variavel usada para chamar metodos de UI
    public PratoBehaviour pb; //Usado para chamar metodos do prato
    private Sanduiche pedido; //variavel usada para armazenar qual é o pedido atual
    public TMPro.TextMeshProUGUI pedidoTXT;
    public Sanduiche[] sanduiches; //Lista de sanduiches possiveis

    void Start()
    {
        StartCoroutine(ui.Contar());
        NovoSanduiche();
    }

    //Executa todas as ações necessarias para verificar se foi feito o sanduiche certo e permitir um novo pedido
    public void EntregarPrato()
    {
        Debug.Log("entrega");
        pb.ReiniciarPrato();
    }

    //Cria um pedido novo
    private void NovoSanduiche()
    {
        pedido = sanduiches[UnityEngine.Random.Range(0, sanduiches.Length)];
        pedidoTXT.text = pedido.Nome;
    }
}
