using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler main;

    [SerializeField] private GameObject enemy;  
    [SerializeField] private int enemiesCount;
    private List<GameObject> enemies;

    private void Start()
    {
        main = this;
        InstantiateObjects();
    }

    private void InstantiateObjects()
    {
        enemies = new List<GameObject>();
        GameObject currentEnemy;
        for (int i = 0; i < enemiesCount; i++)
        {
            currentEnemy = Instantiate(enemy);
            enemies.Add(currentEnemy);
            currentEnemy.SetActive(false);
        }
    }

    public GameObject SpawnEnemy()
    {
        for (int i = 0; i < enemiesCount; i++)
        {
            if (!enemies[i].activeInHierarchy)
            {
                return enemies[i];
            }           
        }
        return null;
    }
}
