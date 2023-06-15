using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PratoBehaviour : MonoBehaviour
{
    public GameController gc; //Guarda o GameController para acessar suas funcões
    public Transform[] ingredientePos; //Guarda quais são as posições possiveis dos ingredientes
    public GameObject paoTopo; //A parte de cima do pão

    //Navega pelas posições que pode colocar os ingredientes, caso esteja colocando na ultima posição o sanduiche é tampado e entregue
    public void ReceberIngrediente(GameObject ingrediente)
    {
        for (int i = 0; i < ingredientePos.Length; i++)
        {
            if (ingredientePos[i].childCount == 0)
            {
                ingrediente.transform.parent = ingredientePos[i];
                ingrediente.transform.localPosition = Vector3.zero;

                if (i == ingredientePos.Length - 1)
                {
                    paoTopo.SetActive(true);
                    gc.EntregarPrato();
                }

                break;
            }
        }
    }

    //Limpa o prato para fazer um novo sanduiche
    public void ReiniciarPrato()
    {
        for (int i = 0; i < ingredientePos.Length; i++)
        {
            Destroy(ingredientePos[i].GetChild(0).gameObject);
        }
        paoTopo.SetActive(false);
    }
}