﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_Movement : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody2D rb;
    private float platformBodyX;
    public float jumpForce = 300f;
    private bool isGrounded;

    public bool rightDirection;
    private bool platformMove;

    public float timeDash = 0.15f;

    public float dashPause = 2f;
    public float distance;
    public LayerMask ladderLayer;
    private bool isClimbing;
    private float xDisplacement;
    private float yDisplacement;
    public bool activeDown;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isGrounded = true;
        rightDirection = true;
    }


    void Update()
    {
        //float xDisplacement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        //transform.position = new Vector3(transform.position.x + xDisplacement, transform.position.y, 0);

        xDisplacement = Input.GetAxis("Horizontal") * speed;

        if (dashPause <= 2f)
        {
            dashPause += Time.deltaTime;
            if (dashPause >= 2f)
            {
                timeDash = 0.15f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce));

            isGrounded = false;

        }

        if ((Input.GetKey(KeyCode.LeftControl) && isGrounded && timeDash > 0f) && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            if (rightDirection)
            {
                rb.AddForce(new Vector2(100, 0));
                timeDash = timeDash - Time.deltaTime;

            }
            else if (!rightDirection)
            {
                rb.AddForce(new Vector2(-100, 0));
                timeDash = timeDash - Time.deltaTime;

            }
            dashPause = 0f;

        }

        if ((Input.GetKey(KeyCode.LeftShift) && isGrounded) && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.AddForce(new Vector2(30, 0));
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.AddForce(new Vector2(-30, 0));
            }


        }

        Flip(xDisplacement);
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, ladderLayer);

        if (hitInfo.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                isClimbing = true;

            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                isClimbing = true;

            }
            //if(hitInfo.collider.tag == "Ladder")
            //{
            //    isClimbing = true;
            //}


        }
        else
        {
            isClimbing = false;
        }

        if (isClimbing == true)
        {
            yDisplacement = Input.GetAxis("Vertical") * speed;
            rb.velocity = new Vector2(rb.position.x, yDisplacement);
            rb.gravityScale = 0;

        }
        else
        {
            rb.gravityScale = 1;

        }
        rb.velocity = new Vector2(xDisplacement, rb.velocity.y);


    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;



    }
    private void OnCollisionExit2D(Collision2D collision)
    {


        isGrounded = false;
    }


    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !rightDirection || horizontal < 0 && rightDirection)
        {
            rightDirection = !rightDirection;
            Vector3 localScale = transform.localScale;

            localScale.x *= -1;
            transform.localScale = localScale;
        }

    }
}
