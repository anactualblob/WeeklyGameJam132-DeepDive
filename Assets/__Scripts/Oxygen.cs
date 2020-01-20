using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen : MonoBehaviour
{

    static Oxygen _S;
    static Oxygen S
    {
        get 
        {
            if (_S != null) return _S;
            Debug.LogError("Oxygen.cs : trying to get Oxygen Sigleton instance but it has not been assigned." );
            return null;
        }
        set 
        {
            if (_S != null) 
            {
                Debug.LogError("Oxygen.cs : trying to set Oxygen Sigleton instance again." );
                return;
            }
            _S = value;
        }
    }

    [Header("Oxygen Values")]
    [Tooltip("Quantity of oxygen the player starts with")]
    [SerializeField] float startingOxygen;
    [Tooltip("How much oxygen the players loses every second")]
    [SerializeField] float constantOxygenCost;
    [Tooltip("How Much oxygen the player loses when diving")]
    [SerializeField] float diveOxygenCost;

    [Space]
    [Header("Debug")]
    public float oxygenLevel;
    public static float OXYGEN_LEVEL
    {
        get { return S.oxygenLevel; }
        set 
        {
            S.oxygenLevel = (value <= 0) ? 0 : value;
        }
    }


    void Awake()
    {
        _S = this;
        OXYGEN_LEVEL = startingOxygen;
    }



    void Update()
    {
        // oxygen level drops at a rate of constantOxygenCost/second
        OXYGEN_LEVEL -= constantOxygenCost * Time.deltaTime;
    }


    /// <summary>
    /// Substracts Oxygen.diveOxygenCost from Oxygen.OXYGEN_LEVEL.
    /// <para>
    /// Call this when diving.
    /// </para>
    /// </summary>
    static public void SpendOxygenDive()
    {
        OXYGEN_LEVEL -= S.diveOxygenCost;
    }


    /// <summary>
    /// Restores oxygen by a given amount.
    /// </summary>
    /// <param name="regain">Amount of oxygen to regain.</param>
    public void RegainOxygen(float regain)
    {
        OXYGEN_LEVEL = (OXYGEN_LEVEL + regain >= startingOxygen) ? startingOxygen : OXYGEN_LEVEL + regain;
    }


}
