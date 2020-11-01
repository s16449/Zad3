using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder_script : MonoBehaviour
{
        private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
           
        }
    }
}
