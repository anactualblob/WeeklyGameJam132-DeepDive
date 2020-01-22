using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemiesListScriptableObject enemyListSO;
    [Space]
    [Tooltip("Interval in depth meters at which the spawnable enemies list is updated")]
    [SerializeField] int spawnableEnemiesUpdateInterval;
    [Space]
    [SerializeField] float spawnDelay;
    [SerializeField] float secondsBeforeFirstSpawn;
    [SerializeField] int maxEnemiesInScene;
    [Space]
    [Tooltip("Minimum vertical offset from the camera center where enemies spawn.")]
    [SerializeField] float minVerticalSpawnOffset = 0;
    [Tooltip("Maximum vertical offset from the camera center where enemies spawn.")]
    [SerializeField] float maxVerticlaSpawnOffset = 0;
    [Tooltip("How far horizontally from the edge of the camera should enemies spawn.")]
    [SerializeField] float horizontalSpawnMargin = 0;

    float countdown = 0;


   
    int depthInteger = 0;
    int previousDepthInteger = -1;


    List<GameObject> enemiesInScene = new List<GameObject>();


    private void Start()
    {
        Debug.Log("Start");
        UpdateSpawnableEnemies();
    }


    private void Update()
    {

        // Basically, calling UpdateSpawnableEnemies every spawnableEnemiesUpdateInterval meters of depth.
        //  We divide DEPTH as an int (it truncates every decimal digit) by spawnableEnemiesUpdateInterval.
        //  This gives us an int that changes each time DEPTH reaches a value divisible by spawnableEnemiesUpdateInterval.
        //  When the value of the int division is different than the cached int, we call UpdateSpawnableEnemies and cache the new value.
        depthInteger = (int)Diver.DEPTH / spawnableEnemiesUpdateInterval;
        
        if (depthInteger != previousDepthInteger)
        {
            
            UpdateSpawnableEnemies();
            previousDepthInteger = depthInteger;
        }



        // Spawning enemies :
        // wait until the secondsBeforFirstSpawn is passed
        if (Time.realtimeSinceStartup < secondsBeforeFirstSpawn)
        {
            return;
        }
            



        // countdown every frame
        countdown -= Time.deltaTime;

        // if the countdown reaches 0, spawn an enemy and reset countdown
        if (countdown <= 0)
        {
            Debug.Log("SpawningEnemy");
            InstantiateEnemy();
            countdown = spawnDelay;
        }
        

    }



    /// <summary>
    /// Calls Register() on every enemyInfo prefab in enemyListSO.enemyInfoList.
    /// </summary>
    public void UpdateSpawnableEnemies()
    {
        foreach(EnemyInfo enemyInfo in enemyListSO.enemyInfoList)
        {
            enemyInfo.DetermineSpawnable(Diver.DEPTH);
        }
    }



    /// <summary>
    /// Instantiate a random enemy as a child of this GameObject.
    /// </summary>
    public void InstantiateEnemy()
    {
        Vector2 spawnPos = new Vector2();

        // spawn the enemy at a vertical offset from the height of the camera
        spawnPos.y = Camera.main.transform.position.y + Random.Range(minVerticalSpawnOffset, maxVerticlaSpawnOffset);
        
        // this finds half the width of the camera viewport
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float camHalfHeight = Camera.main.orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;

        // the x position where the enemy spawns is outside the camera viewport either on the right or left.
        spawnPos.x = Mathf.Sign(Random.Range(-1.0f, 1.0f)) * (horizontalSpawnMargin + camHalfWidth);


        // get a random enemy prefab from the list of spawnable enemies;
        GameObject enemy = enemyListSO.GetRandomSpawnableEnemy();


        // instantiate if GetRandomSpawnableEnemy returned a valid enemy prefab
        if (enemy != null)
        {
            GameObject instEnemy = Instantiate(enemy, spawnPos, Quaternion.identity);

            // sets this game object as parent
            instEnemy.transform.SetParent(this.transform);

        }
        else
        {
            Debug.LogError("EnemySpawner.cs : Couldn't get enemy prefab from EnemiesListScriptableObject.spawnableEnemies .");
        }
        
    }
}
