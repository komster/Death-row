﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_Player_Cannons : MonoBehaviour
{
    public AudioSource shot;
    public GameObject cannonSmoke;

    public GameObject rightReloadIndicator;
    public GameObject leftReloadIndicator;

    public GameObject cannonBall;
    public GameObject[] cannons;

    public int cannonSpeed;

    public Transform[] leftCannonsSpawnPoints;
    public Transform[] rightCannonsSpawnPoints;

    public Transform[] leftCannonAimPositions;
    public Transform[] rightCannonAimPositions;

    public CS_Player player;

    public List<Text> rightAmountTilReload;
    public List<Text> leftAmountTilReload;

    private bool leftReloaded = true;
    public bool rightReloaded = true;

    private bool left1 = false;
    private bool left2 = false;
    private bool right1 = false;
    private bool right2 = false;

    private int leftReloadMultiplay = 4;
    private int rightReloadMultiplay = 4;


    private int leftCannonsPositon = 0;
    private int rightCannonsPositon = 0;

    private float rightIndicationTime = 6f;
    private bool rightIndicationOn = false;
    private float leftIndicationTime = 6f;
    private bool leftIndicationOn = false;

 

    void Start()
    {
        shot = this.gameObject.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CS_Player>();

        foreach (Transform child in GameObject.Find("ReloadRight").GetComponent<Transform>())
        {
            if (child.tag == "Reload")
            {
                rightAmountTilReload.Add(child.GetComponent<Text>());
            }
        }
        foreach (Transform child in GameObject.Find("ReloadLeft").GetComponent<Transform>())
        {
            if (child.tag == "Reload")
            {
                leftAmountTilReload.Add(child.GetComponent<Text>());
            }
        }

        rightReloadIndicator = GameObject.Find("ReloadRight");
        leftReloadIndicator = GameObject.Find("ReloadLeft");
        rightReloadIndicator.SetActive(false);
        leftReloadIndicator.SetActive(false);

    
    }

    void Update()
    {
        if (player.ended == false || player.dead == false)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.A))
            {
                if (leftReloaded == true)
                {
                    shot.Play();
                    for (int index = 0; index < cannons.Length; index++)
                    {
                        if (cannons[index].activeInHierarchy)
                        {
                            cannonBall.transform.position = new Vector3(leftCannonsSpawnPoints[index].transform.position.x, leftCannonsSpawnPoints[index].transform.position.y, 0);
                            GameObject temp = Instantiate(cannonBall);
                            Rigidbody2D rb = temp.GetComponent<Rigidbody2D>();
                            rb.velocity = (-transform.right * cannonSpeed);
                            Instantiate(cannonSmoke, leftCannonsSpawnPoints[index].transform.position, Quaternion.LookRotation(leftCannonAimPositions[index].transform.position - leftCannonsSpawnPoints[index].transform.position));

                        };
                    }

                    leftReloaded = false;
                    leftReloadMultiplay = 4;
                }

            }

            if (Input.GetKeyDown(KeyCode.Joystick2Button0))
            {
                if (rightReloaded == true)
                {
                    shot.Play();
                    for (int index = 0; index < cannons.Length; index++)
                    {
                        if (cannons[index].activeInHierarchy)
                        {
                            cannonBall.transform.position = new Vector3(rightCannonsSpawnPoints[index].transform.position.x, rightCannonsSpawnPoints[index].transform.position.y, 0);
                            GameObject temp = Instantiate(cannonBall);
                            Rigidbody2D rb = temp.GetComponent<Rigidbody2D>();
                            rb.velocity = (transform.right * cannonSpeed);
                            Instantiate(cannonSmoke, rightCannonsSpawnPoints[index].transform.position, Quaternion.LookRotation(rightCannonAimPositions[index].transform.position - rightCannonsSpawnPoints[index].transform.position));
                        }
                    }

                    rightReloaded = false;
                    rightReloadMultiplay = 4;

                }

            }

            if (Input.GetAxis("LeftReload") >= 0.5)
            {
                left1 = true;
            }
            if (Input.GetAxis("LeftReload") <= -0.5)
            {
                left2 = true;
            }

            if (Input.GetAxis("RightReload") >= 0.5)
            {
                right1 = true;
            }
            if (Input.GetAxis("RightReload") <= -0.5)
            {
                right2 = true;
            }

            if (left1 == true && left2 == true)
            {
                if (leftReloadMultiplay == 0)
                {
                    leftReloaded = true;
                    rightReloadIndicator.SetActive(false);
                    rightIndicationOn = false;
                    rightIndicationTime = 1f;
                    leftReloadMultiplay = 4;
                    left1 = false;
                    left2 = false;
                }
                else
                {
                    left1 = false;
                    left2 = false;
                    leftReloadMultiplay--;
                }


            }

            if (right1 == true && right2 == true)
            {
                if (rightReloadMultiplay == 0)
                {
                    rightReloaded = true;
                    leftReloadIndicator.SetActive(false);
                    leftIndicationOn = false;
                    leftIndicationTime = 1f;
                    rightReloadMultiplay = 4;
                    left1 = false;
                    left2 = false;
                }
                else
                {
                    right1 = false;
                    right2 = false;
                    rightReloadMultiplay--;
                }


            }


            ActivateRightReloadIndicator();
            ActivateLeftReloadIndicator();
        }

        if (leftReloaded == false)
        {
            for(int i = 0; i < rightAmountTilReload.Count; i++)
            {
                rightAmountTilReload[i].text = "x" + (leftReloadMultiplay+1);
            }
        }

        if (rightReloaded == false)
        {
            for (int i = 0; i < leftAmountTilReload.Count; i++)
            {
                leftAmountTilReload[i].text = "x" + (rightReloadMultiplay+1);
            }
        }
    }

    private void ActivateRightReloadIndicator()
    {
        if (rightIndicationOn == false)
        {
            rightIndicationOn = true;
            rightIndicationTime = 1f;

        }
        if (leftReloaded == false)
        {
            rightIndicationTime -= Time.deltaTime;
         if   (rightIndicationTime < 0f)
            {

                rightReloadIndicator.SetActive(true);
            }   
        }
    }
    private void ActivateLeftReloadIndicator()
    {
        if (leftIndicationOn == false)
        {
            leftIndicationOn = true;
            leftIndicationTime = 1f;

        }
        if (rightReloaded == false)
        {
            leftIndicationTime -= Time.deltaTime;
            if (leftIndicationTime < 0f)
            {

                leftReloadIndicator.SetActive(true);
            }
        }
    }
}
