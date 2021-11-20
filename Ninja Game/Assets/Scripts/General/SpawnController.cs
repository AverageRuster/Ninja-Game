using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    float spawnCooldown = 3;
    bool canSpawnEnemy;
    bool allCoroutinesStopped;

    private void Awake()
    {
        canSpawnEnemy = true;
        allCoroutinesStopped = false;
    }

    private void Update()
    {
        if (!GameManager.main.gameOver)
        {
            SpawnEnemy();
        }
        else if (!allCoroutinesStopped)
        {
            StopAllCoroutines();
            allCoroutinesStopped = true;
        }
    }

    private void SpawnEnemy()
    {
        if (canSpawnEnemy)
        {
            GameObject enemy = ObjectPooler.main.SpawnEnemy();
            if (enemy != null)
            {
                enemy.transform.position = new Vector3(0, 10, 0);
                enemy.SetActive(true);
                StartCoroutine(EnemySpawnCooldown());
            }
        }
    }

    IEnumerator EnemySpawnCooldown()
    {
        canSpawnEnemy = false;
        yield return new WaitForSeconds(spawnCooldown);
        canSpawnEnemy = true;
    }
}
