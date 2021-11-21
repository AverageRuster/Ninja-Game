using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    private Rigidbody2D rb;
    private Animator animator;

    private Vector3 touchTargetPoint;
    private Vector3 movementVector;

    private bool isPlayerMoving = false;
    private bool isPlayerOnTheLeftWall = true;
    private float playerSpeed = 10000;
    private float maxPlayerSpeed = 20;

    GameObject obstacleToAttach;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetGravity();
        GetControlls();
        SetAnimation();
    }

    private void FixedUpdate()
    {
        //Проверяем, прикреплен ли игрок к препятствию
        if (obstacleToAttach == null)
        {
            //Двигаем игрока
            if (isPlayerMoving && rb.velocity.magnitude < maxPlayerSpeed)
            {
                rb.AddForce(movementVector * playerSpeed * Time.fixedDeltaTime);
            }
        }

        else
        {        
            //Смотрим, к какой стороне его прикрепить
            float xPos = obstacleToAttach.transform.position.x;
            float yPos = obstacleToAttach.transform.position.y;
            if (Mathf.Abs(transform.position.x - xPos) > Mathf.Abs(transform.position.y - yPos))
            {
                // Крепим игрока справа
                if (transform.position.x > xPos)
                {
                    xPos += 1;
                }
                //Крепим игрока слева
                else
                {
                    xPos -= 1;
                }
            }
            else
            {
                //Крепим игрока сверху
                if (transform.position.y > yPos)
                {
                    yPos += 1;
                }
                //Крепим игрока снизу
                else
                {
                    yPos -= 1;
                }
            }

            Vector3 targetPos = new Vector3(xPos, yPos);
            rb.MovePosition(targetPos);
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
        if (Input.touchCount > 0)
        {
            Touch mainTouch;

            //Берем самое первое касание
            mainTouch = Input.GetTouch(0);

            if (!isPlayerMoving)
            {
                //Если касание только началось или если игрок ведет палец по экрану, запоминаем его позицию как целевую
                if (mainTouch.phase == TouchPhase.Began || mainTouch.phase == TouchPhase.Moved)
                {
                    touchTargetPoint = mainCamera.ScreenToWorldPoint(mainTouch.position);
                }

                //Если игрок отпустил палец, начинаем движение
                else if (mainTouch.phase == TouchPhase.Ended)
                {
                    movementVector = (touchTargetPoint - transform.position + new Vector3(0, 0, 10)).normalized;
                    if (obstacleToAttach != null)
                    {
                        obstacleToAttach = null;
                    }
                    if (isPlayerOnTheLeftWall && movementVector.x > 0 || !isPlayerOnTheLeftWall && movementVector.x < 0)
                    {
                        rb.velocity = Vector3.zero;
                        isPlayerMoving = true;
                    }
                }
            }
            else
            {
                if (mainTouch.phase == TouchPhase.Began)
                {                  
                    rb.velocity = Vector3.zero;
                    if (movementVector.x >= 0)
                    {
                        movementVector = new Vector3(-0.5f, 0.5f, 0);
                    }
                    else
                    {
                        movementVector = new Vector3(0.5f, 0.5f, 0);
                    }
                }
            }
        }
    }

    private void SetAnimation()
    {
        if (isPlayerOnTheLeftWall)
        {
            animator.Play("LeftWallIdle");
        }
        else
        {
            animator.Play("RightWallIdle");
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
                rb.MovePosition(new Vector3(collision.transform.position.x + 1, transform.position.y, transform.position.z));
                isPlayerOnTheLeftWall = true;
            }

            //Правая стенка
            else
            {
                rb.MovePosition(new Vector3(collision.transform.position.x - 1, transform.position.y, transform.position.z));
                isPlayerOnTheLeftWall = false;
            }
            rb.velocity = Vector3.zero;
            isPlayerMoving = false;
        }

        //Игрок попал по врагу, режем его пополам
        if (collision.CompareTag("Enemy"))
        {
            bool horizoltalCutDirection = isPlayerMoving && movementVector.x > movementVector.y;
            collision.GetComponent<EnemyController>().KillEnemy(horizoltalCutDirection);
        }

        //Игрок попал в потолок
        if (collision.CompareTag("Ceiling"))
        {
            movementVector = new Vector3(movementVector.x / 2, -movementVector.y / 2, movementVector.z);
        }

        //Игрок цепляется за препятствие
        if (collision.CompareTag("Obstacle"))
        {            
            obstacleToAttach = collision.gameObject;
            isPlayerMoving = false;
        }

        //Конец игры, игрок упал
        if (collision.CompareTag("Floor Enemy"))
        {

        }
    }
}
