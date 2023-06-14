using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableBehaviour : MonoBehaviour
{
    public GameObject item;
    
    public void Pegar(out GameObject itemSaida)
    {
        itemSaida = Instantiate(item);
    }
}
