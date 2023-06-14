using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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

    private RaycastHit hit;
    private bool rayBateu;
    public float distanciaDePegar = 10f;
    public Transform areaItens;
    private GameObject objetoSegurado;
    


    void Start()
    {
        pCamera = Camera.main; //pCamera agora corresponde a "Main Camera" na cena
        Cursor.lockState = CursorLockMode.Locked; //Trava o cursor no meio da tela
    }






    void Update()
    {
        //-------------------------------------------------------------------------------------------------//
        //---------------------------------------------Camera----------------------------------------------//
        //-------------------------------------------------------------------------------------------------//
        

        //Armazena os movimentos do mouse apenas se o jogo não estiver parado
        if(Time.timeScale != 0){
            mouseX = Input.GetAxis("Mouse X") * sensibilidadeMouse;
            mouseY = Input.GetAxis("Mouse Y") * sensibilidadeMouse; 
        }
        //Inverte o movimento da camera e armazena em uma nova variavel
        xRotation -= mouseY;

        //Limita o movimento vertical em 90 graus para cada lado impedindo que de uma volta completa e a camera se inverta por exemplo
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Modifica a rotação da camera 
        pCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //Rotaciona o jogador com o movimento horizontal do mouse para que ele ande na direção apontada
        transform.Rotate(Vector3.up * mouseX);





        //-------------------------------------------------------------------------------------------------//
        //--------------------------------------------Movimento--------------------------------------------//
        //-------------------------------------------------------------------------------------------------//
        playerX = Input.GetAxisRaw("Horizontal"); //Captura o movimento lateral
        playerY = Input.GetAxisRaw("Vertical"); //Captura o movimento para frente e para trás

        //Transforma os inputs em um vetor
        Vector3 move = transform.forward * playerY + transform.right * playerX;

        /*Utiliza a função Move para mover o player, usando o vetor move, um valor de velocidade e multiplica pelo tempo desde o ultimo
        frame(Time.deltatime) para que não mude de maquina para maquina*/
        controller.Move(move * velocidade * Time.deltaTime);





        //-------------------------------------------------------------------------------------------------//
        //--------------------------------------------Raycast----------------------------------------------//
        //-------------------------------------------------------------------------------------------------//
        
        rayBateu = Physics.Raycast(pCamera.transform.position, pCamera.transform.forward, out hit, distanciaDePegar);

        if (rayBateu && hit.transform.CompareTag("Ingrediente"))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                hit.transform.gameObject.GetComponent<PickableBehaviour>().Pegar(out objetoSegurado);
                objetoSegurado.transform.parent = areaItens;
                objetoSegurado.transform.localPosition = Vector3.zero;
            }
        }
    }

}
