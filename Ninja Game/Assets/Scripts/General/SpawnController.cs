using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private float spawnCooldown = 3;
    private bool canSpawnEnemy;
    private bool allCoroutinesStopped;
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;

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
                float randomXPos = Random.Range(leftWall.transform.position.x + 1, rightWall.transform.position.x - 1);
                Vector3 enemySpawnPoint = new Vector3(randomXPos, 10, 0);
                enemy.transform.position = enemySpawnPoint;
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
