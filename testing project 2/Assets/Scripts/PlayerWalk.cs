using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    public int PlayerSpeed = 5;
    public int shootingRange = 100;
    public int interactionRange = 5;
    public float movingRange = 10;
    public float gunDamage = 5;

    public bool [] accquiredItem ; // 0-key 1-key 2-key 3-other 4-other
    public ParticleSystem muzzleFlash;
    public GameObject hitEffect;
    public GameObject telePoint;

    bool teleportable;
    bool interactable;

    private void Start()
    {
        accquiredItem = new bool[5];
        for (int i = 0; i < accquiredItem.Length; i++) accquiredItem[i] = false;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 100,Color.red);
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, shootingRange))
        {
            float dis = Vector3.Distance(hit.point, Camera.main.transform.position);    
            if (dis < movingRange && hit.collider.tag == "Ground")
            {
                teleportable = true;
                telePoint.transform.position = hit.point;           
            }
            else
            {             
                teleportable = false;
                telePoint.transform.position = new Vector3(100, 100, 100);                
            }
        }
        if (Input.GetButtonDown("Fire1"))
        {
            CheckHit(hit);
        }
    }
    private void CheckHit(RaycastHit hit)
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, shootingRange))
        {                   
            if (hit.collider.tag == "Ground" && teleportable)
            {
                Debug.Log(hit.collider.name);
                Teleport(hit.point);               
            }
            else if (hit.collider.tag != "Door" || hit.collider.tag != "AcquireItem")
            {
                muzzleFlash.Play();
                Debug.Log(hit.collider.name);
                Shoot(hit);               
            }
        }
        else muzzleFlash.Play(); //Gun sound
    }

 
    public void CollectItem(int Itemindex)
    {
        accquiredItem[Itemindex] = true;
        //acquire Item sound      
    }

    private void Shoot(RaycastHit hit)
    {                
        Enemy enemy = hit.transform.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamege(gunDamage);
        }
        Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        //Gun sound
    }

    private void Teleport(Vector3 targetLocation)
    {
        var pos = targetLocation;
        pos.y = 1;
        transform.position = pos;
        //teleport sound
    }
}
