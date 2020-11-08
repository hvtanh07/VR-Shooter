using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{    
    public int Doorindex;
    public Color originalColor;
    public GameObject player;
    private PlayerWalk playerwalk;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        originalColor = GetComponent<Renderer>().material.color;
        player = GameObject.Find("Player");
        playerwalk = (PlayerWalk)player.GetComponent(typeof(PlayerWalk));
    }
    public void Selected()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }
    public void Unselected()
    {
        GetComponent<Renderer>().material.color = originalColor;
    }
    public void opendoor()
    {
        if (playerwalk.accquiredItem[Doorindex])
        {
            //open door with sound
            Debug.Log("Door Open");
            anim.SetTrigger("opendoor");
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.red;
            //rejected sound
        }
    }

}
