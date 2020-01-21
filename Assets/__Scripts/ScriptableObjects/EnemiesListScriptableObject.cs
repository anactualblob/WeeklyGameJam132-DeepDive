using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesListScriptableObject.asset", menuName = "Scriptable Object/Enemies List")]
public class EnemiesListScriptableObject : ScriptableObject
{
    public List<EnemyInfo> enemyInfoList = new List<EnemyInfo>();

    public static List<EnemyInfo> spawnableEnemies = new List<EnemyInfo>();



    public GameObject GetRandomSpawnableEnemy()
    {
        if (spawnableEnemies.Count > 0)
        {
            int i = Random.Range(0, spawnableEnemies.Count);
            return spawnableEnemies[i].prefab;
        }
        else
        {
            Debug.LogError("EnemiesListScriptableObject.cs : Couldn't get random spawnable enemy from spawnableEnemies because the list is empty.");
            return null;
        }
        
    }
}

[System.Serializable]
public class EnemyInfo
{
    public string name;
    [Space]
    [Tooltip("Minimum depth at which the attached prefab can be spawned. Must be divisible by 10.")]
    public float minDepth;
    [Tooltip("Maximum depth at which the attached prefab can be spawned. Must be divisible by 10.")]
    public float maxDepth;

    public GameObject prefab;


    /// <summary>
    /// Adds this EnemyInfo instance to the spawnableEnemies list on EnemiesListScriptableObject if the given depth is in its minDepth-maxDepth range, 
    /// and removes it if depth is out of its range and it's still in the list.
    /// </summary>
    /// <param name="depth"></param>
    public void DetermineSpawnable(float depth)
    {
        if (depth <= minDepth && depth > maxDepth && !EnemiesListScriptableObject.spawnableEnemies.Contains(this))
        {
            Debug.Log("adding enemy : " + this.name);
            EnemiesListScriptableObject.spawnableEnemies.Add(this);
        }
        else if ( (depth > minDepth || depth <= maxDepth) && EnemiesListScriptableObject.spawnableEnemies.Contains(this))
        {
            Debug.Log("removing enemy : " + this.name);
            EnemiesListScriptableObject.spawnableEnemies.Remove(this);
        }
    }
}