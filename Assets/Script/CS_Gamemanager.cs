﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CS_Gamemanager : MonoBehaviour {

    public int coins;
    public List<Text> score;
    public CS_Gamemanager scoreCoins;
    private CS_Stages stage;
    public GameObject coinPre;
    public CS_Player checkIfHit;
    private CS_Player_Movment Movement;
    private movment movement;
    private CS_Pickup_Coin pickS;
    public float timeInGame;
    public AudioSource boostSound;
    public Text pickUpParticleText;
    public Text lossParticleText;
    public GameObject lossParticle;
    public CS_DeathCounter dC;

    public GameObject boostEffect;

    private int previousScore;
    private float pointAnimDurationSec = 1f;
    private float pointAnimTimer = 0f;
    private float prcComplete;
    void Start () {
        Movement = GameObject.FindGameObjectWithTag("Player").GetComponent<CS_Player_Movment>();
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<movment>();
        checkIfHit = GameObject.FindGameObjectWithTag("Player").GetComponent<CS_Player>();
        boostEffect = GameObject.Find("Speed Particle Effect");
        stage = this.gameObject.GetComponent<CS_Stages>();
        pickUpParticleText = GameObject.Find("Text_ParticleMaterialMaker").GetComponentInChildren<Text>();
        lossParticleText = GameObject.Find("Text_ParticleMaterialMakerLoss").GetComponentInChildren<Text>();
        scoreCoins = GameObject.Find("GameManager").GetComponent<CS_Gamemanager>();
        dC = GameObject.Find("DeathCounter").GetComponent<CS_DeathCounter>();
        Cursor.visible = false;
        boostEffect.gameObject.SetActive(false);
        foreach (Transform child in GameObject.FindGameObjectWithTag("TrueCanvas").transform)
        {
            if (child.tag == "Score")
            {
                score.Add(child.GetComponent<Text>());
              
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        timeInGame += Time.deltaTime;
        if (checkIfHit.gotHit == true)
        {
            decreScore();
            checkIfHit.gotHit = false;
        }
        pointAnimTimer += Time.deltaTime;
        prcComplete = pointAnimTimer / pointAnimDurationSec;

        for (int i = 0; i < score.Count; i++)
        {
            score[i].text = "SCORE: " + Mathf.RoundToInt(Mathf.Lerp(previousScore, coins, prcComplete));
        }
       

        
        

    }
    public void CalculateFinalScore()
    {
        if (checkIfHit.ended == true)
        {
            coins += 500;
            for (int i = 0; i < Mathf.RoundToInt(dC.seconds/10); i++)
            {
                coins += 10;
            }
            for (int g = 0; g < checkIfHit.hp; g++)
            {
                coins += 500;
            }
        }
    }
    public void decreScore()
    {

        if (coins == 100)
        {
            previousScore = coins;
            coins -= 100;
            lossParticleText.text = "- " + 100;
            pointAnimTimer = 0;
            Instantiate(coinPre, checkIfHit.playerpos + new Vector3(Random.Range(-7.0f, 7.0f), Random.Range(-7.0f, 7.0f), 0), Quaternion.identity);
            Instantiate(lossParticle, GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().transform.position, Quaternion.Euler(90, 0, 0));


        }
        else if (coins == 200)
        {
            previousScore = coins;
            coins -= 200;
            lossParticleText.text = "- " + 200;
            pointAnimTimer = 0;
            Instantiate(coinPre, checkIfHit.playerpos + new Vector3(Random.Range(-7.0f, 7.0f), Random.Range(-7.0f, 7.0f), 0), Quaternion.identity);
            Instantiate(coinPre, checkIfHit.playerpos + new Vector3(Random.Range(-7.0f, 7.0f), Random.Range(-7.0f, 7.0f), 0), Quaternion.identity);
            Instantiate(lossParticle, GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().transform.position, Quaternion.Euler(90, 0, 0));
        

        }
        else if (coins >= 300)
        {
            previousScore = coins;
            coins -= 300;
            lossParticleText.text = "- " + 300;
            pointAnimTimer = 0;
            Instantiate(coinPre, checkIfHit.playerpos + new Vector3(Random.Range(-7.0f, 7.0f), Random.Range(-7.0f, 7.0f), 0), Quaternion.identity);
            Instantiate(coinPre, checkIfHit.playerpos + new Vector3(Random.Range(-7.0f, 7.0f), Random.Range(-7.0f, 7.0f), 0), Quaternion.identity);
            Instantiate(coinPre, checkIfHit.playerpos + new Vector3(Random.Range(-7.0f, 7.0f), Random.Range(-7.0f, 7.0f), 0), Quaternion.identity);
            Instantiate(lossParticle, GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().transform.position, Quaternion.Euler(90, 0, 0));
        }
        else if (coins <= 0)
        {

        }
        //score.text = "score: " + coins;
        
    }
    public void InitScore(int nrOfCoins)
    {
        previousScore = coins;
        pickUpParticleText.text = "+ " + nrOfCoins;
        coins += nrOfCoins;
        pointAnimTimer = 0;
       // score.text = "score: " + coins;
        
    }
    public void checkForBoost()
    {
        StartCoroutine(boost());
        //boostSound.Play();
    }
    
    private IEnumerator boost()
    {
        if (movement.MovSpeed < movement.MovSpeed + 6 && Movement.movSpeed < Movement.movSpeed + 6)
        {
            Movement.movSpeed += 2f;
            movement.MovSpeed += 2f;
            boostEffect.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            boostEffect.gameObject.SetActive(false);
            Movement.movSpeed -= 2f;
            movement.MovSpeed -= 2f;
            Debug.Log("Boosted");
        }
        else
        {
            yield return new WaitForSeconds(3f);
            Movement.movSpeed -= 2f;
            movement.MovSpeed -= 2f;
        }
        


    }
}
