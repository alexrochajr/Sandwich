using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientesInterface : MonoBehaviour
{
    public GameObject geladeiraInterface, plantacaoInterface;
    public PlayerController pc;
    
    public void SelecionarIngrediente(GameObject ingrediente, String nomeIngrediente) //Recebe as informações do botão, volta o jogador ao normal e da o ingrediente para o player
    {
        pc.mira.SetActive(true);
        pc.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        geladeiraInterface.SetActive(false);
        plantacaoInterface.SetActive(false);
        GameObject itemGerando = Instantiate(ingrediente);
        itemGerando.name = nomeIngrediente;
        pc.Pegar(itemGerando);
    }
    public void FecharInterface() //Caso o jogador tenha aberto a interface errada pode fechar usando um botão que use essa função
    {
        pc.mira.SetActive(true);
        pc.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        geladeiraInterface.SetActive(false);
        plantacaoInterface.SetActive(false);
    }
    public void AbrirDeposito(GameObject deposito) //Abre a interface de escolhas de ingrediente e deixa o jogador parado
    {
        pc.mira.SetActive(false);
        pc.enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        if(deposito.name == "Geladeira")
        {
            geladeiraInterface.SetActive(true);
        }
        else if(deposito.name == "Plantação")
        {
            plantacaoInterface.SetActive(true);
        }
    }
}
