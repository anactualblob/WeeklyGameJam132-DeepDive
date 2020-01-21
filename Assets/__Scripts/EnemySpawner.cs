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

    float countdown = 0;


    [Header("Debug")]
    public int depthInteger = 0;
    public int previousDepthInteger = 0;



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
        // TODO : spawn at camera y position and either to the left or to the right
        Vector2 spawnPos = new Vector2();

        spawnPos.y = Camera.main.transform.position.y;

        // get a random enemy prefab from the list of spawnable enemies;
        GameObject enemy = enemyListSO.GetRandomSpawnableEnemy();

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
