using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main;
    public bool gameOver;

    private void Start()
    {
        main = this;
    }
}
