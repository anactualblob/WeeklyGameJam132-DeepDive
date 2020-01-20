using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DiveController))]
public class PlayerInput : MonoBehaviour
{
    
    DiveController diveController;

    void Awake()
    {
        diveController = GetComponent<DiveController>();
    }

    
    void Update()
    {
        if (Input.GetButtonDown("DiveLeft"))
        {
            diveController.DiveLeft();
        } 
        else if (Input.GetButtonDown("DiveRight"))
        {
            diveController.DiveRight();   
        }
    }

}
