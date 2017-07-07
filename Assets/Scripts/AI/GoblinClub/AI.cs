﻿using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AI : MonoBehaviour
{
    public string flip;
    public GameObject player;
    public MonsterInterface Thisgoblin;
    public GameObject npc;
    public bool inSight;
    public float range;
    public int counter;
    public float xVelocity;
    public float yVelocity;
    private float x;
    private bool isFlippingLeft;
    private bool isFlippingRight;
    // Use this for initialization
    void Start () {
        counter = 0;
        InvokeRepeating("movement", 0, .03f);
        Thisgoblin.hp = 3;
        Thisgoblin.ms = .025f;
        x = Mathf.Abs(transform.localScale.x);
        isFlippingRight = false;
        isFlippingLeft = false;
    }
    
    void movement ()
    {
        var target = player.transform.position;
        var gp = Thisgoblin.transform.position;
        range = Mathf.Sqrt((target.x - gp.x)* (target.x - gp.x) + (target.z - gp.z)* (target.z - gp.z));

        if( range < 3.5 )
        {
            inSight = true;
        }
        else
        {
            if (counter % 60 ==0)
            {
                xVelocity = Random.Range(-0.02f, 0.02f);
                if(xVelocity <0)
                {
                    Flip("left");
                }
                else
                {
                    Flip("right");
                }
                yVelocity = Random.Range(-0.02f, 0.02f);
            }
            transform.Translate(xVelocity, 0f, yVelocity);
            inSight = false;
            counter++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Thisgoblin.hp <=0)
        {
            player.GetComponent<KnightStats>().exp++; //im a genius
            npc.SetActive(false);
        }
        if(Thisgoblin.hp == 1)
        {
            Thisgoblin.ms = .01f;
        }
        var target = player.transform.position;
        if (inSight)
        {
            if (transform.position.x > target.x)
            {
                Flip("left");
                transform.Translate(-Thisgoblin.ms, 0f, 0f);
            }
            else if (transform.position.x < target.x)
            {
                Flip("right");
                transform.Translate(Thisgoblin.ms, 0f, 0f);
            }
            if (transform.position.z > target.z)
            {
                transform.Translate(0f, 0f, -Thisgoblin.ms);
            }
            else
            {
                transform.Translate(0f, 0f, Thisgoblin.ms);
            }
        }
    }

    public void FlippingLeft(bool flip)
    {

    }
    public void FlippingRight(bool flip)
    {

    }

    public void Flip(string Methodflip)
    {
        flip = Methodflip;
        var theScale = transform.localScale;
        var temp = transform.localScale;
        temp.x = x;
        if (Methodflip == "right")
        {
            if (theScale.x < 0f)
                theScale.x = -theScale.x;
        }
        if (Methodflip == "left")
        {
            if (theScale.x > 0f)
                theScale.x = -theScale.x;
        }
        transform.localScale = theScale;
    }
}