using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diver : MonoBehaviour
{

    [SerializeField] float diveAngle = 45;
    [SerializeField] float diveForce = 1;
    [SerializeField] float waterDragMultiplier = 1;
    [SerializeField] float maxVelocityDown = 1;
    [SerializeField] float maxVelocitySide = 1;

    [Header("Debug")]
    public Vector2 velocity;
    public Vector2 acceleration;

    void Start()
    {
        velocity = Vector2.zero;
        acceleration = Vector2.zero;
    }

    
    void Update()
    {
        // clamp vertical velocity
        if (Mathf.Abs(velocity.y) > maxVelocityDown)
        {
            velocity.y = Mathf.Sign(velocity.y) * maxVelocityDown;
        }

        //clamp horizontal velocity
        if (Mathf.Abs(velocity.x) > maxVelocitySide)
        {
            velocity.x = Mathf.Sign(velocity.x) * maxVelocitySide;
        }


        // if velocity is greater than a value, scale it down by waterDragMultiplier (must be < 0)
        // this simulates water drag
        if (velocity.magnitude > 0.0001)
        {
            velocity.Scale(new Vector2(waterDragMultiplier, waterDragMultiplier));
        }
        else
        {
            velocity = Vector2.zero;
        }

        
        // resolve position, velocity and aceleration
        velocity += acceleration;
        transform.Translate( velocity );
        acceleration = Vector2.zero;
        
    }


    /// <summary>
    /// Makes the player dive left.
    /// <para>
    /// Adds to the player's acceleration Vector2.down, oriented according to diveAngle, scaled by Time.deltaTime and diveForce.
    /// </para>
    /// </summary>
    public void DiveLeft()
    {
        acceleration += (Vector2)(Quaternion.AngleAxis(diveAngle, Vector3.back) * Vector2.down * diveForce * Time.deltaTime);

        //spend oxygen
        Oxygen.SpendOxygenDive();
    }

    /// <summary>
    /// Makes the player dive right.
    /// <para>
    /// Adds to the player's acceleration Vector2.down, oriented according to -diveAngle, scaled by Time.deltaTime and diveForce.
    /// </para>
    /// </summary>
    public void DiveRight()
    {
        acceleration += (Vector2)(Quaternion.AngleAxis(-diveAngle, Vector3.back) * Vector2.down * diveForce * Time.deltaTime);

        //spend oxygen
        Oxygen.SpendOxygenDive();
    }
}
