using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler main;

    [SerializeField] private GameObject enemy;  
    [SerializeField] private int enemiesCount;
    private List<GameObject> enemiesList;

    [SerializeField] private GameObject enemyRemains;
    private List<GameObject> enemyRemainsList;

    private void Start()
    {
        main = this;
        InstantiateObjects();
    }

    private void InstantiateObjects()
    {
        enemiesList = new List<GameObject>();
        enemyRemainsList = new List<GameObject>();
        GameObject currentEnemy;
        GameObject currentEnemyRemains;
        for (int i = 0; i < enemiesCount; i++)
        {
            currentEnemy = Instantiate(enemy);
            enemiesList.Add(currentEnemy);
            currentEnemy.SetActive(false);
        }
        for (int i = 0; i < enemiesCount * 2; i++)
        {
            currentEnemyRemains = Instantiate(enemyRemains);
            enemyRemainsList.Add(currentEnemyRemains);
            currentEnemyRemains.SetActive(false);
        }
    }

    public GameObject SpawnEnemy()
    {
        for (int i = 0; i < enemiesCount; i++)
        {
            if (!enemiesList[i].activeInHierarchy)
            {
                return enemiesList[i];
            }           
        }
        return null;
    }

    public GameObject SpawnEnemyRemains()
    {
        for (int i = 0; i < enemiesCount * 2; i++)
        {
            if (!enemyRemainsList[i].activeInHierarchy)
            {
                return enemyRemainsList[i];
            }
        }
        return null;
    }
}
