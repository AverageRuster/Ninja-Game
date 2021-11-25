using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler main;

    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject enemyRemains;
    [SerializeField] private GameObject obstacle;
    [SerializeField] private int enemiesCount;
    [SerializeField] private int obstacleCount;

    private List<GameObject> enemiesList;
    private List<GameObject> enemyRemainsList;
    private List<GameObject> obstaclesList;

    private void Start()
    {
        enemiesList = new List<GameObject>();
        enemyRemainsList = new List<GameObject>();
        obstaclesList = new List<GameObject>();
        main = this;
        InstantiateObjects(enemy, enemiesList, enemiesCount);
        InstantiateObjects(enemyRemains, enemyRemainsList, enemiesCount * 2);
        InstantiateObjects(obstacle, obstaclesList, obstacleCount);
    }

    private void InstantiateObjects(GameObject gameObjectToInstantiate, List<GameObject> listOfGameObjects, int countOfGameObjects)
    {
        GameObject currentObject;
        for (int i = 0; i < countOfGameObjects; i++)
        {
            currentObject = Instantiate(gameObjectToInstantiate);
            listOfGameObjects.Add(currentObject);
            currentObject.SetActive(false);
        }
    }

    public GameObject SpawnEnemy()
    {
        return SpawnGameObject(enemiesCount, enemiesList);
    }

    public GameObject SpawnEnemyRemains()
    {
        return SpawnGameObject(enemiesCount * 2, enemyRemainsList);
    }

    public GameObject SpawnObstacle()
    {
        return SpawnGameObject(obstacleCount, obstaclesList);
    }

    private GameObject SpawnGameObject(int countOfGameObjects, List<GameObject> listOfGameobjects)
    {
        for (int i = 0; i < countOfGameObjects * 2; i++)
        {
            if (!listOfGameobjects[i].activeInHierarchy)
            {
                return listOfGameobjects[i];
            }
        }
        return null;
    }
}
