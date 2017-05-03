﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Enemy_Battel : MonoBehaviour {

    public float movSpeed = 6;

    public Rigidbody2D rb;
    public GameObject player;
    public GameObject cannonBall;

    public Transform leftCannon;
    public Transform rightCannon;

    private Vector3 direction;
    private float cannonSpeed = 10;
    private float reloadTimer = 5;
    private bool reloding = false;


    void Start () {
		
	}
	
	void Update () {

        float distans = Vector3.Distance(transform.position, player.transform.position);
        rb.velocity = transform.up * movSpeed;
        direction = transform.position - player.transform.position;
        direction.Normalize();
        float angle = Vector3.Angle(direction, transform.up);

        if (distans >10)
        {
            if (angle <= 130)
            {
                movSpeed = 4;
                if (angle <= 90)
                {
                    movSpeed = 2;
                }
                transform.Rotate(0, 0, direction.x * direction.y * 2);
            }
            else
            {
                movSpeed = 6;
            }
        }

        if (distans <= 10)
        {
            movSpeed = 2;

            if (reloding == false && angle < 95 && angle > 85)
            {
                if (direction.x * direction.y < 0)
                {
                    cannonBall.transform.position = leftCannon.position;
                    GameObject temp = Instantiate(cannonBall);
                    Rigidbody2D rb = temp.GetComponent<Rigidbody2D>();
                    rb.velocity = (-transform.right * cannonSpeed);
                    reloding = true;
                }
                if (direction.x * direction.y > 0)
                {
                    cannonBall.transform.position = rightCannon.position;
                    GameObject temp = Instantiate(cannonBall);
                    Rigidbody2D rb = temp.GetComponent<Rigidbody2D>();
                    rb.velocity = (transform.right * cannonSpeed);
                    reloding = true;
                }

            }
            if (reloding == true)
            {
                reloadTimer -= Time.deltaTime;
                if (reloadTimer <= 0)
                {
                    reloding = false;
                    reloadTimer = 5;
                }
            }

        }
  
    }
}
