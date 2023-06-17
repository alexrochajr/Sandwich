using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    //Os sprites que tiverem esse script ficarão sempre virados para camera
    private Vector3 mainCamera;

    
    void Update()
    {
        mainCamera = Camera.main.transform.forward;
        mainCamera.y = 0;

        transform.rotation = Quaternion.LookRotation(mainCamera);
    }
}
