using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
  [SerializeField] GameObject pathPrefab;
  [SerializeField] GameObject enemyPrefab;

  [SerializeField] int numberOfEnemies = 5;

  [SerializeField] float moveSpeed = 2f;
  [SerializeField] float timeBetweenSpawns = 0.5f;
  [SerializeField] float spawnRandomFactor = 0.3f;

  public List<Transform> GetWaypoints()
  {

    var waveWaypoints = new List<Transform>();

    foreach (Transform child in pathPrefab.transform)
    {
      waveWaypoints.Add(child);
    }

    return waveWaypoints;
  }

  public GameObject GetEnemyPrefab() { return enemyPrefab; }

  public int GetNumberOfEnemies() { return numberOfEnemies; }

  public float GetMoveSpeed() { return moveSpeed; }

  public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }

  public float GetSpawnRandomFactor() { return spawnRandomFactor; }
}
