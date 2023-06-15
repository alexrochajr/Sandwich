using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject timer; //Objeto UI do timer
    private TMPro.TextMeshProUGUI timerText; //Especificamente o elemento de texto dentro do timer

    void Start()
    {
        timerText = timer.GetComponent<TMPro.TextMeshProUGUI>();
    }

    //A cada segundo altera o texto do timer
    public IEnumerator Contar()
    {
        timerText.text = "3";
        yield return new WaitForSecondsRealtime(1.2f);
        timerText.text = "2";
        yield return new WaitForSecondsRealtime(1);
        timerText.text = "1";
        yield return new WaitForSecondsRealtime(1);
        timerText.text = "VAI";
        yield return new WaitForSecondsRealtime(1);
    }
}
