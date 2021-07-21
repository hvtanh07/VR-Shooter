﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float playerSpeed = 5f;
    public float gravity = 10.0f;
    public int startingHealth = 100;
    public int currentHealth;
    public int currentMoney;
    public bool haveGun;
    public GameObject StateButton;

    public bool[] accquiredItem;
    public TextMesh healthText;
    public TextMesh MoneyText;
    public TextMesh StateText;
    public TextMesh Timer;
    public TextMesh TimeEnd;
    public SumMoney sumMoney;

    public GameObject Gun;
    
    AudioSource playerAudio;
    public AudioSource playerCollecItemSound;
    CharacterController controller;

    public AudioClip Win;
    public AudioClip Lose;
    public AudioClip Key;
    public AudioClip Heart;

    bool teleportable;
    bool isDead;
    float startTime;
    //float timer;



    private void Start()
    {
        StateButton.SetActive(false);
        haveGun = false;
        currentMoney = 0;
        startTime = Time.time;
        accquiredItem = new bool[10];
        for (int i = 0; i < accquiredItem.Length; i++) accquiredItem[i] = false;
        playerAudio = GetComponent<AudioSource>();
        controller = gameObject.GetComponent<CharacterController>();
        currentHealth = startingHealth;
        healthText.text = currentHealth.ToString();
        MoneyText.text = currentMoney.ToString();
        StateText.text = "  ";
        TimeEnd.text = "  ";
    }
    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
            {
                playerAudio.Play();
            }
            else if (!Input.GetButton("Horizontal") && !Input.GetButton("Vertical") && playerAudio.isPlaying)
            {
                playerAudio.Stop();
            }
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            move = move * playerSpeed * Time.deltaTime;
            move = Camera.main.transform.TransformDirection(move);
            move.y -= gravity;
            controller.Move(move);
            TimerCounting();
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthText.text = currentHealth.ToString();

        if (currentHealth <= 0 && !isDead) Death();       
    }
    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;
        StateText.text = "YOU DIED!!!";
        TimeEnd.text = "Your time is: " + Timer.text;
        StateButton.SetActive(true);
        playerCollecItemSound.clip = Lose;
        playerCollecItemSound.Play();
        Timer.text = "   ";
        healthText.text = "  ";
        MoneyText.text = "   ";
       
    }
    public void Won()
    {
        StateText.text = "YOU WON!!!";
        TimeEnd.text = "Your time is: " + Timer.text;
        StateButton.SetActive(true);
        playerCollecItemSound.clip = Win;
        playerCollecItemSound.Play();
        Timer.text = "   ";
        healthText.text = "  ";
        MoneyText.text = "   ";
    }    

    public void AddHealth(int amount)
    {
        playerCollecItemSound.clip = Heart;
        playerCollecItemSound.Play();
        currentHealth += amount;
        if (currentHealth >= startingHealth) 
            currentHealth = startingHealth;
        healthText.text = currentHealth.ToString();
    }
    public void AddMoney(int amount)
    {
        currentMoney += amount;      
        MoneyText.text = currentMoney.ToString();
        sumMoney.Money += amount;
    }

    public void TimerCounting()
    {
        float t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");
        Timer.text = minutes + ":" + seconds;
    }

    public void CollectItem(int Itemindex)
    {
        playerCollecItemSound.clip = Key;
        playerCollecItemSound.Play();
        accquiredItem[Itemindex] = true;
        //acquire Item sound      
    }

    public void CollectGun()
    {
        haveGun = true;
        Gun.SetActive(true);
        //acquire Item sound      
    }   
}
