using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDepthColor : MonoBehaviour
{
    [Tooltip("The depth at which the color starts to change")]
    [SerializeField] float startDepth;
    [Tooltip("The depth at which the color stops changing")]
    [SerializeField] float maxDepth;

    [SerializeField] Gradient depthColorGradient;

   
    
    void Update()
    {
        float u;
        

        if (Diver.DEPTH < startDepth)
        {
            u = Diver.DEPTH / maxDepth; // get a 0-1 value
        } 
        else
        {
            u = 0; // if we haven't hit the depth where the gradient starts to change, u stays 0
        }
        
        Camera.main.backgroundColor = depthColorGradient.Evaluate(u);
    }
}
