using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Diver))]
public class PlayerInput : MonoBehaviour
{
    
    Diver diver;

    void Awake()
    {
        diver = GetComponent<Diver>();
    }

    
    void Update()
    {
        if (Input.GetButtonDown("DiveLeft"))
        {
            diver.DiveLeft();
        } 
        else if (Input.GetButtonDown("DiveRight"))
        {
            diver.DiveRight();   
        }
    }

}
