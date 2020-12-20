using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{    
    public int Doorindex;
    public Renderer indicator;
    public GameObject door;

    Color originalColor;
    GameObject player;
    Player playerscript;
    Animator anim;
    public bool unlocked;   

    private void Start()
    {
        anim = GetComponent<Animator>();
        unlocked = false;

        originalColor = GetComponent<Renderer>().material.color;

        player = GameObject.Find("Player");
        playerscript = (Player)player.GetComponent(typeof(Player));
    }
    void FixedUpdate()
    { 
        if (unlocked)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, new Vector3 (door.transform.position.x,-3, door.transform.position.z), 0.02f);
        }else
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, new Vector3(door.transform.position.x, 1.1f, door.transform.position.z), 0.02f);
        }
    }

    public void Selected()
    {
        if(!unlocked)
            indicator.material.color = Color.white;
    }
    public void Unselected()
    {
        if (!unlocked)
            indicator.material.color = originalColor;
    }
    public void opendoor()
    {
        if (playerscript.accquiredItem[Doorindex])
        {
            if (Doorindex != 6)
            {
                indicator.material.color = Color.green;
                unlocked = true;
            }
            else
            {
                playerscript.Won();
            }

            //open door with sound
        }
        else
        {
            indicator.material.color = Color.red;
            //rejected sound
        }
    }
   
}
