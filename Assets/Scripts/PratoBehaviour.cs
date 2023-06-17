using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PratoBehaviour : MonoBehaviour
{
    public GameController gc; //Guarda o GameController para acessar suas funcões
    public Transform[] ingredientePos; //Guarda quais são as posições possiveis dos ingredientes
    public GameObject paoTopo; //A parte de cima do pão
    public GameObject[] ingredientesColocados = new GameObject[3]; //Guarda quantos ingredientes foram colocados, sendo possivel no maximo 3
    [SerializeField]
    private Animator animator; //para chamar as animações

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    public void ReceberIngrediente(GameObject ingrediente) //Navega pelas posições possiveis dos ingredientes, caso esteja colocando na ultima posição o sanduiche é tampado e entregue
    {
        for (int i = 0; i < ingredientePos.Length; i++)
        {
            if (ingredientePos[i].childCount == 0)
            {
                ingredientesColocados[i] = ingrediente;
                ingrediente.transform.parent = ingredientePos[i];
                ingrediente.transform.rotation = Quaternion.Euler(Vector3.zero);
                ingrediente.transform.localPosition = Vector3.zero;

                if (i == ingredientePos.Length - 1)
                {
                    paoTopo.SetActive(true);
                    EntregarPrato();
                }

                break;
            }
        }
    }

    
    public void ReiniciarPrato() //Limpa o prato para fazer um novo sanduiche, chamado no fim da animação de entrega por meio de um evento
    {
        gc.ConferePrato();
        for (int i = 0; i < ingredientePos.Length; i++)
        {
            ingredientesColocados[i] = null;
            Destroy(ingredientePos[i].GetChild(0).gameObject);
        }
        paoTopo.SetActive(false);
        animator.Play("Inicial");
    }

    public void EntregarPrato() //Toca a animação de entrega
    {
        animator.Play("PratoAnim");
    }
}