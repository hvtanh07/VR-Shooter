using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{    
    public int Doorindex;
    public Renderer indicator;
    public GameObject door;
    public AudioClip wrongDoor;
    public AudioClip doorOpen;

    AudioSource doorAudio;
    Color originalColor;
    GameObject player;
    Player playerscript;
    Animator anim;
    public bool unlocked;   

    private void Start()
    {
        anim = GetComponent<Animator>();
        doorAudio = GetComponent<AudioSource>();
        unlocked = false;        
        originalColor = GetComponent<Renderer>().material.color;

        player = GameObject.Find("Player");
        playerscript = (Player)player.GetComponent(typeof(Player));
    }
    void FixedUpdate()
    { 
        if (unlocked)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, new Vector3 (door.transform.position.x,-3, door.transform.position.z), 0.01f);
        }else
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, new Vector3(door.transform.position.x, 1.1f, door.transform.position.z), 0.01f);
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
                doorAudio.clip = doorOpen;
                doorAudio.Play();
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
            doorAudio.clip = wrongDoor;
            doorAudio.Play();
            //rejected sound
        }
    }
    public void doorClose()
    {
        if (unlocked)
        {
            unlocked = false;
            doorAudio.clip = doorOpen;
            doorAudio.Play();
        }
    }
}
