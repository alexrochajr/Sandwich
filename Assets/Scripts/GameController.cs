using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GeradorClientes gerador; //Guarda qual objeto é o gerador de clientes
    public UIController ui; //Variavel usada para chamar metodos de UI
    public PratoBehaviour pb; //Usado para chamar metodos do prato
    [HideInInspector]
    public Sanduiche pedido; //variavel usada para armazenar qual é o pedido atual
    public Sanduiche[] sanduiches; //Lista de sanduiches possiveis
    private int pontuacao; //Guarda a pontuação atual
    private bool timerComecou; //Verdadeiro quando puder começar a contar o tempo que o jogador tem
    private float quandoTimerComecou; //Guarda quando a partida começou para que o timer esteja correto
    private int timer; //Guarda quanto tempo falta para acabar a rodada
    public int tempoLimite = 120; //Maximo de tempo da rodada
    public GameObject jogador;
    private GameObject cliente;

    void Start()
    {
        pontuacao = 0;
        StartCoroutine(ui.Contar());
        Time.timeScale = 1;
    }
    
    void FixedUpdate()
    {
        if(timerComecou) //Começa a contar quando começar a partida e pede para a UI atualizar o texto do timer
        {
            timer = (int)(Time.time - quandoTimerComecou);
            ui.AtualizarTimer(tempoLimite - timer);
        }
        if(timer == tempoLimite){FimDoJogo();}
    }

    public void ComecarContagem() //Atualiza as variaveis necessarias e chama o primeiro cliente
    {
        timerComecou = true;
        quandoTimerComecou = Time.time;
        GerarCliente();
    }
    public void GerarCliente() //Função criada para caso outro objeto necessite criar outro cliente
    {
        gerador.GerarCliente(out cliente);
    }

    public void ConferePrato() //Executa todas as ações necessarias para verificar se foi feito o sanduiche certo e permitir um novo pedido
    {

        int ingredientesCorretos = 0;
        List<string> confereIngredientes = new List<string>(pedido.IngredientesEscolhidos()); //Cria a lista para que se um ingrediente ja foi conferido ele não seja procurado de novo
        foreach (GameObject ingrediente in pb.ingredientesColocados)
        {  
            if(confereIngredientes.IndexOf(ingrediente.name) != -1)
            {
                confereIngredientes.Remove(ingrediente.name);
                ingredientesCorretos++;
            }
        }
        if(ingredientesCorretos == 3)
        {
            AdicionaPontos();
            ingredientesCorretos = 0;
        }
        else
        {
            RemovePontos();
            ingredientesCorretos = 0;
        }
        pedido = null;
        ui.LimparPedido();
        cliente.GetComponent<ClienteBehaviour>().Sair();
    }

    public void NovoSanduiche() //Cria um pedido novo
    {
        pedido = sanduiches[UnityEngine.Random.Range(0, sanduiches.Length)];
        ui.AtualizarPedidoUI(pedido);
    }

    void AdicionaPontos()
    {
        pontuacao += 10;
        ui.AtualizaPontos(pontuacao);
    }

    void RemovePontos()
    {
        pontuacao -= 10;
        if(pontuacao <= 0){pontuacao = 0;} //Não permite pontuação negativa
        ui.AtualizaPontos(pontuacao);
    }

    private void FimDoJogo()
    {
        Time.timeScale = 0;
        jogador.GetComponent<PlayerController>().enabled = false;
        ui.Finalizar();
        Cursor.lockState = CursorLockMode.None;
    }

    public void ReiniciarFase()
    {
        SceneManager.LoadScene("Cozinha");
    }
    public void VoltarParaMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
