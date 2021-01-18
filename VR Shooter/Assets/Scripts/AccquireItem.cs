using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccquireItem : MonoBehaviour
{   
    public int Itemindex;
    private GameObject player;
    private Player playerscript;

    public int amount = 10;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerscript = (Player)player.GetComponent(typeof(Player));       
    }
    public void ItemAcquired()
    {
        Destroy(gameObject);
        playerscript.CollectItem(Itemindex);      
    }

    public void GunAcquired()
    {
        Destroy(gameObject);
        playerscript.CollectGun();
    }

    public void HealthAcquired()
    {
        Destroy(gameObject);
        playerscript.AddHealth(amount);
    }
}
