﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controls : MonoBehaviour
{
    public float Ab1;
    private float fallMultiplier = 1.5f;
    private float lowJumpMultiplier = 2.22f;
    private bool isMoving;
    private bool isRunning;
    public GameObject autoAttack;
    public GameObject SetSkillTreeActive;
    public char previousKey;
    public char previousKeyws;
    public bool isFlippingLeft;
    public bool isFlippingRight;
    public bool onGround;
    private Rigidbody rb;
    private bool STActive;
    Animator action;
    public AudioClip jump;
    private AudioSource source;


    void Start()
    {
        source = GetComponent<AudioSource>();
        Ab1 = 1;
        rb = GetComponent<Rigidbody>();
        isMoving = false;
        action = GetComponent<Animator>();
        previousKey = 'd';
        onGround = true;
    }
    private void Awake()
    {
        SetSkillTreeActive = FindObjectOfType<SKIdentifier>().gameObject;

    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K) && !STActive)
            {
            SetSkillTreeActive.SetActive(true);
            STActive = true;
            }
        else if (Input.GetKeyDown(KeyCode.K) && STActive)
        {
            SetSkillTreeActive.SetActive(false);
            STActive = false;
        }
        action.SetBool("walking", false); // used for animation
        // BEING JUMPING SCRIPT
        if(Input.GetKeyDown(KeyCode.Space) && gameObject.GetComponent<KnightStats>().energy > 5 && onGround)
        {

            source.clip = jump;
            source.Play();
            action.SetBool("jump", true);

            gameObject.GetComponent<KnightStats>().energy = gameObject.GetComponent<KnightStats>().energy - 5;
            rb.velocity = new Vector3(0f, 4f + Ab1, 0f);
            onGround = false;
        }
        if(rb.velocity.y <0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier) * Time.deltaTime;
        }
        else if(rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        //END JUMPING SCRIPT
        if (gameObject.GetComponent<KnightStats>().energy > 0)
        {
            CheckWalking();
            CheckRunning();
            CheckRoll();

            if (!isRunning)
            {
            }
            //BASIC INPUT
            if (Input.GetKey("d"))
            {

                if (previousKey == 'a')
                {
                    isFlippingLeft = false;
                    isFlippingRight = true;
                    //GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                }
                if (GetComponent<Rigidbody>().velocity.x < gameObject.GetComponent<KnightStats>().movementSpeed * 200)
                    GetComponent<Rigidbody>().velocity += new Vector3(gameObject.GetComponent<KnightStats>().movementSpeed *20, 0f, 0f);
                previousKey = 'd';
            }
            if (Input.GetKey("a"))
            {
                if (previousKey == 'd')
                {
                    isFlippingLeft = true;
                    isFlippingRight = false;
                   // GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                }
                if (GetComponent<Rigidbody>().velocity.x > -gameObject.GetComponent<KnightStats>().movementSpeed * 200)
                    GetComponent<Rigidbody>().velocity += new Vector3(-gameObject.GetComponent<KnightStats>().movementSpeed * 20, 0f, 0f);
                previousKey = 'a';
            }

            if (Input.GetKey("w"))
            {

                if (GetComponent<Rigidbody>().velocity.z < gameObject.GetComponent<KnightStats>().movementSpeed * 200)
                    GetComponent<Rigidbody>().velocity += new Vector3(0f, 0f, gameObject.GetComponent<KnightStats>().movementSpeed * 20);
            }

            if (Input.GetKey("s"))
            {

                if (GetComponent<Rigidbody>().velocity.z > -gameObject.GetComponent<KnightStats>().movementSpeed * 200)
                    GetComponent<Rigidbody>().velocity += new Vector3(0f, 0f, -gameObject.GetComponent<KnightStats>().movementSpeed * 20);
            }
            if (isMoving && isRunning && Input.GetKey("left shift"))
            {
                action.SetBool("shift", true);
                gameObject.GetComponent<KnightStats>().energy = gameObject.GetComponent<KnightStats>().energy - .05f;
            }
        }
                    if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0f, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
            }
            if (Input.GetKeyUp("w") || Input.GetKeyUp("s"))
            {
                GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, 0f);
            }
        CheckFlipping();


    }
    public void CheckRunning()
    {
        if (Input.GetKey("left shift") && (Input.GetKey("a") || Input.GetKey("d") || Input.GetKey("w") || Input.GetKey("s")))
        {
            isRunning = true;
            if (Ab1 == 1.5f)
                gameObject.GetComponent<KnightStats>().movementSpeed = 0.05f * Ab1 + gameObject.GetComponent<KnightStats>().SPBonusMS;
            else
                gameObject.GetComponent<KnightStats>().movementSpeed = 0.05f;
        }
        else
        {
            isRunning = false;
            action.SetBool("shift", false);
        }
    }
    public void CheckWalking()
    {
        if (!(Input.GetKey("a") && Input.GetKey("d")) && !(Input.GetKey("s") && Input.GetKey("w")) && (Input.GetKey("a") || Input.GetKey("d") || Input.GetKey("w") || Input.GetKey("s")))
        {
            isMoving = true;
            action.SetBool("walking", true);
            if (action.GetBool("roll") == false)
            {
                if (Ab1 == 1.5f)
                    gameObject.GetComponent<KnightStats>().movementSpeed = 0.02f * Ab1 + gameObject.GetComponent<KnightStats>().SPBonusMS;
                else
                    gameObject.GetComponent<KnightStats>().movementSpeed = 0.02f;
            }
        }
    }
    public void CheckRoll()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && gameObject.GetComponent<KnightStats>().energy >= 20)
        {
            action.SetBool("roll", true);

            if (!action.GetCurrentAnimatorStateInfo(0).IsName("roll"))
            {
                gameObject.GetComponent<KnightStats>().energy = gameObject.GetComponent<KnightStats>().energy - 20;
            }
            if (Ab1 == 1.5f)
                gameObject.GetComponent<KnightStats>().movementSpeed = 0.1f * Ab1 + gameObject.GetComponent<KnightStats>().SPBonusMS;
            else
                gameObject.GetComponent<KnightStats>().movementSpeed = 0.1f;
        }
        if (action.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !action.IsInTransition(0))
        {
            action.SetBool("roll", false);
        }
    }
    public void CheckFlipping()
    {
        if (Ab1 == 1.5f) //ability1 active
        {
            //FLIPPING LEFT
            if (isFlippingLeft && transform.localScale.x > -.40f - gameObject.GetComponent<KnightStats>().SPBonusScale)
            {
                float rotationSpeed = 5.0f;
                Vector3 rot = transform.localScale;
                rot.x = rot.x + -rotationSpeed * Time.deltaTime;
                transform.localScale = rot;

            }
            else if (isFlippingLeft && transform.localScale.x <= -.41f - gameObject.GetComponent<KnightStats>().SPBonusScale)
            {
                Vector3 rot = transform.localScale;
                rot.x = -.41f - gameObject.GetComponent<KnightStats>().SPBonusScale;
                transform.localScale = rot;
            }
            //FLIPPING RIGHT
            if (isFlippingRight && transform.localScale.x < .40f + gameObject.GetComponent<KnightStats>().SPBonusScale)
            {
                float rotationSpeed = 5.0f;
                Vector3 rot = transform.localScale;
                rot.x = rot.x + rotationSpeed * Time.deltaTime;
                transform.localScale = rot;

            }
            else if (isFlippingRight && transform.localScale.x >= .41f + gameObject.GetComponent<KnightStats>().SPBonusScale)
            {
                Vector3 rot = transform.localScale;
                rot.x = .41f + gameObject.GetComponent<KnightStats>().SPBonusScale;
                transform.localScale = rot;
            }
        }
        else
        {
            //FLIPPING LEFT
            if (isFlippingLeft && transform.localScale.x > -.40f)
            {
                float rotationSpeed = 5.0f;
                Vector3 rot = transform.localScale;
                rot.x = rot.x + -rotationSpeed * Time.deltaTime;
                transform.localScale = rot;

            }
            else if (isFlippingLeft && transform.localScale.x <= -.41f)
            {
                Vector3 rot = transform.localScale;
                rot.x = -.41f;
                transform.localScale = rot;
            }
            //FLIPPING RIGHT
            if (isFlippingRight && transform.localScale.x < .40f)
            {
                float rotationSpeed = 5.0f;
                Vector3 rot = transform.localScale;
                rot.x = rot.x + rotationSpeed * Time.deltaTime;
                transform.localScale = rot;

            }
            else if (isFlippingRight && transform.localScale.x >= .41f)
            {
                Vector3 rot = transform.localScale;
                rot.x = .41f;
                transform.localScale = rot;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Scenery" || collision.gameObject.tag == "InteractableScenery" || collision.gameObject.tag == "npc" )
        {
            onGround = true;
            action.SetBool("jump", false);
        }
    }
}
/* CONTROLLER SUPPORT
        vlimit = Input.GetAxis("Vertical");
        hlimit = Input.GetAxis("Horizontal");
        if(Input.GetAxis("Vertical") <.1f && Input.GetAxis("Vertical") > -.1f)
        {
            transform.Translate(0f, Input.GetAxis("Vertical"), 0f);

        }
        else if (Input.GetAxis("Vertical") > .1f )
        {
            vlimit = .1f;
            transform.Translate(0f, vlimit, 0f);
        }
        else if (Input.GetAxis("Vertical") < -.1f)
        {
            vlimit = -.1f;
            transform.Translate(0f, vlimit, 0f);

        }
        if (Input.GetAxis("Horizontal") < .1f && Input.GetAxis("Horizontal") > -.1f)
        {
            transform.Translate(Input.GetAxis("Horizontal"), 0f, 0f);

        }

        else if (Input.GetAxis("Horizontal") > .1f)
        {
            hlimit = -.1f;
            transform.Translate(hlimit, 0f, 0f);

        }
        else if (Input.GetAxis("Horizontal") < -.1f)
        {
            hlimit = .1f;
            transform.Translate(hlimit, 0f, 0f);

        }
    */
