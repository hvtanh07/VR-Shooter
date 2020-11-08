using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccquireItem : MonoBehaviour
{   
    public int Itemindex;
    public Color originalColor;
    private GameObject player;
    private PlayerWalk playerwalk;

    private void Start()
    {
        originalColor = GetComponent<Renderer>().material.color;
        player = GameObject.Find("Player");
        playerwalk = (PlayerWalk)player.GetComponent(typeof(PlayerWalk));       
    }
    public void Selected()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }
    public void Unselected()
    {
        GetComponent<Renderer>().material.color = originalColor;
    }
    public void ItemAcquired()
    {
        Destroy(gameObject);
        playerwalk.CollectItem(Itemindex);
    }
}
