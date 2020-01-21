using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float speed;

    Vector2 direction;

    private void Start()
    {
        // direction must be a normalized vector pointing horizontally in the direction of the camera with no vertical component
        direction = Camera.main.transform.position - transform.position;
        direction.y = 0;
        direction = direction.normalized;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           Diver diver = other.gameObject.GetComponent<Diver>();
           diver.EscapeEnemy();
        }
    }

}
