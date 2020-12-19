using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToNextScene : MonoBehaviour
{
    public GameObject prevStage;
    public GameObject nextStage;
   
    public Door door;
    GameObject player;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            prevStage.SetActive(false);
            nextStage.SetActive(true);

            //delete prevStage
            //spawn nextStage
        }
        door.unlocked = false;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
