using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float randomAngleRange;
    [Space]
    [Tooltip("Lifetime of enemy in seconds. If its lifetime is over, the enemy will destroy the next time it leaves the screen.")]
    [SerializeField] float enemyLifetime;

    Vector2 direction;

    bool needsToSwitchDirection = false;

    float leftBound;
    float rightBound;

    float topBound;


    private void Start()
    {
        // direction must be a normalized vector pointing horizontally in the direction of the camera with no vertical component
        direction = Camera.main.transform.position - transform.position;
        direction.y = 0;
        direction = direction.normalized;

        // orienting direction by a random angle between randomAngleRange and -randomAngleRange
        direction = Quaternion.AngleAxis(Random.Range(-randomAngleRange, randomAngleRange), Vector3.back) * direction;



        // finds half the width of the camera
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = screenAspect * halfHeight;
        
        // leftbound and rightbound are the left and right borders of the camera (plus a little margin)
        leftBound = -halfWidth - 0.3f;
        rightBound = halfWidth + 0.3f;

    }


    
    void Update()
    {


        transform.Translate(direction * speed * Time.deltaTime);

        enemyLifetime -= Time.deltaTime;

        // switch direction if the game object goes off screen
        if ( (transform.position.x < leftBound || transform.position.x > rightBound))
        {
            if (needsToSwitchDirection) direction = -direction;
            if (enemyLifetime <= 0) Destroy(gameObject);
        } 
        else
        {
            needsToSwitchDirection = true;
        }

        // destroy game object if it goes higher than the camera view (camera y position + orthSize)
        topBound = Camera.main.transform.position.y + Camera.main.orthographicSize;
        if (transform.position.y > topBound)
        {
            Destroy(gameObject);
        }
        
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // if an enemy collides with the player, the player escapes the enemy
            Diver diver = other.gameObject.GetComponent<Diver>();
            diver.EscapeEnemy();
        }
    }

}
