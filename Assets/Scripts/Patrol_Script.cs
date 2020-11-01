using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Patrol_Script : MonoBehaviour
{
    public float speed;
    public float distance;
    private bool isRightDirection;
    public Transform edgeDetection;
    public LayerMask groundMask;
    public Transform player;

    private Rigidbody2D rb2d;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(edgeDetection.position, Vector2.down, distance);
        if (transform.position.x > player.position.x)
        {
            if (groundInfo.collider == false)
            {
                if (isRightDirection)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    isRightDirection = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    isRightDirection = true;
                }
            }
        }else if(transform.position.x < player.position.x)
        {
            ChasePlayer();
        }
    }
    void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            rb2d.velocity = new Vector2(speed, 0);
            transform.localScale = new Vector2(0.5f, 1);

        }
        else if (transform.position.x > player.position.x)
        {
            rb2d.velocity = new Vector2(-speed, 0);
            transform.localScale = new Vector2(-0.5f, 1);
        }
        if (transform.position.x > player.position.x)
        {
            StopChasingPlayer();
        }
    }
    void StopChasingPlayer()
    {
        rb2d.velocity = new Vector2(0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
