using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float visualRange = 2;
    public float speed = 4 ;
    float playerDistance = 2 ;
    private float startPosition;


    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
       startPosition = transform.position.x;
        rb2d = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector2.Distance(transform.position, player.position);
        print(playerDistance);
        if(playerDistance <visualRange)
        {
            ChasePlayer();
        }
        else
        {
            StopChasingPlayer();
        }
        
    }

    void ChasePlayer()
    {
        if(transform.position.x < player.position.x)
        {
            rb2d.velocity = new Vector2(speed, 0);
            transform.localScale = new Vector2(0.5f, 1);

        }
        else if (transform.position.x > player.position.x)
        {
            rb2d.velocity = new Vector2(-speed, 0);
            transform.localScale = new Vector2(-0.5f, 1);
        }
    }

    void StopChasingPlayer()
    {
        rb2d.velocity = new Vector2(0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



}
