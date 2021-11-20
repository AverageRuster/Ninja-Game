using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    Rigidbody2D rb;

    private Vector3 touchTargetPoint;
    private Vector3 movementVector;

    private bool canPlayerMove = false;
    private float playerSpeed = 1000;
    private float maxPlayerSpeed = 10;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetControlls();
        Debug.Log(rb.velocity.magnitude);
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude < maxPlayerSpeed)
        {
            rb.AddForce(movementVector * playerSpeed * Time.fixedDeltaTime);
        }
    }

    private void GetControlls()
    {
        Touch mainTouch;
        if (Input.touchCount > 0)
        {
            //Берем самое первое касание
            mainTouch = Input.GetTouch(0);


            //Если касание только началось или если игрок ведет палец по экрану, запоминаем его позицию как целевую
            if (mainTouch.phase == TouchPhase.Began || mainTouch.phase == TouchPhase.Moved)
            {
                touchTargetPoint = mainCamera.ScreenToWorldPoint(mainTouch.position);
            }

            //Если игрок отпустил палец, начинаем движение
            else if (mainTouch.phase == TouchPhase.Ended)
            {
                rb.velocity = Vector3.zero;
                movementVector = touchTargetPoint - transform.position;
                
            }
        }
    }
}
