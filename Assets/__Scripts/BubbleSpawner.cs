using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{

    public GameObject bubblePrefab;
    [Space]
    [Tooltip("Limit of bubble spawn position, in units away from x = 0")]
    [SerializeField] float horizontalSpawnBound;
    [Tooltip("Senconds between bubble spawn")]
    [SerializeField] float spawnDelay;
    [SerializeField] float delayBeforeFirstSpawn;

    float countdown = 0;


    void Update()
    {
        // wait until the delayBeforFirstSpawn is passed
        if (Time.realtimeSinceStartup < delayBeforeFirstSpawn)
        {
            return;
        }

        // countdown every frame
        countdown -= Time.deltaTime;

        // if the countdown reaches 0, spawn a bubble and reset it
        if (countdown <= 0)
        {
            SpawnBubble();
            countdown = spawnDelay;
        }



    }



    /// <summary>
    /// Instantiate an oxygen bubble at a random x position and under the viewport of the camera
    /// </summary>
    void SpawnBubble()
    {
        float randomX = Random.Range(-horizontalSpawnBound, horizontalSpawnBound);
        
        float y = Camera.main.transform.position.y - Camera.main.orthographicSize - 0.2f;


        Vector3 pos = new Vector3(randomX, y, 0);
        GameObject newBubble = Instantiate(bubblePrefab, pos, Quaternion.identity);
        newBubble.transform.SetParent(transform);
    }



}
