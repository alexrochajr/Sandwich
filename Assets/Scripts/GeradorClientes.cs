using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorClientes : MonoBehaviour
{
    public GameObject cliente; //Prefab do cliente

    public void GerarCliente(out GameObject clienteSaida) //Instancia um cliente novo e deixa ele como filho do gerador, alem de arrumar sua posição
    {
        GameObject novoCliente = Instantiate(cliente);
        novoCliente.transform.parent = this.transform;
        novoCliente.transform.localPosition = new Vector3(0, 1, 0);
        clienteSaida = novoCliente;
    }
}
