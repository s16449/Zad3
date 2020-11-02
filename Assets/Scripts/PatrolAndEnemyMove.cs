using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PatrolAndEnemyMove : MonoBehaviour
{
    public float speed = 0.5f;
    public float doubleSpeed = 2;
    public float distance = 0.15f;
    private bool isRightDirection;
    public Transform edgeDetection;
    public LayerMask groundMask;
    public Transform player;
    private Rigidbody2D rb2d;
    //z enemy
    public float playerDistance;
    public float visualRange = 2;
    private float startPosition;


    public bool isNear;
    public bool isChasing = false;
    // Update is called once per frame



    void Start()
    {
        startPosition = transform.position.x;
        rb2d = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        playerDistance = Vector2.Distance(transform.position, player.position);

        //transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(edgeDetection.position, Vector2.down, distance);

        if (playerDistance < visualRange)
        {
            isNear = true;
        }
        else
        {
            isNear = false;
        }

        if (isNear)
        {
            if (transform.position.x < player.position.x)
            {
                rb2d.velocity = new Vector2(doubleSpeed, 0);
                transform.localScale = new Vector2(0.5f, 1);
                isRightDirection = true;


            }
            else if (transform.position.x > player.position.x)
            {
                rb2d.velocity = new Vector2(-doubleSpeed, 0);
                transform.localScale = new Vector2(-0.5f, 1);
                isRightDirection = false;
            }

            if (groundInfo.collider == false)
            {
                rb2d.velocity = new Vector2(0, 0);
            }

        }
        else
        {
            if(isRightDirection)
            {
                rb2d.velocity = new Vector2(speed, 0);
            }
            else
            {
                rb2d.velocity = new Vector2(-speed, 0);
            }

           // rb2d.velocity = new Vector2(0, 0);
            if (groundInfo.collider == false)
            {
                if(isRightDirection)
                {
                    transform.localScale = new Vector2(-0.5f, 1);
                    isRightDirection = false;
                }
                else
                {
                    transform.localScale = new Vector2(0.5f, 1);
                    isRightDirection = true;
                }
                
            }
         

        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            print(collision.collider.name.ToString());

            collision.collider.attachedRigidbody.velocity = new Vector3(60, 10, 34);
        }
            
    }

}
