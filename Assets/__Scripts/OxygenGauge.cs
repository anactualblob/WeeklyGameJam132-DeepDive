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
        Debug.Log(startWidth * (gaugeLevel / gaugeMax));

        //gaugeMask.rect.Set(gaugeMask.rect.x, gaugeMask.rect.y, startWidth * (gaugeLevel / gaugeMax), gaugeMask.rect.height);

        gaugeMask.sizeDelta = new Vector2(startWidth * (gaugeLevel / gaugeMax), gaugeMask.sizeDelta.y);
    }

}
