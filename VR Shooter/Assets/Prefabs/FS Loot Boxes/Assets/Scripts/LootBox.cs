// © 2019 Flying Saci Game Studio
// written by Sylker Teles

using System;
using UnityEngine;
using System.Collections.Generic;


public class LootBox : MonoBehaviour
{
    public int amount = 10;

    bool bouncingBox = true;
    private GameObject player;
    private Player playerscript;
    public bool isOpen { get; set; }   
    Animator animator;   
    public event Action <GameObject[]> OnBoxOpen;   
    void Start()
    {
        // gets the animator
        animator = GetComponent<Animator>();

        // set the animation to bounce or not
        BounceBox(bouncingBox);
        player = GameObject.Find("Player");
        playerscript = (Player)player.GetComponent(typeof(Player));
    }

    public void BounceBox (bool bounceIt)
    {
        // flag the animator property "bounce" accordingly
        if (animator) animator.SetBool("bounce", bounceIt);
    }
    public void Open ()
    {
        // avoid opening when it's already open
        if (isOpen) return;
        isOpen = true;

        // play the open animation
        if (animator) animator.Play("Open");
        //player get money
        playerscript.AddMoney(amount);

    }
  
}
