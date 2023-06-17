using System;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameController gc;
    public TMPro.TextMeshProUGUI contagemText; //Elemento de texto da contagem inicial
    public TMPro.TextMeshProUGUI timerText; //Elemento textual do timer, para que o jogador saiba quanto tempo falta
    public TMPro.TextMeshProUGUI nomePedidoText, ingredientesText; //Locais quais estarão o nome e ingredientes dos pedidos
    public Image iconePedido; //Local onde aparecera a imagem dos pedidos
    public TMPro.TextMeshProUGUI pontuacaoText; //Texto que mostra a pontuação
    private int pontosL; //Variavel local do script de pontos
    public GameObject pedidoPanel; 
    public GameObject PanelFinal;
    public TMPro.TextMeshProUGUI pontuacaoFinalText;
    
    
    public IEnumerator Contar()//A cada segundo altera o texto do contador de inicio
    {
        contagemText.text = "3";
        yield return new WaitForSecondsRealtime(1.2f);
        contagemText.text = "2";
        yield return new WaitForSecondsRealtime(1);
        contagemText.text = "1";
        yield return new WaitForSecondsRealtime(1);
        gc.ComecarContagem(); //Avisa o GameController que pode começar a contar o tempo que o jogador tem
        contagemText.text = "VAI";
        yield return new WaitForSecondsRealtime(1);
    }

    public void AtualizarPedidoUI(Sanduiche pedido)//Sempre que chamado irá alterar na UI qual é o pedido
    {
        pedidoPanel.SetActive(true);
        nomePedidoText.text = pedido.Nome;
        ingredientesText.text = pedido.IngredientesEscolhidos()[0] + "\n" + pedido.IngredientesEscolhidos()[1] + "\n" + pedido.IngredientesEscolhidos()[2];
        iconePedido.sprite = pedido.icone;
    }
    public void AtualizarTimer(int tempo)//Modifica o texto do timer para o jogador saber quanto tempo falta
    {
        timerText.text = tempo.ToString();
    }
    public void AtualizaPontos(int pontos) //Muda a pontuação que está na tela
    {
        pontuacaoText.text = pontos.ToString() + "  pts";
        pontosL = pontos;
    }
    public void LimparPedido() //Desativa o painel de pedidos para não ficar com informação defasada ou sem informação
    {
        pedidoPanel.SetActive(false);
    }
    public void Finalizar() //Ativa o painel final e informa a pontuação final
    {
        PanelFinal.SetActive(true);
        pontuacaoFinalText.text = "Pontuação: " + pontosL;
    }
}
