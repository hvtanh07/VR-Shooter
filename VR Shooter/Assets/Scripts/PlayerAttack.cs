using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int shootingRange = 50;
    public float gunDamage = 5;
    public float timeBetweenShot = 0.5f;

    public ParticleSystem muzzleFlash;
    public GameObject hitEffect;

    Animator gunanim;
    AudioSource gunShotAudio;
    CharacterController controller;

    bool teleportable;
    bool isDead;
    float timer;



    private void Start()
    {
        gunanim = GetComponent<Animator>();
        gunShotAudio = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if (Input.GetButtonDown("Fire1"))
        {
            CheckHit();
        }
    }
  
    private void CheckHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, shootingRange))
        {
            if (hit.collider.tag != "DoorPanel" && hit.collider.tag != "AcquireItem")
            {
                if (timer >= timeBetweenShot)
                {
                    muzzleFlash.Play();
                    Shoot(hit);
                }
            }
        }
    }

    
    private void Shoot(RaycastHit hit)
    {
        timer = 0f;
        gunanim.SetTrigger("fired");
        gunShotAudio.Play();
        Enemy enemy = hit.transform.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamege(gunDamage);
        }
        else
        {
            Bosss boss = hit.transform.GetComponent<Bosss>();
            if (boss != null)
            {
                boss.TakeDamege(gunDamage);
            }
        }
        Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
    }
}
