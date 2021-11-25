using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public void KillEnemy(bool horizontalDirection)
    {
        string spritePath;
        if (horizontalDirection)
        {
            spritePath = "EnemyRemains/HorizontalEnemyRemains";
        }
        else
        {
            spritePath = "EnemyRemains/VerticalEnemyRemains";
        }

        for (int i = 0; i < 2; i++)
        {
            GameObject currentEnemyRemains = ObjectPooler.main.SpawnEnemyRemains();
            currentEnemyRemains.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spritePath + i);
            currentEnemyRemains.transform.position = transform.position;
            currentEnemyRemains.SetActive(true);
            currentEnemyRemains.GetComponent<Rigidbody2D>().AddForce((-2 * i + 1) * Vector3.left * 250);
        }

        gameObject.SetActive(false);
    }
}
