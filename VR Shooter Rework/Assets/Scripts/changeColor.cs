using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColor : MonoBehaviour
{
   public void changeIntoRed()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }
    public void changeIntoGreen()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }
    public void changeIntoBlue()
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }
}
