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

    private bool isPlayerMoving = false;
    private bool isPlayerOnTheLeftWall = true;
    private float playerSpeed = 10000;
    private float maxPlayerSpeed = 20;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        SetGravity();
        GetControlls();
    }

    private void FixedUpdate()
    {
        //Двигаем игрока
        if (isPlayerMoving && rb.velocity.magnitude < maxPlayerSpeed)
        {
            rb.AddForce(movementVector * playerSpeed * Time.fixedDeltaTime);
        }
    }

    private void SetGravity()
    {
        //Настраиваем гравитацию в зависимости от того, зацепился ли игрок за стену
        if (!isPlayerMoving)
        {
            rb.gravityScale = 0.5f;
        }
        else
        {
            rb.gravityScale = 5f;
        }
    }

    private void GetControlls()
    {
        if (!isPlayerMoving && Input.touchCount > 0)
        {
            Touch mainTouch;

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
                movementVector = (touchTargetPoint - transform.position + new Vector3(0, 0, 10)).normalized;
                if (isPlayerOnTheLeftWall && movementVector.x > 0 || !isPlayerOnTheLeftWall && movementVector.x < 0)
                {
                    rb.velocity = Vector3.zero;
                    isPlayerMoving = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Игрок цепляется за стену
        if (collision.CompareTag("Wall"))
        {
            //Левая стенка
            if (transform.position.x < 0)
            {
                transform.position = new Vector3(collision.transform.position.x + 1, transform.position.y, transform.position.z);
                isPlayerOnTheLeftWall = true;
            }

            //Правая стенка
            else
            {
                transform.position = new Vector3(collision.transform.position.x - 1, transform.position.y, transform.position.z);
                isPlayerOnTheLeftWall = false;
            }
            rb.velocity = Vector3.zero;
            isPlayerMoving = false;
        }

        //Игрок попал по врагу, убиваем врага
        if (collision.CompareTag("Enemy"))
        {
            
            collision.gameObject.SetActive(false);
        }

        //Игрок попал в потолок
        if (collision.CompareTag("Ceiling"))
        {
            movementVector = new Vector3(movementVector.x / 2, -movementVector.y, movementVector.z);
        }

        //Конец игры, игрок упал
        if (collision.CompareTag("Floor Enemy"))
        {
            
        }
    }
}
