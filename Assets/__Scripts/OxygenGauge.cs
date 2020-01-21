using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenGauge : MonoBehaviour
{   
    
    [SerializeField] RectTransform gaugeMask;
    [Header("Set Dynamically")]
    public float gaugeMax;
    public float gaugeLevel;

    [Header("Debug")]
    [SerializeField] float startWidth;

    private void Awake()
    {
        startWidth = gaugeMask.rect.width;
    }



    void Update()
    {
        // set size to a percentage of startWidth determined by the current and maximum oxygen levels
        // since all 4 anchors are at the same position, sizeDelta is the same as size
        gaugeMask.sizeDelta = new Vector2(startWidth * (gaugeLevel / gaugeMax), gaugeMask.sizeDelta.y);
    }

}
