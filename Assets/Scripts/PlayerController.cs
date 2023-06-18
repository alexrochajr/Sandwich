using System;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameController gc;
    //-------------------------------------------------------------------------------------------------//
    //----------------------------------------Variaveis camera-----------------------------------------//
    //-------------------------------------------------------------------------------------------------//
    private Camera pCamera; //Camera
    private float mouseX, mouseY; //Guarda o movimento do mouse
    [Range(0.1f, 10)]public float sensibilidadeMouse = 1; //Altera a sensibilidade do movimento da camera
    float xRotation = 0f; //Guarda o movimento do mouse na vertical invertido



    //-------------------------------------------------------------------------------------------------//
    //---------------------------------------Variaveis personagem--------------------------------------//
    //-------------------------------------------------------------------------------------------------//
    private float playerX, playerY; //Guarda os inputs de movimento WASD
    public CharacterController controller; //Referencia o componente de "Character Controller" do player
    public float velocidade = 2f; //Altera a velocidade de movimento



    //-------------------------------------------------------------------------------------------------//
    //---------------------------------------Variaveis Raycast e segurar-------------------------------//
    //-------------------------------------------------------------------------------------------------//

    //Gerar um raio que sai do meio da tela para identificar o que o jogador está olhando
    private RaycastHit hit; //Guarda qual objeto está sendo olhado
    private bool rayBateu; //Fica verdadeira se o jogador olhar para algo
    public float distanciaDePegar = 1f; //Define a distancia maxima para pegar algum objeto
    public Transform areaItens; //Guarda a area que os itens devem ficar
    [SerializeField]
    private GameObject objetoSegurado; //Guarda qual objeto está sendo segurado



    //-------------------------------------------------------------------------------------------------//
    //---------------------------------------Variaveis Interface---------------------------------------//
    //-------------------------------------------------------------------------------------------------//
    public GameObject mira; //Objeto da mira que fica no meio da tela
    public Sprite miraBase; //Imagem quando não pode pegar item que está olhando
    public Sprite miraInteragir; //Imagem quando pode pegar item
    public IngredientesInterface ingredientesInterface; //Usada para chamar a função que ativa as interfaces da plantação ou geladeira
    


    void Start()
    {
        pCamera = Camera.main; //pCamera agora corresponde a "Main Camera" na cena
        Cursor.lockState = CursorLockMode.Locked; //Trava o cursor no meio da tela
        gc = FindAnyObjectByType<GameController>();
    }




    void Update()
    {
        //-------------------------------------------------------------------------------------------------//
        //---------------------------------------------Camera----------------------------------------------//
        //-------------------------------------------------------------------------------------------------//
        

        
        if(Time.timeScale != 0) //Armazena os movimentos do mouse apenas se o jogo não estiver parado
        {
            mouseX = Input.GetAxis("Mouse X") * sensibilidadeMouse;
            mouseY = Input.GetAxis("Mouse Y") * sensibilidadeMouse; 
        }
        
        xRotation -= mouseY; //Inverte o movimento da camera e armazena em uma nova variavel

        
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //Limita o movimento vertical em 90 graus para cada lado impedindo que de uma volta completa e a camera se inverta por exemplo

        
        pCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);//Modifica a rotação da camera 

        
        transform.Rotate(Vector3.up * mouseX);//Rotaciona o jogador com o movimento horizontal do mouse para que ele ande na direção apontada





        //-------------------------------------------------------------------------------------------------//
        //--------------------------------------------Movimento--------------------------------------------//
        //-------------------------------------------------------------------------------------------------//
        playerX = Input.GetAxisRaw("Horizontal"); //Captura o movimento lateral
        playerY = Input.GetAxisRaw("Vertical"); //Captura o movimento para frente e para trás

        
        Vector3 move = transform.forward * playerY + transform.right * playerX;//Transforma os inputs em um vetor

        /*Utiliza a função Move para mover o player, usando o vetor move, um valor de velocidade e multiplica pelo tempo desde o ultimo
        frame(Time.deltatime) para que não mude de maquina para maquina*/
        controller.Move(move * velocidade * Time.deltaTime);





        //-------------------------------------------------------------------------------------------------//
        //--------------------------------------------Raycast----------------------------------------------//
        //-------------------------------------------------------------------------------------------------//
        
        rayBateu = Physics.Raycast(pCamera.transform.position, pCamera.transform.forward, out hit, distanciaDePegar);

        //Caso o jogador esteja olhando para um deposito e não esteja segurando nada, permite que ele aperte "E" para abrir o deposito
        if (rayBateu && hit.transform.CompareTag("Deposito") && objetoSegurado == null) 
        {
            mira.GetComponent<Image>().sprite = miraInteragir; //Muda o icone de mira para que o jogador perceba que pode interagir
            if(Input.GetKeyDown(KeyCode.E))
            {
                ingredientesInterface.AbrirDeposito(hit.transform.gameObject); //Envia qual objeto está olhando para que possa ser comparado e abrir a interface correta
            }
        }
        else {mira.GetComponent<Image>().sprite = miraBase;} //Caso não olhe para nada coletavel volta a mira para o normal

        //Caso o jogador olhe para o prato e está segurando algo, esse algo será colocado no sanduiche
        if (rayBateu && hit.transform.CompareTag("Prato") && objetoSegurado != null)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                hit.transform.gameObject.GetComponent<PratoBehaviour>().ReceberIngrediente(objetoSegurado);
                objetoSegurado = null;
            }
        }

        //Caso o jogador olhe para o lixo com algo na mão, destruira o que está sendo segurado e vai retirar pontos
        if (rayBateu && hit.transform.CompareTag("Lixo") && objetoSegurado != null)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Destroy(objetoSegurado);
                objetoSegurado = null;
                gc.RemovePontos(5);

            }
        }
    }

    public void Pegar(GameObject objetoRecebido) //Pega objeto que essa função receber e coloca na area dos itens
    {
        objetoRecebido.transform.parent = areaItens;
        objetoRecebido.transform.localPosition = Vector3.zero;
        objetoRecebido.transform.localRotation = Quaternion.Euler(Vector3.zero);
        objetoSegurado = objetoRecebido;
    }

}
