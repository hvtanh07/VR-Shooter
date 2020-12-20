using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float playerSpeed = 5f;
    public float gravity = 10.0f;
    public int shootingRange = 50;
    public float movingRange = 4;
    public float gunDamage = 5;
    public int startingHealth = 100;
    public float timeBetweenShot = 0.5f;
    public int currentHealth;   
    public bool haveGun;

    public bool[] accquiredItem;
    public TextMesh healthText;
    public ParticleSystem muzzleFlash;
    public GameObject hitEffect;
    public GameObject Gun;
    
    Animator gunanim;
    CharacterController controller;

    bool teleportable;
    bool isDead;
    float timer;
    

    private void Start()
    {
        haveGun = false;
        accquiredItem = new bool[10];
        for (int i = 0; i < accquiredItem.Length; i++) accquiredItem[i] = false;
        gunanim = Gun.GetComponent<Animator>();
        controller = gameObject.GetComponent<CharacterController>();
        currentHealth = startingHealth;
        healthText.text = currentHealth.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = move * playerSpeed * Time.deltaTime;
        move = Camera.main.transform.TransformDirection(move);
        move.y -= gravity;
        controller.Move(move);
                    
        if (Input.GetButtonDown("Fire1"))
        {
            CheckHit();
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
       
    }
    private void CheckHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, shootingRange))
        {           
            if (haveGun && hit.collider.tag != "DoorPanel" && hit.collider.tag != "AcquireItem")
            {
                if (timer >= timeBetweenShot)
                {
                    muzzleFlash.Play();                  
                    Shoot(hit);
                }
            }
        }        
    }

    public void AddHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth >= startingHealth) 
            currentHealth = startingHealth;
        healthText.text = currentHealth.ToString();
    }

    public void CollectItem(int Itemindex)
    {
        accquiredItem[Itemindex] = true;
        //acquire Item sound      
    }

    public void CollectGun()
    {
        haveGun = true;
        Gun.SetActive(true);
        //acquire Item sound      
    }
    private void Shoot(RaycastHit hit)
    {
        timer = 0f;
        gunanim.SetTrigger("fired");
        Enemy enemy = hit.transform.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamege(gunDamage);
        }
        Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        //Gun sound
    }
}
