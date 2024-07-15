using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    private bool isMoving = false;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        Move();
        if (isMoving ) 
        {
            anim.SetBool("isMoving", true);
        }
    }

    private void Move()
    {
        isMoving = true;
        transform.position += transform.forward * 10.0f * Time.deltaTime;
    }
}
