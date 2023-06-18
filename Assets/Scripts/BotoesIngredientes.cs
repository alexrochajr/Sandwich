using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotoesIngredientes : MonoBehaviour
{
    private PlayerController pc;
    private IngredientesInterface ii;
    public GameObject ingrediente; //prefab do ingrediente que será criado
    public String nomeIngrediente;
    void Start()
    {
        pc = GameObject.FindWithTag("Jogador").GetComponent<PlayerController>();
        ii = GetComponentInParent<IngredientesInterface>();
    }
    public void GerarIngrediente() //Chama a função de criar o ingrediente na mão do jogador enviando as informações do botão
    {
        ii.SelecionarIngrediente(ingrediente, nomeIngrediente);
    }
}
