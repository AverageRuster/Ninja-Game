using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeactivator : MonoBehaviour
{
    private void Update()
    {
        if (gameObject.transform.position.y < -20)
        {
            gameObject.SetActive(false);
        }
    }
}
