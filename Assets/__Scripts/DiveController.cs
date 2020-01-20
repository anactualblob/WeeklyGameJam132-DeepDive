using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveController : MonoBehaviour
{

    [SerializeField] float diveAngle = 45;
    [SerializeField] float diveForce = 1;
    [SerializeField] float waterDragMultiplier = 1;

    [Space]
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
        if (velocity.magnitude > 0.0001)
        {
            velocity.Scale(new Vector2(waterDragMultiplier, waterDragMultiplier));
        }
        else
        {
            velocity = Vector2.zero;
        }
        

        velocity += acceleration;

        transform.Translate( velocity );


        acceleration = Vector2.zero;
    }


    public void DiveLeft()
    {
        acceleration += (Vector2)(Quaternion.AngleAxis(diveAngle, Vector3.back) * Vector2.down * diveForce * Time.deltaTime);
    }

    public void DiveRight()
    {
        acceleration += (Vector2)(Quaternion.AngleAxis(-diveAngle, Vector3.back) * Vector2.down * diveForce * Time.deltaTime);
    }
}
