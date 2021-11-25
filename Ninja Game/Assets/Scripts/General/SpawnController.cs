using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private float spawnCooldown = 3;
    private bool canSpawnObject = true;
    private bool allCoroutinesStopped;
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;

    private void Awake()
    {
        allCoroutinesStopped = false;
    }

    private void Update()
    {
        if (!GameManager.main.gameOver)
        {
            if (Random.Range(0, 2) == 1)
            {
                SpawnObject(ObjectPooler.main.SpawnEnemy());
            }
            else
            {
                SpawnObject(ObjectPooler.main.SpawnObstacle());
            }
        }
        else if (!allCoroutinesStopped)
        {
            StopAllCoroutines();
            allCoroutinesStopped = true;
        }
    }

    private void SpawnObject(GameObject objToSpawn)
    {
        if (canSpawnObject)
        {
            GameObject obj = objToSpawn;
            if (obj != null)
            {
                float randomXPos = Random.Range(leftWall.transform.position.x + 1, rightWall.transform.position.x - 1);
                Vector3 objSpawnPoint = new Vector3(randomXPos, 10, 0);
                obj.transform.position = objSpawnPoint;
                obj.SetActive(true);
                StartCoroutine(ObjectSpawnCooldown());
            }
        }
    }

    IEnumerator ObjectSpawnCooldown()
    {
        canSpawnObject = false;
        yield return new WaitForSeconds(spawnCooldown);
        canSpawnObject = true;
    }
}
