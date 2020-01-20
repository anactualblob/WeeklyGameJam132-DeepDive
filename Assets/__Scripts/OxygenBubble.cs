using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]
public class OxygenBubble : MonoBehaviour
{

    public float oxygenRegain = 30;
    [Space]
    [SerializeField] float floatUpSpeed = 1;

    
    void Update()
    {
        FloatUp();
    }


    /// <summary>
    /// Moves the object upwards
    /// </summary>
    void FloatUp()
    {
        transform.Translate (Vector3.up * floatUpSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ( other.CompareTag("Player") )
        {
            Oxygen ox = other.GetComponent<Oxygen>();
            ox.RegainOxygen(oxygenRegain);
            Destroy(gameObject);
        }
    }

}
