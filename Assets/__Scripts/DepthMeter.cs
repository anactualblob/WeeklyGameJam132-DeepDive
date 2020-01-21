using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DepthMeter : MonoBehaviour
{

    [SerializeField] RectTransform meterNeedle;
    // this doesn't work so fuck it
    // might come back to it later
    [SerializeField] float startNeedleAngle = -10;
    [SerializeField] float maxNeedleAngle = -180;
    [Space]
    [SerializeField] TextMeshProUGUI depthText;

    [Header("Debug (set this in Diver.cs on the player)")]
    public float depth;
    public float maxDepth;




    private void Update()
    {

        if (depth <= 0)
        {
            depthText.text = (Mathf.Abs(depth)).ToString("N0");


            // lerp between startNeedleAngle and maxNeedleAngle by depth/maxDepth
            float u = Mathf.Lerp(startNeedleAngle, maxNeedleAngle, depth / maxDepth);

            // -u because u comes out positive and it makes the needle rotate the wrong way
            meterNeedle.localRotation = Quaternion.AngleAxis(-u, Vector3.back);
        }
        else
        {
            depthText.text = "0";
        }
        
    }
}
